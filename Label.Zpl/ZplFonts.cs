


using System.Collections.Generic;
using System.Drawing;


namespace Label.Zpl
{
    public class ZplFonts
    {


        public string SystemFontName { get; set; }
        public string IdZplFont { get; set; }
        public bool SempreMaiuscula { get; set; }
        public int TamanhoFixo { get; set; }
        public double WidthZebraToSystem { get; private set; }
        public double HeigthProportion { get; set; }
        public double HeightToWidthRatio { get; set; }
        public double HeigthZebraToSystem { get; private set; }

        public Dictionary<char, ZplFontProportion> ProportionChars { get; set; }

        public static void SetFont()
        {

        }



        public static ZplFonts ZplFontToSystemFont(string name, int height)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "0";
            }

            return Fonts[name];
        }



        // Bahnschrift Condensed
        private static string _DefaultSystemFontName2 = "Bahnschrift Condensed";
        private static string _DefaultSystemFontName1 = "Consolas";


        internal static Dictionary<string, ZplFonts> Fonts;




        static ZplFonts()
        {
            ZplFonts[] codes = new ZplFonts[]
            {
                new ZplFonts()
                {
                    IdZplFont = "0",
                    SystemFontName = "Bahnschrift SemiBold SemiConden",
                    HeigthZebraToSystem = 1.0402,
                    HeigthProportion = 2.0,
                    WidthZebraToSystem = 0.755,
                    ProportionChars = FontProportions.Obter_Zebra0(),
                    HeightToWidthRatio = 1
                },
                new ZplFonts()
                {
                    IdZplFont = "A",
                    SystemFontName = "Courier New",
                    HeigthZebraToSystem = 1,
                    HeigthProportion = 2.8346,
                    WidthZebraToSystem = 2.40,// 1.1250
                    HeightToWidthRatio = 0.5
                },
                new ZplFonts()
                {
                    IdZplFont = "B",
                    SystemFontName = "Courier New",
                    HeigthZebraToSystem = 1,
                    HeigthProportion = 2.8346,
                    WidthZebraToSystem = 1.3058,
                    HeightToWidthRatio = 0.66667
                },
                new ZplFonts(){ IdZplFont = "C", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1.0748},
                new ZplFonts(){ IdZplFont = "D", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1.0748},
                new ZplFonts(){ IdZplFont = "E", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 0.9653},
                new ZplFonts(){ IdZplFont = "F", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 0.9984},
                new ZplFonts(){ IdZplFont = "G", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 0.8549},
                new ZplFonts(){ IdZplFont = "H", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1.3287},
                new ZplFonts(){ IdZplFont = "I", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "J", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "K", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "L", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "M", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "N", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "O", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "P", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "Q", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "R", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "S", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "T", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "U", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "V", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "W", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "X", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "Y", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "Z", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "1", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "2", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "3", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "4", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "5", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "6", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "7", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "8", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
                new ZplFonts(){ IdZplFont = "9", SystemFontName = "Courier New", HeigthZebraToSystem = 1, HeigthProportion = 2.8346, WidthZebraToSystem = 1},
            };

            Fonts = new Dictionary<string, ZplFonts>();

            foreach (var c in codes)
            {
                Fonts.Add(c.IdZplFont, c);
            }
        }

    }

    public class ZplFontProportion
    {

        public static int ObterLarguraProporcional(string content, ZplFonts font, float tamanhoEstimado, double ratio)
        {
            double W = -1;

            if (font != null)
            {
                Font target = new Font(font.SystemFontName, tamanhoEstimado);
                var size = Graphics.FromImage(new Bitmap(1, 1)).MeasureString("a", target);


                if (font.ProportionChars != null)
                {
                    W = 0;
                    int i = 0;
                    int CNT = content.Length;
                    while (i < CNT)
                    {
                        var ch = content[i];
                        if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'B') || (ch >= '0' && ch <= '9'))
                        {
                            var prop = font.ProportionChars[ch];
                            W += (size.Width * prop.Proportion) * (ratio + (tamanhoEstimado / 220.0));
                        }
                        else
                        {
                            W += size.Width * ratio;
                        }
                        i++;
                    }
                }
            }

            return (int)W;
        }


        public char Character { get; set; }
        public double Proportion { get; set; }
    }

    public class FontProportions
    {

        public static Dictionary<char, ZplFontProportion> Obter_Zebra0()
        {
            var saida = new Dictionary<char, ZplFontProportion>();

            ZplFontProportion[] codes = new ZplFontProportion[]
            {
                new ZplFontProportion(){ Character = 'a', Proportion = 1 },
                new ZplFontProportion(){ Character = 'b', Proportion = 1.07893 },
                new ZplFontProportion(){ Character = 'c', Proportion = 0.97328 },
                new ZplFontProportion(){ Character = 'd', Proportion = 1.07893 },
                new ZplFontProportion(){ Character = 'e', Proportion = 1.02610 },
                new ZplFontProportion(){ Character = 'f', Proportion = 0.60534 },
                new ZplFontProportion(){ Character = 'g', Proportion = 1.07893 },
                new ZplFontProportion(){ Character = 'h', Proportion = 1.07893 },
                new ZplFontProportion(){ Character = 'i', Proportion = 0.55252 },
                new ZplFontProportion(){ Character = 'j', Proportion = 0.70665 },
                new ZplFontProportion(){ Character = 'k', Proportion = 0.97328 },
                new ZplFontProportion(){ Character = 'l', Proportion = 0.70665 },
                new ZplFontProportion(){ Character = 'm', Proportion = 1.65755 },
                new ZplFontProportion(){ Character = 'n', Proportion = 1.07893 },
                new ZplFontProportion(){ Character = 'o', Proportion = 1.02610 },
                new ZplFontProportion(){ Character = 'p', Proportion = 1.07893 },
                new ZplFontProportion(){ Character = 'q', Proportion = 1.07893 },
                new ZplFontProportion(){ Character = 'r', Proportion = 0.73648 },
                new ZplFontProportion(){ Character = 's', Proportion = 0.92107 },
                new ZplFontProportion(){ Character = 't', Proportion = 0.60534 },
                new ZplFontProportion(){ Character = 'u', Proportion = 1.07893 },
                new ZplFontProportion(){ Character = 'v', Proportion = 0.97327 },
                new ZplFontProportion(){ Character = 'w', Proportion = 1.44686 },
                new ZplFontProportion(){ Character = 'x', Proportion = 0.97328 },
                new ZplFontProportion(){ Character = 'y', Proportion = 0.97327 },
                new ZplFontProportion(){ Character = 'z', Proportion = 0.84213 },
                new ZplFontProportion(){ Character = ' ', Proportion = 0.63144 },
                new ZplFontProportion(){ Character = 'A', Proportion = 1.21006 },
                new ZplFontProportion(){ Character = 'B', Proportion = 1.21006 },
                new ZplFontProportion(){ Character = 'C', Proportion = 1.15786 },
                new ZplFontProportion(){ Character = 'D', Proportion = 1.28899 },
                new ZplFontProportion(){ Character = 'E', Proportion = 1.07893 },
                new ZplFontProportion(){ Character = 'F', Proportion = 1.07893 },
                new ZplFontProportion(){ Character = 'G', Proportion = 1.28899 },
                new ZplFontProportion(){ Character = 'H', Proportion = 1.31572 },
                new ZplFontProportion(){ Character = 'I', Proportion = 0.60534 },
                new ZplFontProportion(){ Character = 'J', Proportion = 0.97327 },
                new ZplFontProportion(){ Character = 'K', Proportion = 1.21006 },
                new ZplFontProportion(){ Character = 'L', Proportion = 1.02610 },
                new ZplFontProportion(){ Character = 'M', Proportion = 1.65755 },
                new ZplFontProportion(){ Character = 'N', Proportion = 1.31572 },
                new ZplFontProportion(){ Character = 'O', Proportion = 1.23679 },
                new ZplFontProportion(){ Character = 'P', Proportion = 1.21006 },
                new ZplFontProportion(){ Character = 'Q', Proportion = 1.23679 },
                new ZplFontProportion(){ Character = 'R', Proportion = 1.28899 },
                new ZplFontProportion(){ Character = 'S', Proportion = 1.15786 },
                new ZplFontProportion(){ Character = 'T', Proportion = 1.07893 },
                new ZplFontProportion(){ Character = 'U', Proportion = 1.31572 },
                new ZplFontProportion(){ Character = 'V', Proportion = 1.15786 },
                new ZplFontProportion(){ Character = 'W', Proportion = 1.76258 },
                new ZplFontProportion(){ Character = 'X', Proportion = 1.21006 },
                new ZplFontProportion(){ Character = 'Y', Proportion = 1.21006 },
                new ZplFontProportion(){ Character = 'Z', Proportion = 1.07893 },
                new ZplFontProportion(){ Character = '0', Proportion = 1.02610 },
                new ZplFontProportion(){ Character = '1', Proportion = 1.02610 },
                new ZplFontProportion(){ Character = '2', Proportion = 1.02610 },
                new ZplFontProportion(){ Character = '3', Proportion = 1.02610 },
                new ZplFontProportion(){ Character = '4', Proportion = 1.02610 },
                new ZplFontProportion(){ Character = '5', Proportion = 1.02610 },
                new ZplFontProportion(){ Character = '6', Proportion = 1.02610 },
                new ZplFontProportion(){ Character = '7', Proportion = 1.02610 },
                new ZplFontProportion(){ Character = '8', Proportion = 1.02610 },
                new ZplFontProportion(){ Character = '9', Proportion = 1.02610 },
            };

            foreach (var c in codes)
            {
                saida.Add(c.Character, c);
            }

            return saida;
        }

    }
}
