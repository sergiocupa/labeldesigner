

using System.Drawing;


namespace Label.Renderers.Print.Zpl
{
    public class ZplGraphicRenderer : IGraphicRenderer
    {

        public Font GetPrinterFont(string font_name, double size)
        {
            var sysFont = ZplFonts.ZplFontToSystemFont(font_name, (int)size);
            var tamanho = size * sysFont.HeigthProportion * 0.5;
            var zebraf  = new Font(sysFont.SystemFontName, (float)tamanho);
            return zebraf;
        }


    }


    

}
