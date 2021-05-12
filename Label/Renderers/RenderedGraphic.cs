


using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Label.Renderers
{
    public class RenderedGraphic
    {
        public string ImageName { get; set; }
        public string ID { get; set; }
        public string CompressedContent { get; set; }
        public int Magnification { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


        [JsonIgnore]
        public Bitmap Image { get; set; }

        [JsonIgnore]
        public Bitmap Original { get; set; }

        public List<Rectangle> Rectangles { get; set; }

    }

    
}
