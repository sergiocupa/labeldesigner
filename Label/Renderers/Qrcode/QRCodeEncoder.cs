

using Label.Renderers.Base;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace Label.Renderers.Qrcode
{
    public class QRCodeEncoder : IEncoder
    {




        public EncoderResult Render(string contents)
        {
            if (string.IsNullOrEmpty(contents))
            {
                throw new ArgumentException("Found empty contents");
            }

            if (Width < 0 || Height < 0)
            {
                throw new ArgumentException("Requested dimensions are too small: " + Width + 'x' + Height);
            }

            var errorCorrectionLevel = ErrorCorrectionLevel.L;
           
            var code  = Encoder.encode(contents, errorCorrectionLevel, null);
            var rects = RenderRectangles(code, Thickness, Width, Height, Zoom);

            EncoderResult sa = new EncoderResult()
            {
                Rectangles = rects.Rectangles,
                Width = rects.Width,
                Height = rects.Height
            };
            return sa;
        }



        private static Resultrec RenderRectangles(QRCodeData code, int thickness, int width, int height, double zoom)
        {
            Resultrec saida = new Resultrec();
            var input = code.Matrix;
            if (input == null)
            {
                throw new InvalidOperationException();
            }
            int outputWidth  = Math.Max(width, input.Width);
            int outputHeight = Math.Max(height, input.Height);

            int multiple = (thickness >= 1) ? thickness : Math.Min(outputWidth / input.Width, outputHeight / input.Height);

            int HEIGHT_T = input.Height * multiple;
            int WIDTH_T  = input.Width * multiple;

            int outputY = 0; int outputX = 0; int ix = 0; int iy = 0;
            while(outputY < HEIGHT_T)
            {
                ix = 0;
                outputX = 0;
                while (outputX < WIDTH_T)
                {
                    if (input[ix, iy] == 1)
                    {
                        var x_z = outputX * zoom;
                        var y_z = outputY * zoom;
                        var m_z = multiple * zoom;

                        var r = new Rectangle((int)x_z, (int)y_z, (int)m_z, (int)m_z);
                        saida.Rectangles.Add(r);
                        saida.Width  = (int)(x_z + m_z);
                        saida.Height = (int)(y_z + m_z);
                    }
                    ix++;
                    outputX += multiple;
                }
                iy++;
                outputY += multiple;
            }

            return saida;
        }



        class Resultrec
        {
            public List<Rectangle> Rectangles { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            internal Resultrec()
            {
                Rectangles = new List<Rectangle>();
            }
        }


        public int Thickness { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool RenderContent { get; set; }
        public bool RenderContentAboveBarcode { get; set; }
        public Font RenderContentFont { get; set; }
        public double Zoom { get; set; }



        public QRCodeEncoder(int thickness, int width, int height, double zoom = 1, Font renderContentFont = null, bool renderContent = true, bool renderContentAboveBarcode = false)
        {
            Zoom = zoom;
            Thickness = thickness;
            Width = width;
            Height = height;
            RenderContent = renderContent;
            RenderContentAboveBarcode = renderContentAboveBarcode;
            RenderContentFont = renderContentFont;

            if(renderContent && renderContentFont == null)
            {
                RenderContentFont = new Font(FontFamily.GenericSansSerif, 10);
            }
        }
    }
}
