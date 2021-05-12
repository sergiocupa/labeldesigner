

using Label.Renderers.Datamatrix;
using LabelDesigner.UI.Fields.Base;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace LabelDesigner.UI.Fields
{
    public class Datamatrix : ResizableControl
    {
        private void _Draw()
        {
            SuspendLayout();

            if (gPath == null)
            {
                gPath = new GraphicsPath();
            }
            else
            {
                gPath.Reset();
            }

            if (Element.Home.ViewZoom <= 0)      Element.Home.ViewZoom      = 1;
            if (Element.Material.Thickness <= 0) Element.Material.Thickness = 2;
            if (Element.Value == null)           Element.Value              = "";

            var enc = new DatamatrixEncoder((int)Element.Material.Thickness, 0, 0, Element.Home.ViewZoom);
            var rects = enc.Render(Element.Value.ToString());

            if (rects.Rectangles.Count > 0)
            {
                Width  = rects.Width;
                Height = rects.Height;

                foreach (var rec in rects.Rectangles)
                {
                    gPath.AddRectangle(rec);
                }
            }

            Left = (int)((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom);
            Top  = (int)((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom);

            Region = new Region(gPath);

            ResumeLayout();
        }


        private GraphicsPath gPath;

        public Datamatrix()
        {
            Draw       = _Draw;

            ResetBack();
        }
    }
}
