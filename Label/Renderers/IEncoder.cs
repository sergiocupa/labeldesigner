

using System.Collections.Generic;
using System.Drawing;


namespace Label.Renderers
{
    public interface IEncoder
    {
        EncoderResult Render(string contents);
    }

    public class EncoderResult
    {
        public List<Rectangle> Rectangles { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public EncoderResult()
        {
            Rectangles = new List<Rectangle>();
        }
    }
        

}
