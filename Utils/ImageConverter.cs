

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;


namespace Utils
{
    public class ImageConverter
    {


        public static int[,] Base64ListToMatrix(string[] s, int width)
        {
            if (s != null)
            {
                int[,] result = new int[width, s.Length];
                int ibyte = 0; int im; int i = 0; int iw = 0;

                try
                {
                    while (i < s.Length)
                    {
                        var str = Convert.FromBase64String(s[i]);
                        var brow = Decompress(str);
                        iw = 0;
                        im = 0;
                        while (im < width)
                        {
                            ibyte = 0;
                            while ((ibyte < 8) && (iw < width))
                            {
                                result[iw, i] = ((brow[im] >> ibyte) & 1);
                                ibyte++;
                                iw++;
                            }
                            im++;
                        }
                        i++;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                return result;
            }

            return null;
        }



        public static string[] MatrixToBase64List(int[,] m)
        {
            if (m == null || m.Length == 0) return null;

            var LY = m.GetLength(0);
            var LX = m.GetLength(1);
            var result = new string[LY];

            var COLs = LX / 8;
            var COLr = LX % 8;
            if (COLr != 0) COLs++;

            int ix = 0; int iy = 0; int im = 0; int ibyte = 0;
            while (iy < LY)
            {
                var brow = new byte[COLs];
                ix = 0;
                im = 0;
                while ((ix < COLs) && (im < LX))
                {
                    ibyte = 0;
                    while (ibyte < 8)
                    {
                        brow[ix] |= (m[iy, im] > 0) ? (byte)(1 >> ibyte) : (byte)0;
                        ibyte++;
                        im++;
                    }
                    ix++;
                }
                var comp = Compress(brow);
                var b64 = Convert.ToBase64String(comp);
                result[iy] = b64;
                iy++;
            }
            return result;
        }



        public static List<Rectangle> CompressedBase64ToRectangles(string base64)
        {
            List<Rectangle> rectangles = new List<Rectangle>();

            if (!string.IsNullOrEmpty(base64))
            {
                var str   = Convert.FromBase64String(base64);
                var brow  = Decompress(str);
                var raw   = Encoding.UTF8.GetString(brow);
                var lines = raw.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                int x, y, w, h;

                int CNT = lines.Length;
                int ix = 0;
                while(ix < CNT)
                {
                    var parts = lines[ix].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if(parts.Length == 4)
                    {
                        var r = new Rectangle(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
                        rectangles.Add(r);
                    }
                    ix++;
                }
            }
            return rectangles;
        }



        public static string CommandToBase64(string command)
        {
            if(!string.IsNullOrEmpty(command))
            {
                var bytes = Encoding.UTF8.GetBytes(command);
                var compressed = Compress(bytes);
                var base64 = Convert.ToBase64String(compressed);
                return base64;
            }
            return "";
        }


        public static string RectanglesToCompressedBase64(List<Rectangle> rectangles)
        {
            if(rectangles != null && rectangles.Count > 0)
            {
                var dstring = string.Join("|", rectangles.Select(s => s.X + ";" + s.Y + ";" + s.Width + ";" + s.Height));

                var bytes      = Encoding.UTF8.GetBytes(dstring);
                var compressed = Compress(bytes);
                var base64     = Convert.ToBase64String(compressed);
                return base64;
            }
            return "";
        }

        

        public static List<Rectangle> MatrixCompressRectangles(int[,] matrix)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            int start_x1 = -1; int stop_x1 = -1; int start_first = -1; int start_y1 = -1;
            int x = 0; int y = 0;
            int CY = matrix.GetLength(0);
            int CX = matrix.GetLength(1);
            var copy = Copy(matrix);

            Action<int[,]> add = (int[,] _matrix) =>
            {
                x = stop_x1 + 1;
                var r = new Rectangle(start_x1, start_y1, (stop_x1 - start_x1 + 1), (y - start_y1));
                Set(_matrix, r, 1);
                rectangles.Add(r);
                y = start_y1;
                start_x1 = -1; start_y1 = -1; stop_x1 = -1;
            };
            Func<int, bool> endLine = (int count_y) =>
            {
                start_first = start_x1;
                x = start_x1;

                if ((y + 1) < count_y)
                {
                    y++;
                    return false;
                }
                else return true;
            };

            while (y < CY)
            {
                x = 0;
                while (x < CX)
                {
                    int pix = copy[y, x];

                    if (pix == 0)
                    {
                        if (start_x1 < 0)// Inicio coluna e linha
                        {
                            start_x1 = x;
                            start_y1 = y;
                        }
                        else
                        {
                            if (stop_x1 > 0)// segunca linha em diante
                            {
                                if (x >= stop_x1)
                                {
                                    var end = endLine(CY);
                                    if (end)
                                    {
                                        y++;
                                        int temp_y = y;
                                        add(copy);

                                        if (temp_y >= CY && x >= CX)
                                        {
                                            y = temp_y;
                                            break;
                                        }
                                        y--;
                                    }
                                    continue;
                                }
                            }
                            if ((x + 1) >= CX)
                            {
                                stop_x1 = x;
                                endLine(CY);
                                continue;
                            }
                        }
                    }
                    else
                    {
                        if (start_x1 >= 0)
                        {
                            if (start_first >= 0)// Testa se deslocado, entao add rectangle
                            {
                                start_first = -1;
                                if (x == start_x1)
                                {
                                    add(copy);
                                    continue;
                                }
                            }

                            if (stop_x1 >= 0)
                            {
                                if (x >= stop_x1)
                                {
                                    add(copy);
                                    continue;
                                }
                            }
                            else
                            {
                                stop_x1 = x - 1;
                                endLine(CY);
                                continue;
                            }
                        }
                    }
                    x++;
                }
                y++;
            }
            return rectangles;
        }


        private static byte[] Compress(byte[] data)
        {
            byte[] decompressedArray = null;
            try
            {
                using (var stream = new MemoryStream(data))
                {
                    using (var deco = new MemoryStream())
                    {
                        using (var compressor = new DeflateStream(deco, CompressionMode.Compress))
                        {
                            stream.CopyTo(compressor);
                        }

                        decompressedArray = deco.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                // do something !
            }

            return decompressedArray;
        }


        private static byte[] Decompress(byte[] data)
        {
            byte[] decompressedArray = null;
            try
            {
                using (MemoryStream decompressedStream = new MemoryStream())
                {
                    using (MemoryStream compressStream = new MemoryStream(data))
                    {
                        using (DeflateStream deflateStream = new DeflateStream(compressStream, CompressionMode.Decompress))
                        {
                            deflateStream.CopyTo(decompressedStream);
                        }
                    }
                    decompressedArray = decompressedStream.ToArray();
                }
            }
            catch (Exception ex)
            {

            }

            return decompressedArray;
        }



        private static int[,] Copy(int[,] m)
        {
            int CY = m.GetLength(0);
            int CX = m.GetLength(1);
            var copy = new int[CY, CX];
            int x = 0; int y = 0;

            try
            {
                while (y < CY)
                {
                    x = 0;
                    while (x < CX)
                    {
                        copy[y, x] = m[y, x];
                        x++;
                    }
                    y++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return copy;
        }



        public static void Set(int[,] matrix, Rectangle rec, int valor)
        {
            int W = rec.Width + rec.X; int H = rec.Height + rec.Y;
            int x; int y = rec.Y;
            while (y < H)
            {
                x = rec.X;
                while (x < W)
                {
                    matrix[y, x] = valor;
                    x++;
                }
                y++;
            }
        }


        public static List<Rectangle> CompressTest()
        {
            int[,] m = new int[,]
            {
                { 1,1,0,0,0,1 },
                { 1,1,0,0,0,1 },
                { 1,1,0,0,0,1 },
                { 1,1,1,0,0,1 },
                { 1,1,1,0,0,1 },
                { 1,1,1,1,1,1 }
            };
            int[,] m2 = new int[,]
            {
                { 1,1,0,0,0,1 },
                { 1,1,0,0,0,1 },
                { 1,1,0,0,1,1 },
                { 1,0,0,0,1,1 },
                { 1,0,1,1,1,1 },
                { 1,1,1,1,1,1 }
            };
            int[,] m3 = new int[,]
            {
                { 0,0,1,1,0,0 },
                { 0,0,1,1,0,0 },
                { 1,1,0,0,1,1 },
                { 1,1,0,0,1,1 },
                { 0,0,1,1,0,0 },
                { 0,0,1,1,0,0 }
            };

            var recs = MatrixCompressRectangles(m);
            return recs;
        }

    }
}
