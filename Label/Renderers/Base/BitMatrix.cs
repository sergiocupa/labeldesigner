

using System;
using System.Text;


namespace Label.Renderers.Base
{
    public class BitMatrix
    {
        private readonly int width;
        private readonly int height;
        private readonly int rowSize;
        private readonly int[] bits;


        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }


        public int Dimension
        {
            get
            {
                if (width != height)
                {
                    throw new ArgumentException("Can't call Dimension on a non-square matrix");
                }
                return width;
            }

        }


        public int RowSize
        {
            get { return rowSize; }
        }

        public BitMatrix(int dimension)
            : this(dimension, dimension)
        {
        }


        public BitMatrix(int width, int height)
        {
            if (width < 1 || height < 1)
            {
                throw new System.ArgumentException("Both dimensions must be greater than 0");
            }
            this.width = width;
            this.height = height;
            this.rowSize = (width + 31) >> 5;
            bits = new int[rowSize * height];
        }

        internal BitMatrix(int width, int height, int rowSize, int[] bits)
        {
            this.width = width;
            this.height = height;
            this.rowSize = rowSize;
            this.bits = bits;
        }

        internal BitMatrix(int width, int height, int[] bits)
        {
            this.width = width;
            this.height = height;
            this.rowSize = (width + 31) >> 5;
            this.bits = bits;
        }


        public static BitMatrix parse(bool[][] image)
        {
            var height = image.Length;
            var width = image[0].Length;
            var bits = new BitMatrix(width, height);
            for (var i = 0; i < height; i++)
            {
                var imageI = image[i];
                for (var j = 0; j < width; j++)
                {
                    bits[j, i] = imageI[j];
                }
            }
            return bits;
        }

        public static BitMatrix parse(String stringRepresentation, String setString, String unsetString)
        {
            if (stringRepresentation == null)
            {
                throw new ArgumentException();
            }

            bool[] bits = new bool[stringRepresentation.Length];
            int bitsPos = 0;
            int rowStartPos = 0;
            int rowLength = -1;
            int nRows = 0;
            int pos = 0;
            while (pos < stringRepresentation.Length)
            {
                if (stringRepresentation.Substring(pos, 1).Equals("\n") ||
                    stringRepresentation.Substring(pos, 1).Equals("\r"))
                {
                    if (bitsPos > rowStartPos)
                    {
                        if (rowLength == -1)
                        {
                            rowLength = bitsPos - rowStartPos;
                        }
                        else if (bitsPos - rowStartPos != rowLength)
                        {
                            throw new ArgumentException("row lengths do not match");
                        }
                        rowStartPos = bitsPos;
                        nRows++;
                    }
                    pos++;
                }
                else if (stringRepresentation.Substring(pos, setString.Length).Equals(setString))
                {
                    pos += setString.Length;
                    bits[bitsPos] = true;
                    bitsPos++;
                }
                else if (stringRepresentation.Substring(pos, unsetString.Length).Equals(unsetString))
                {
                    pos += unsetString.Length;
                    bits[bitsPos] = false;
                    bitsPos++;
                }
                else
                {
                    throw new ArgumentException("illegal character encountered: " + stringRepresentation.Substring(pos));
                }
            }

            // no EOL at end?
            if (bitsPos > rowStartPos)
            {
                if (rowLength == -1)
                {
                    rowLength = bitsPos - rowStartPos;
                }
                else if (bitsPos - rowStartPos != rowLength)
                {
                    throw new ArgumentException("row lengths do not match");
                }
                nRows++;
            }

            BitMatrix matrix = new BitMatrix(rowLength, nRows);
            for (int i = 0; i < bitsPos; i++)
            {
                if (bits[i])
                {
                    matrix[i % rowLength, i / rowLength] = true;
                }
            }
            return matrix;
        }


        public bool this[int x, int y]
        {
            get
            {
                int offset = y * rowSize + (x >> 5);
                return (((int)((uint)(bits[offset]) >> (x & 0x1f))) & 1) != 0;
            }
            set
            {
                if (value)
                {
                    int offset = y * rowSize + (x >> 5);
                    bits[offset] |= 1 << (x & 0x1f);
                }
                else
                {
                    int offset = y * rowSize + (x / 32);
                    bits[offset] &= ~(1 << (x & 0x1f));
                }
            }
        }


        public void flip(int x, int y)
        {
            int offset = y * rowSize + (x >> 5);
            bits[offset] ^= 1 << (x & 0x1f);
        }


        public void flipWhen(Func<int, int, bool> shouldBeFlipped)
        {
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (shouldBeFlipped(y, x))
                    {
                        int offset = y * rowSize + (x >> 5);
                        bits[offset] ^= 1 << (x & 0x1f);
                    }
                }
            }
        }


        public void xor(BitMatrix mask)
        {
            if (width != mask.Width || height != mask.Height || rowSize != mask.RowSize)
            {
                throw new ArgumentException("input matrix dimensions do not match");
            }
            var rowArray = new BitArray(width);
            for (int y = 0; y < height; y++)
            {
                int offset = y * rowSize;
                int[] row = mask.getRow(y, rowArray).Array;
                for (int x = 0; x < rowSize; x++)
                {
                    bits[offset + x] ^= row[x];
                }
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


        public void setRegion(int left, int top, int width, int height)
        {
            if (top < 0 || left < 0)
            {
                throw new System.ArgumentException("Left and top must be nonnegative");
            }
            if (height < 1 || width < 1)
            {
                throw new System.ArgumentException("Height and width must be at least 1");
            }
            int right = left + width;
            int bottom = top + height;
            if (bottom > this.height || right > this.width)
            {
                throw new System.ArgumentException("The region must fit inside the matrix");
            }
            for (int y = top; y < bottom; y++)
            {
                int offset = y * rowSize;
                for (int x = left; x < right; x++)
                {
                    bits[offset + (x >> 5)] |= 1 << (x & 0x1f);
                }
            }
        }


        public BitArray getRow(int y, BitArray row)
        {
            if (row == null || row.Size < width)
            {
                row = new BitArray(width);
            }
            else
            {
                row.clear();
            }
            int offset = y * rowSize;
            for (int x = 0; x < rowSize; x++)
            {
                row.setBulk(x << 5, bits[offset + x]);
            }
            return row;
        }


        public void setRow(int y, BitArray row)
        {
            Array.Copy(row.Array, 0, bits, y * rowSize, rowSize);
        }


        public void rotate180()
        {
            var topRow = new BitArray(width);
            var bottomRow = new BitArray(width);
            int maxHeight = (height + 1) / 2;
            for (int i = 0; i < maxHeight; i++)
            {
                topRow = getRow(i, topRow);
                int bottomRowIndex = height - 1 - i;
                bottomRow = getRow(bottomRowIndex, bottomRow);
                topRow.reverse();
                bottomRow.reverse();
                setRow(i, bottomRow);
                setRow(bottomRowIndex, topRow);
            }
        }


        public int[] getEnclosingRectangle()
        {
            int left = width;
            int top = height;
            int right = -1;
            int bottom = -1;

            for (int y = 0; y < height; y++)
            {
                for (int x32 = 0; x32 < rowSize; x32++)
                {
                    int theBits = bits[y * rowSize + x32];
                    if (theBits != 0)
                    {
                        if (y < top)
                        {
                            top = y;
                        }
                        if (y > bottom)
                        {
                            bottom = y;
                        }
                        if (x32 * 32 < left)
                        {
                            int bit = 0;
                            while ((theBits << (31 - bit)) == 0)
                            {
                                bit++;
                            }
                            if ((x32 * 32 + bit) < left)
                            {
                                left = x32 * 32 + bit;
                            }
                        }
                        if (x32 * 32 + 31 > right)
                        {
                            int bit = 31;
                            while (((int)((uint)theBits >> bit)) == 0) // (theBits >>> bit)
                            {
                                bit--;
                            }
                            if ((x32 * 32 + bit) > right)
                            {
                                right = x32 * 32 + bit;
                            }
                        }
                    }
                }
            }

            if (right < left || bottom < top)
            {
                return null;
            }

            return new[] { left, top, right - left + 1, bottom - top + 1 };
        }


        public int[] getTopLeftOnBit()
        {
            int bitsOffset = 0;
            while (bitsOffset < bits.Length && bits[bitsOffset] == 0)
            {
                bitsOffset++;
            }
            if (bitsOffset == bits.Length)
            {
                return null;
            }
            int y = bitsOffset / rowSize;
            int x = (bitsOffset % rowSize) << 5;

            int theBits = bits[bitsOffset];
            int bit = 0;
            while ((theBits << (31 - bit)) == 0)
            {
                bit++;
            }
            x += bit;
            return new[] { x, y };
        }


        public int[] getBottomRightOnBit()
        {
            int bitsOffset = bits.Length - 1;
            while (bitsOffset >= 0 && bits[bitsOffset] == 0)
            {
                bitsOffset--;
            }
            if (bitsOffset < 0)
            {
                return null;
            }

            int y = bitsOffset / rowSize;
            int x = (bitsOffset % rowSize) << 5;

            int theBits = bits[bitsOffset];
            int bit = 31;

            while (((int)((uint)theBits >> bit)) == 0) // (theBits >>> bit)
            {
                bit--;
            }
            x += bit;

            return new int[] { x, y };
        }


        public override bool Equals(object obj)
        {
            if (!(obj is BitMatrix))
            {
                return false;
            }
            var other = (BitMatrix)obj;
            if (width != other.width || height != other.height ||
                rowSize != other.rowSize || bits.Length != other.bits.Length)
            {
                return false;
            }
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i] != other.bits[i])
                {
                    return false;
                }
            }
            return true;
        }


        public override int GetHashCode()
        {
            int hash = width;
            hash = 31 * hash + width;
            hash = 31 * hash + height;
            hash = 31 * hash + rowSize;
            foreach (var bit in bits)
            {
                hash = 31 * hash + bit.GetHashCode();
            }
            return hash;
        }


        public override String ToString()
        {
#if WindowsCE
         return ToString("X ", "  ", "\r\n");
#else
            return ToString("X ", "  ", Environment.NewLine);
#endif
        }


        public String ToString(String setString, String unsetString)
        {
#if WindowsCE
         return buildToString(setString, unsetString, "\r\n");
#else
            return buildToString(setString, unsetString, Environment.NewLine);
#endif
        }



        public String ToString(String setString, String unsetString, String lineSeparator)
        {
            return buildToString(setString, unsetString, lineSeparator);
        }

        private String buildToString(String setString, String unsetString, String lineSeparator)
        {
            var result = new StringBuilder(height * (width + 1));
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result.Append(this[x, y] ? setString : unsetString);
                }
                result.Append(lineSeparator);
            }
            return result.ToString();
        }



        public object Clone()
        {
            return new BitMatrix(width, height, rowSize, (int[])bits.Clone());
        }
    }
}
