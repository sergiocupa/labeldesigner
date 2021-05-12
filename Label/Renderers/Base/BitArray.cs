

using System;


namespace Label.Renderers.Base
{
    public class BitArray
    {
        private int[] bits;
        private int size;


        public int Size
        {
            get
            {
                return size;
            }
        }


        public int SizeInBytes
        {
            get
            {
                return (size + 7) >> 3;
            }
        }


        public bool this[int i]
        {
            get
            {
                return (bits[i >> 5] & (1 << (i & 0x1F))) != 0;
            }
            set
            {
                if (value)
                    bits[i >> 5] |= 1 << (i & 0x1F);
            }
        }


        public BitArray()
        {
            this.size = 0;
            this.bits = new int[1];
        }


        public BitArray(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException("size must be at least 1");
            }
            this.size = size;
            this.bits = makeArray(size);
        }

        // For testing only
        private BitArray(int[] bits, int size)
        {
            this.bits = bits;
            this.size = size;
        }

        private void ensureCapacity(int size)
        {
            if (size > bits.Length << 5)
            {
                int[] newBits = makeArray(size);
                System.Array.Copy(bits, 0, newBits, 0, bits.Length);
                bits = newBits;
            }
        }


        public void flip(int i)
        {
            bits[i >> 5] ^= 1 << (i & 0x1F);
        }

        private static int numberOfTrailingZeros(int num)
        {
            var index = (-num & num) % 37;
            if (index < 0)
                index *= -1;
            return _lookup[index];
        }

        private static readonly int[] _lookup =
           {
            32, 0, 1, 26, 2, 23, 27, 0, 3, 16, 24, 30, 28, 11, 0, 13, 4, 7, 17,
            0, 25, 22, 31, 15, 29, 10, 12, 6, 0, 21, 14, 9, 5, 20, 8, 19, 18
         };



        public int getNextSet(int from)
        {
            if (from >= size)
            {
                return size;
            }
            int bitsOffset = from >> 5;
            int currentBits = bits[bitsOffset];
            // mask off lesser bits first
            currentBits &= -(1 << (from & 0x1F));
            while (currentBits == 0)
            {
                if (++bitsOffset == bits.Length)
                {
                    return size;
                }
                currentBits = bits[bitsOffset];
            }
            int result = (bitsOffset << 5) + numberOfTrailingZeros(currentBits);
            return result > size ? size : result;
        }



        public int getNextUnset(int from)
        {
            if (from >= size)
            {
                return size;
            }
            int bitsOffset = from >> 5;
            int currentBits = ~bits[bitsOffset];
            // mask off lesser bits first
            currentBits &= -(1 << (from & 0x1F));
            while (currentBits == 0)
            {
                if (++bitsOffset == bits.Length)
                {
                    return size;
                }
                currentBits = ~bits[bitsOffset];
            }
            int result = (bitsOffset << 5) + numberOfTrailingZeros(currentBits);
            return result > size ? size : result;
        }



        public void setBulk(int i, int newBits)
        {
            bits[i >> 5] = newBits;
        }



        public void setRange(int start, int end)
        {
            if (end < start || start < 0 || end > size)
            {
                throw new ArgumentException();
            }
            if (end == start)
            {
                return;
            }
            end--; // will be easier to treat this as the last actually set bit -- inclusive
            int firstInt = start >> 5;
            int lastInt = end >> 5;
            for (int i = firstInt; i <= lastInt; i++)
            {
                int firstBit = i > firstInt ? 0 : start & 0x1F;
                int lastBit = i < lastInt ? 31 : end & 0x1F;
                // Ones from firstBit to lastBit, inclusive
                int mask = (2 << lastBit) - (1 << firstBit);
                bits[i] |= mask;
            }
        }


        public void clear()
        {
            int max = bits.Length;
            for (int i = 0; i < max; i++)
            {
                bits[i] = 0;
            }
        }


        public bool isRange(int start, int end, bool value)
        {
            if (end < start || start < 0 || end > size)
            {
                throw new ArgumentException();
            }
            if (end == start)
            {
                return true; // empty range matches
            }
            end--; // will be easier to treat this as the last actually set bit -- inclusive    
            int firstInt = start >> 5;
            int lastInt = end >> 5;
            for (int i = firstInt; i <= lastInt; i++)
            {
                int firstBit = i > firstInt ? 0 : start & 0x1F;
                int lastBit = i < lastInt ? 31 : end & 0x1F;
                // Ones from firstBit to lastBit, inclusive
                int mask = (2 << lastBit) - (1 << firstBit);

                // Return false if we're looking for 1s and the masked bits[i] isn't all 1s (that is,
                // equals the mask, or we're looking for 0s and the masked portion is not all 0s
                if ((bits[i] & mask) != (value ? mask : 0))
                {
                    return false;
                }
            }
            return true;
        }


        public void appendBit(bool bit)
        {
            ensureCapacity(size + 1);
            if (bit)
            {
                bits[size >> 5] |= 1 << (size & 0x1F);
            }
            size++;
        }



        public int[] Array
        {
            get { return bits; }
        }



        public void appendBits(int value, int numBits)
        {
            if (numBits < 0 || numBits > 32)
            {
                throw new ArgumentException("Num bits must be between 0 and 32");
            }
            ensureCapacity(size + numBits);
            for (int numBitsLeft = numBits; numBitsLeft > 0; numBitsLeft--)
            {
                appendBit(((value >> (numBitsLeft - 1)) & 0x01) == 1);
            }
        }



        public void appendBitArray(BitArray other)
        {
            int otherSize = other.size;
            ensureCapacity(size + otherSize);
            for (int i = 0; i < otherSize; i++)
            {
                appendBit(other[i]);
            }
        }


        public void xor(BitArray other)
        {
            if (size != other.size)
            {
                throw new ArgumentException("Sizes don't match");
            }
            for (int i = 0; i < bits.Length; i++)
            {
                // The last int could be incomplete (i.e. not have 32 bits in
                // it) but there is no problem since 0 XOR 0 == 0.
                bits[i] ^= other.bits[i];
            }
        }



        public void toBytes(int bitOffset, byte[] array, int offset, int numBytes)
        {
            for (int i = 0; i < numBytes; i++)
            {
                int theByte = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (this[bitOffset])
                    {
                        theByte |= 1 << (7 - j);
                    }
                    bitOffset++;
                }
                array[offset + i] = (byte)theByte;
            }
        }



        public void reverse()
        {
            var newBits = new int[bits.Length];
            // reverse all int's first
            var len = ((size - 1) >> 5);
            var oldBitsLen = len + 1;
            for (var i = 0; i < oldBitsLen; i++)
            {
                var x = (long)bits[i];
                x = ((x >> 1) & 0x55555555u) | ((x & 0x55555555u) << 1);
                x = ((x >> 2) & 0x33333333u) | ((x & 0x33333333u) << 2);
                x = ((x >> 4) & 0x0f0f0f0fu) | ((x & 0x0f0f0f0fu) << 4);
                x = ((x >> 8) & 0x00ff00ffu) | ((x & 0x00ff00ffu) << 8);
                x = ((x >> 16) & 0x0000ffffu) | ((x & 0x0000ffffu) << 16);
                newBits[len - i] = (int)x;
            }
            // now correct the int's if the bit size isn't a multiple of 32
            if (size != oldBitsLen * 32)
            {
                var leftOffset = oldBitsLen * 32 - size;
                var currentInt = ((int)((uint)newBits[0] >> leftOffset)); // (newBits[0] >>> leftOffset);
                for (var i = 1; i < oldBitsLen; i++)
                {
                    var nextInt = newBits[i];
                    currentInt |= nextInt << (32 - leftOffset);
                    newBits[i - 1] = currentInt;
                    currentInt = ((int)((uint)nextInt >> leftOffset)); // (nextInt >>> leftOffset);
                }
                newBits[oldBitsLen - 1] = currentInt;
            }
            bits = newBits;
        }

        private static int[] makeArray(int size)
        {
            return new int[(size + 31) >> 5];
        }



        public override bool Equals(Object o)
        {
            var other = o as BitArray;
            if (other == null)
                return false;
            if (size != other.size)
                return false;
            for (var index = 0; index < bits.Length; index++)
            {
                if (bits[index] != other.bits[index])
                    return false;
            }
            return true;
        }



        public override int GetHashCode()
        {
            var hash = size;
            foreach (var bit in bits)
            {
                hash = 31 * hash + bit.GetHashCode();
            }
            return hash;
        }



        public override String ToString()
        {
            var result = new System.Text.StringBuilder(size + (size / 8) + 1);
            for (int i = 0; i < size; i++)
            {
                if ((i & 0x07) == 0)
                {
                    result.Append(' ');
                }
                result.Append(this[i] ? 'X' : '.');
            }
            return result.ToString();
        }



        public object Clone()
        {
            return new BitArray((int[])bits.Clone(), size);
        }
    }
}
