

using System;
using System.Text;


namespace Label.Renderers.Base
{
    internal class ByteMatrix
    {
        private readonly byte[][] bytes;
        private readonly int width;
        private readonly int height;


        public ByteMatrix(int width, int height)
        {
            bytes = new byte[height][];
            for (var i = 0; i < height; i++)
                bytes[i] = new byte[width];
            this.width = width;
            this.height = height;
        }


        public int Height
        {
            get { return height; }
        }


        public int Width
        {
            get { return width; }
        }


        public int this[int x, int y]
        {
            get { return bytes[y][x]; }
            set { bytes[y][x] = (byte)value; }
        }

 
        public byte[][] Array
        {
            get { return bytes; }
        }


        public void set(int x, int y, byte value)
        {
            bytes[y][x] = value;
        }


        public void set(int x, int y, bool value)
        {
            bytes[y][x] = (byte)(value ? 1 : 0);
        }


        public void clear(byte value)
        {
            for (int y = 0; y < height; ++y)
            {
                var bytesY = bytes[y];
                for (int x = 0; x < width; ++x)
                {
                    bytesY[x] = value;
                }
            }
        }


        override public String ToString()
        {
            var result = new StringBuilder(2 * width * height + 2);
            for (int y = 0; y < height; ++y)
            {
                var bytesY = bytes[y];
                for (int x = 0; x < width; ++x)
                {
                    switch (bytesY[x])
                    {
                        case 0:
                            result.Append(" 0");
                            break;
                        case 1:
                            result.Append(" 1");
                            break;
                        default:
                            result.Append("  ");
                            break;
                    }
                }
                result.Append('\n');
            }
            return result.ToString();
        }
    }
}
