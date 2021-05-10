

using LabelDesigner.UI.Fields.Base;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace LabelDesigner.UI.Fields
{
    public class Box : ResizableControl
    {


        private void _Draw()
        {
            SuspendLayout();

            if(gPath != null)
            {
                gPath.Reset();
            }
            else
            {
                gPath = new GraphicsPath();
            }

            if (Element.Home.ViewZoom <= 0) Element.Home.ViewZoom = 1;

            Width  = (int)(Element.Size.Width  * Element.Home.ViewZoom);
            Height = (int)(Element.Size.Height * Element.Home.ViewZoom);
            Left   = (int)((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom);
            Top    = (int)((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom);

            var t_z = (int)(Element.Material.Thickness * Element.Home.ViewZoom);

            Rectangle r_top = new Rectangle(0, 0, Width, t_z);
            Rectangle r_rig = new Rectangle((Width - t_z), 0, t_z, Height);
            Rectangle r_bot = new Rectangle(0, (Height - t_z), Width, t_z);
            Rectangle r_lef = new Rectangle(0, 0, t_z, Height);

            gPath.AddRectangle(r_top);
            gPath.AddRectangle(r_rig);
            gPath.AddRectangle(r_bot);
            gPath.AddRectangle(r_lef);

            gPath.FillMode = FillMode.Winding;

            Region = new Region(gPath);

            ResumeLayout();
        }


        private GraphicsPath gPath;

        public Box()
        {
            Draw      = _Draw;

            ResetBack();
        }

    }

    public enum RESIZE_QUADRANT
    {
        START_BOTTOM_LEFT = 0,
        START_TOP_LEFT = 1,
        START_TOP_RIGHT = 2,
        START_BOTTOM_RIGHT = 3
    }
}
 