

using System.Drawing;


namespace Label.Renderers
{
    public class BitmapRenderer
    {
        private static Color Foreground = Color.Black;
        private static Color Background = Color.White;
        private static Brush Black      = Brushes.Black;


        //public static Bitmap InsertTextContent(string content, Bitmap barcode, int width, int heigth, int pad, Font font, bool aboveBarcode)
        //{
        //    var size = Graphics.FromImage(new Bitmap(1, 1)).MeasureString(content, font);

        //    int barcode_x = 0;
        //    int barcode_y = 0;

        //    int text_x    = (width / 2) - (int)(size.Width / 2);
        //    int text_y    = 0;

        //    int total_h = heigth + (int)size.Height + pad;

        //    if(aboveBarcode) barcode_y = (int)size.Height + pad;
        //                else text_y    = heigth + pad;


        //    var bitmap = new Bitmap(width, total_h);
        //    using(var gr = Graphics.FromImage(bitmap))
        //    {
        //        gr.Clear(Background);
        //        gr.DrawImage(barcode, barcode_x, barcode_y);
        //        gr.DrawString(content, font, Black, text_x, text_y);
        //    }

        //    return bitmap;
        //}



        //public static Bitmap RenderUnidirectionalType(BitMatrix matrix, int _width, int _height)
        //{
        //    var width  = matrix.Width;
        //    var height = matrix.Height;
        //    if (_width > width)   width  = _width;
        //    if (_height > height) height = _height;

        //    var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

        //    // calculating the scaling factor
        //    var pixelsizeWidth  = width / matrix.Width;
        //    var pixelsizeHeight = height / matrix.Height;

        //    var bitmap = Render(matrix, width, height, pixelsizeWidth, pixelsizeHeight);
        //    return bitmap;
        //}



        //public static Bitmap RenderBiType(BitMatrix matrix, int _width, int _height)
        //{
        //    var width = matrix.Width;
        //    var height = matrix.Height;
        //    if (_width > width) width = _width;
        //    if (_height > height) height = _height;

        //    var pixelsizeWidth = width / matrix.Width;
        //    var pixelsizeHeight = height / matrix.Height;

        //    if (pixelsizeWidth != pixelsizeHeight)
        //    {
        //        pixelsizeHeight = pixelsizeWidth = pixelsizeHeight < pixelsizeWidth ? pixelsizeHeight : pixelsizeWidth;
        //    }

        //    var bitmap = Render(matrix, width, height, pixelsizeWidth, pixelsizeHeight);
        //    return bitmap;
        //}


        //private static Bitmap Render(BitMatrix matrix, int width, int height, int pixelsizeWidth, int pixelsizeHeight)
        //{
        //    var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

        //    var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
        //    try
        //    {
        //        var pixels = new byte[bmpData.Stride * height];
        //        var padding = bmpData.Stride - (4 * width);
        //        var index = 0;
        //        var color = Background;

        //        // going through the lines of the matrix
        //        for (int y = 0; y < matrix.Height; y++)
        //        {
        //            // stretching the line by the scaling factor
        //            for (var pixelsizeHeightProcessed = 0; pixelsizeHeightProcessed < pixelsizeHeight; pixelsizeHeightProcessed++)
        //            {
        //                // going through the columns of the current line
        //                for (var x = 0; x < matrix.Width; x++)
        //                {
        //                    color = matrix[x, y] ? Foreground : Background;
        //                    // stretching the columns by the scaling factor
        //                    for (var pixelsizeWidthProcessed = 0; pixelsizeWidthProcessed < pixelsizeWidth; pixelsizeWidthProcessed++)
        //                    {
        //                        pixels[index++] = color.B;
        //                        pixels[index++] = color.G;
        //                        pixels[index++] = color.R;
        //                        pixels[index++] = color.A;
        //                    }
        //                }
        //                // fill up to the right if the barcode doesn't fully fit in 
        //                for (var x = pixelsizeWidth * matrix.Width; x < width; x++)
        //                {
        //                    pixels[index++] = Background.B;
        //                    pixels[index++] = Background.G;
        //                    pixels[index++] = Background.R;
        //                    pixels[index++] = Background.A;
        //                }
        //                index += padding;
        //            }
        //        }
        //        // fill up to the bottom if the barcode doesn't fully fit in 
        //        for (var y = pixelsizeHeight * matrix.Height; y < height; y++)
        //        {
        //            for (var x = 0; x < width; x++)
        //            {
        //                pixels[index++] = Background.B;
        //                pixels[index++] = Background.G;
        //                pixels[index++] = Background.R;
        //                pixels[index++] = Background.A;
        //            }
        //            index += padding;
        //        }

        //        //Copy the data from the byte array into BitmapData.Scan0
        //        Marshal.Copy(pixels, 0, bmpData.Scan0, pixels.Length);
        //    }
        //    finally
        //    {
        //        //Unlock the pixels
        //        bmp.UnlockBits(bmpData);
        //    }
        //    return bmp;
        //}


    }
}
