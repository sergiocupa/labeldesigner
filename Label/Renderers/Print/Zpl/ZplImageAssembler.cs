

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;


namespace Label.Renderers.Print.Zpl
{
    public class ZplImageAssembler
    {


        public static RenderedGraphic ImageToZplField(string imageFile, int width, int height, int rotate, string id, bool inverter, int ampliacao_impressao, int threshold = 128)
        {
            RenderedGraphic result = new RenderedGraphic();

            var original = Image.FromFile(imageFile);
            Bitmap trabalho = null;

            if (width > 0 && height > 0)
            {
                if (original.Width != width || original.Height != height)
                {
                    trabalho = new Bitmap(original, width, height);
                }
                else
                {
                    trabalho = new Bitmap(original);
                }
            }
            else
            {
                trabalho = new Bitmap(original);
            }

            if (rotate > 0)
            {
                if (rotate <= 90)
                {
                    trabalho.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                else if (rotate > 90 && rotate <= 180)
                {
                    trabalho.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else if (rotate > 180)
                {
                    trabalho.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
            }

            var array1bit_output = ToArray1bit(trabalho, inverter, threshold);
            var array1bit_zpl    = ToArray1bit(trabalho, !inverter, threshold);
            var ibit_o           = Array1BitToBitmap(array1bit_output.Data, array1bit_output.Stride, trabalho.Width, trabalho.Height);

            result.Image    = ibit_o;
            result.Original = trabalho;
            result.Matrix   = array1bit_zpl.Matrix;

            string data = ByteArrayToString(array1bit_zpl.Data);
            string LENG = array1bit_zpl.Length.ToString("00000");
            string ROW = array1bit_zpl.Stride.ToString("000");

            if (ampliacao_impressao < 1) ampliacao_impressao = 1;
            if (ampliacao_impressao > 10) ampliacao_impressao = 10;

            StringBuilder sb = new StringBuilder();
            sb.Append("~DG");
            sb.Append(id);
            sb.Append(",");
            sb.Append(LENG);
            sb.Append(",");
            sb.Append(ROW);
            sb.Append(",");
            sb.Append(data);

            result.AmpliacaoImpressao = ampliacao_impressao;
            result.ZplContent = sb.ToString();
            return result;
        }




        private static Bitmap Array1BitToBitmap(byte[] data, int stride, int width, int height)
        {
            var bitmap = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
            BitmapData bmData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int _stride = bmData.Stride;
            IntPtr pNative = bmData.Scan0;

            int r_data = 0;
            for (int y = 0; y < bitmap.Height; y++)
            {
                Marshal.Copy(data, r_data, IntPtr.Add(pNative, y * bmData.Stride), stride);
                r_data += stride;
            }

            bitmap.UnlockBits(bmData);
            return bitmap;
        }




        public static RenderedGraphic TextToZplField(string text, Font font, bool reverce, int rotate, ZplRenderMode mode, string id)
        {
            StringBuilder sb = new StringBuilder();
            RenderedGraphic result = new RenderedGraphic() { ZplCommand = mode };
            result.ID = id;

            var size = Graphics.FromImage(new Bitmap(1, 1)).MeasureString(text, font, new PointF(0, 0), StringFormat.GenericTypographic);

            result.Image = new Bitmap((int)size.Width, (int)size.Height);
            using (Graphics graphic = Graphics.FromImage(result.Image))
            {
                graphic.Clear(Color.White);
                graphic.DrawString(text, font, Brushes.Black, 0, 0, StringFormat.GenericTypographic);
            }

            if (rotate > 0)
            {
                if (rotate <= 90)
                {
                    result.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                else if (rotate > 90 && rotate <= 180)
                {
                    result.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else if (rotate > 180)
                {
                    result.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
            }

            var array1bit = ToArray1bit(result.Image, !reverce);
            string LENG = array1bit.Length.ToString("00000");
            string ROW = array1bit.Stride.ToString("000");

            if (mode == ZplRenderMode.GF_Z64)
            {
                var comp = CompressZb64(array1bit.Data);

                sb.Append("^GFA,");
                sb.Append(LENG);
                sb.Append(",");
                sb.Append(LENG);
                sb.Append(",");
                sb.Append(ROW);
                sb.Append(",:Z64:");
                sb.Append(comp);
                sb.Append(":1234\r\n");
            }
            else if (mode == ZplRenderMode.GF_HEX)
            {
                var comp = Convert.ToBase64String(array1bit.Data);

                sb.Append("^GFA,");
                sb.Append(LENG);
                sb.Append(",");
                sb.Append(LENG);
                sb.Append(",");
                sb.Append(ROW);
                sb.Append(",:B64:");
                sb.Append(comp);
                sb.Append(":1234\r\n");
            }
            else if (mode == ZplRenderMode.DG_HEX)
            {
                var data = ByteArrayToString(array1bit.Data);

                sb.Append("~DG");
                sb.Append(id);
                sb.Append(",");
                sb.Append(LENG);
                sb.Append(",");
                sb.Append(ROW);
                sb.Append(",");
                sb.Append(data);
                sb.Append("\r\n");
            }
            result.ZplContent = sb.ToString();
            return result;
        }

        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:X2}", b);
            return hex.ToString();
        }




        private static ResultadoConversaoImagem ToArray1bit(Bitmap bitmap, bool reverce, int threshold = 128)
        {
            ResultadoConversaoImagem saida = new ResultadoConversaoImagem();
            var result = BitmapToArray(bitmap);

            int BYTES_POR_LINHA = bitmap.Width / 8;
            int RESTO = bitmap.Width % 8;
            int STRIDE_1BIT = bitmap.Width;
            if (RESTO > 0)
            {
                STRIDE_1BIT = bitmap.Width + (8 - RESTO);
                BYTES_POR_LINHA++;
            }
            else
            {
                STRIDE_1BIT = bitmap.Width;
            }

            var matrix = ToMonoColor(result.Data, STRIDE_1BIT, result.Length, bitmap, result.Channels, threshold);

            saida.Stride = STRIDE_1BIT / 8;
            var TAM = (saida.Stride * bitmap.Height);
            //var er = (saida.Stride * bitmap.Height);
            saida.Data   = new byte[TAM];
            saida.Length = saida.Data.Length;
            saida.Matrix = matrix;

            MatrixToBitPerPixel(matrix, STRIDE_1BIT, bitmap.Width, bitmap.Height, saida);

            if (reverce)
            {
                saida.Data = BitwiseBytes(saida.Data);
                return saida;
            }
            else return saida;
        }


        private static void MatrixToBitPerPixel(int[,] matrix, int stride, int width, int height, ResultadoConversaoImagem saida)
        {
            int iy = 0; int i = 0; int shifts = 0; int i_shift = 8; int ix = 0;

            while (iy < height)
            {
                ix = 0;
                while (ix < stride)
                {
                    i_shift--;

                    var valor = (matrix[iy, ix] & 1);

                    shifts |= (valor << i_shift);

                    if (i_shift <= 0)
                    {
                        i_shift = 8;
                        saida.Data[i] = (byte)shifts;
                        shifts = 0;
                        i++;
                    }
                    ix++;
                }
                iy++;
            }
        }


        private static int[,] ToMonoColor(byte[] data, int stride, int leng, Bitmap origin, int channels, int threshold = 128)
        {
            int[,] mat = CriarArray(stride, origin.Height);
            int ix_line = 0;
            int iw = 0;
            int iy = 0;
            while (iy < leng)
            {
                var cz = ObterPretoBranco(data, iy, channels, channels - 1, threshold);
                mat[ix_line, iw] = cz;

                iy += channels;
                iw++;

                if (iw >= origin.Width)
                {
                    iw = 0;
                    ix_line++;
                }
            }
            return mat;
        }


        private static int ObterPretoBranco(byte[] data, int index, int DEPTH, int DEPTH_LENG, int threshold = 128)
        {
            int calc = 0;
            int j = 0;
            while (j < DEPTH)
            {
                if (j < DEPTH_LENG)
                {
                    calc += data[index];
                }
                j++;
            }
            var pb = (((calc / DEPTH) >= threshold) ? 1 : 0);
            return pb;
        }


        private static int[,] CriarArray(int X, int Y)
        {
            int[,] mat = new int[Y, X];
            int iy = 0;
            int ix = 0;
            while (iy < Y)// limpar matriz
            {
                ix = 0;
                while (ix < X)
                {
                    mat[iy, ix] = 1;
                    ix++;
                }
                iy++;
            }
            return mat;
        }



        private static Bitmap ArrayToBitmap(byte[] data, int ch, int stride, int w, int h)
        {
            Bitmap bitmap = new Bitmap(w, h);

            int iy = 0; int ix = 0; int ir = 0;
            while (iy < h)
            {
                ix = 0;
                while (ix < w)
                {
                    var color = data[ir] > 127 ? Color.Red : Color.White;
                    bitmap.SetPixel(ix, iy, color);
                    ir += ch;
                    ix++;
                }
                iy++;
            }
            return bitmap;
        }


        private static ResultadoConversaoImagem BitmapToArray(Bitmap bitmap)
        {
            ResultadoConversaoImagem result = new ResultadoConversaoImagem();

            result.Depth = Bitmap.GetPixelFormatSize(bitmap.PixelFormat);
            result.Channels = result.Depth / 8;

            if (result.Depth != 8 && result.Depth != 24 && result.Depth != 32)
            {
                throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
            }

            BitmapData bmData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int RowSize = bmData.Stride < 0 ? -bmData.Stride : bmData.Stride;
            result.Length = bitmap.Height * RowSize;
            result.Data = new byte[result.Length];

            IntPtr pNative = bmData.Scan0;
            //Marshal.Copy(pNative, data, 0, LENG);

            for (int y = 0; y < bitmap.Height; y++)
            {
                Marshal.Copy(IntPtr.Add(pNative, y * bmData.Stride), result.Data, y * RowSize, RowSize);
            }

            bitmap.UnlockBits(bmData);
            result.Stride = bmData.Stride;
            return result;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] BitwiseBytes(byte[] data)// inverte os bits para criar bitmap com fonto cor preta
        {
            byte[] saida = new byte[data.Length];
            int i = 0;
            while (i < saida.Length)
            {
                saida[i] = (byte)(~data[i]);
                i++;
            }
            return saida;
        }


        private static string CompressZb64(byte[] data)
        {
            var da = Compress(data);
            byte[] dama = new byte[da.Length + 2];

            dama[0] = 120;
            dama[1] = 156;

            Array.Copy(da, 0, dama, 2, da.Length);

            var saida = Convert.ToBase64String(dama);
            return saida;
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
                // do something !
            }

            return decompressedArray;
        }




    }







    public class ResultadoConversaoImagem
    {
        public int Length { get; set; }
        public int Stride { get; set; }
        public int Depth { get; set; }
        public int Channels { get; set; }
        public byte[] Data { get; set; }
        public int[,] Matrix { get; set; }
    }


    


}
