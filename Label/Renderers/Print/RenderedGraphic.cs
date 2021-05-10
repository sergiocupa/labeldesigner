


using System.Drawing;

namespace Label.Renderers.Print
{
    public class RenderedGraphic
    {
        public string ImageName { get; set; }
        public string ID { get; set; }
        public Bitmap Image { get; set; }
        public Bitmap Original { get; set; }
        public int[,] Matrix { get; set; }
        public string ZplContent { get; set; }
        public ZplRenderMode ZplCommand { get; set; }
        public bool DescarregadoNaImpressora { get; set; }
        public int AmpliacaoImpressao { get; set; }
    }

    public enum ZplRenderMode
    {
        UNKNOWN = 0,
        GF_Z64 = 1,
        GF_HEX = 2,
        DG_HEX = 3
    }
}
