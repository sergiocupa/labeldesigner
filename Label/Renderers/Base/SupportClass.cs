

using System;
using System.Collections.Generic;
using System.Text;


namespace Label.Renderers.Base
{
    public static class SupportClass
    {


        public static void GetCharsFromString(System.String sourceString, int sourceStart, int sourceEnd, char[] destinationArray, int destinationStart)
        {
            int sourceCounter = sourceStart;
            int destinationCounter = destinationStart;
            while (sourceCounter < sourceEnd)
            {
                destinationArray[destinationCounter] = (char)sourceString[sourceCounter];
                sourceCounter++;
                destinationCounter++;
            }
        }


        public static void SetCapacity<T>(System.Collections.Generic.IList<T> vector, int newCapacity) where T : new()
        {
            while (newCapacity > vector.Count)
                vector.Add(new T());
            while (newCapacity < vector.Count)
                vector.RemoveAt(vector.Count - 1);
        }


        public static String[] toStringArray(ICollection<string> strings)
        {
            var result = new String[strings.Count];
            strings.CopyTo(result, 0);
            return result;
        }


        public static string Join<T>(string separator, IEnumerable<T> values)
        {
            var builder = new StringBuilder();
            separator = separator ?? String.Empty;
            if (values != null)
            {
                foreach (var value in values)
                {
                    builder.Append(value);
                    builder.Append(separator);
                }
                if (builder.Length > 0)
                    builder.Length -= separator.Length;
            }

            return builder.ToString();
        }


        public static void Fill<T>(T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }


        public static void Fill<T>(T[] array, int startIndex, int endIndex, T value)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                array[i] = value;
            }
        }


        public static string ToBinaryString(int x)
        {
            char[] bits = new char[32];
            int i = 0;

            while (x != 0)
            {
                bits[i++] = (x & 1) == 1 ? '1' : '0';
                x >>= 1;
            }

            Array.Reverse(bits, 0, i);
            return new string(bits);
        }


        public static int bitCount(int n)
        {
            int ret = 0;
            while (n != 0)
            {
                n &= (n - 1);
                ret++;
            }
            return ret;
        }


        public static T GetValue<T>(IDictionary<DecodeHintType, object> hints, DecodeHintType hintType, T @default)
        {
            // can't use extension method because of .Net 2.0 support

            if (hints == null)
                return @default;
            if (!hints.ContainsKey(hintType))
                return @default;

            return (T)hints[hintType];
        }
    }
}
