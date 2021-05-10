

using System;
using System.Collections.Generic;
using System.Drawing;


namespace Label.Renderers.Code39
{



    public sealed class Code39Encoder : IEncoder
    {
        public EncoderResult Render(string contents)
        {
            List<Rectangle> saida = new List<Rectangle>();
            string code = ValidarNormalizar(contents);

            int totalWidth = 0;
            int HZ = (int)((double)Height * Zoom);

            foreach (char c in code)
            {
                var bar = CreateBar(c, ref totalWidth);
                foreach (var b in bar)
                {
                    if(b.On)
                    {
                        var w_z = b.Width * Zoom;
                        var x_z = b.X * Zoom;

                        Rectangle r = new Rectangle((int)x_z, 0, (int)w_z, HZ);
                        saida.Add(r);
                    }
                }
            }

            EncoderResult rs = new EncoderResult();
            rs.Rectangles = saida;
            rs.Width      = (int)((double)totalWidth * Zoom);
            rs.Height     = HZ;
            return rs;
        }


        public List<Bar> CreateBar(char digit, ref int X)
        {
            List<Bar> bars = new List<Bar>();
            var encode = BarCode39Pattern.Digits[digit];

            int CNT = encode.Length;
            int i = 0;
            while (i < CNT)
            {
                Bar bar = new Bar();
                char ativ = encode[i];
                var mult = BarCode39Pattern.GetMultiplier(ativ, BAR_WIDTH);

                if (ativ == 'N' || ativ == 'W')
                {
                    bar.On = true;
                }
                else if (ativ == 'n' || ativ == 'w')
                {
                    bar.On = false;
                }

                var W = (BAR_WIDTH * mult);

                bar.X = X;
                bar.Width = W;
                bar.Height = Height;
                X += W;
                i++;
                bars.Add(bar);
            }

            bars.Add(new Bar() { White = true, X = X, Width = BAR_WIDTH, Height = Height });
            X += BAR_WIDTH;
            return bars;
        }



        private string ValidarNormalizar(string code)
        {
            string cods = code;

            foreach (char c in code)
            {
                if (!BarCode39Pattern.Digits.ContainsKey(c)) throw new ArgumentException("Invalid character encountered in specified code.");
            }

            if (!cods.StartsWith("*")) cods = "*" + cods;
            if (!cods.EndsWith("*")) cods = cods + "*";
            return cods;
        }


        public int NarrowWidth { get { return BAR_WIDTH; } set { BAR_WIDTH = value; } }
        public int Height { get; set; }
        public bool RenderContent { get; set; }
        public bool RenderContentAboveBarcode { get; set; }
        public Font RenderContentFont { get; set; }
        public int GapBetweenTextAndCode { get; set; }
        public double Zoom { get; set; }

        private int BAR_WIDTH = 2;


        public Code39Encoder
        (
            int narrowWidth, 
            int height,
            Font renderContentFont = null, 
            int gapBetweenTextAndCode = 1, 
            bool renderContent = true,
            bool renderContentAboveBarcode = false,
            double zoom = 1
        )
        {
            Zoom                      = zoom;
            BAR_WIDTH                 = narrowWidth;
            Height                    = height;
            RenderContent             = renderContent;
            RenderContentAboveBarcode = renderContentAboveBarcode;
            RenderContentFont         = renderContentFont;
            GapBetweenTextAndCode     = gapBetweenTextAndCode;

            if (BAR_WIDTH < 1) BAR_WIDTH = 1;

            if (renderContent && renderContentFont == null)
            {
                RenderContentFont = new Font(FontFamily.GenericSansSerif, 10);
            }
        }
    }



    public class Bar
    {
        public int X { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool On { get; set; }
        public bool White { get; set; }
    }
}
