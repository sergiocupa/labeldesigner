

using Label;
using Label.Renderers;
using Label.Renderers.Print.Zpl;
using LabelDesigner.UI.Fields.Base;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace LabelDesigner.UI.Fields
{
    public class TextField : DraggableControl
    {


        

        private void _Draw()
        {
            SuspendLayout();
            Defaults();

            BackColor = Element.Material.FillReverse ? Color.Gray : Color.Black;

            if (gPath == null) gPath = new GraphicsPath();
                          else gPath.Reset();

            ContentFont = Renderer.GetPrinterFont(Element.Font.FamilyName, Element.Font.Size);
            var f_z = ContentFont.Size * Element.Home.ViewZoom;

            ResizeText();

            gPath.AddString(Element.Value.ToString(), ContentFont.FontFamily, (int)ContentFont.Style, (float)f_z, new Point(0, 0), Format);

            Rotacionar(gPath);

            Region = new Region(gPath);
            ResumeLayout();
        }


        private void Limites()
        {
            Rectangle r_top = new Rectangle(0, 0, 2, 2);
            Rectangle r_rig = new Rectangle((Width - 2), 0, 2, 2);
            Rectangle r_bot = new Rectangle((Width - 2), (Height - 2), 2, 2);
            Rectangle r_lef = new Rectangle(0, (Height - 2), 2, 2);
            gPath.AddRectangle(r_top);
            gPath.AddRectangle(r_rig);
            gPath.AddRectangle(r_bot);
            gPath.AddRectangle(r_lef);
        }


        private void ResizeText()
        {
            var f_z = ContentFont.Size * Element.Home.ViewZoom;
            var fz  = new Font(ContentFont.FontFamily, (float)f_z, ContentFont.Style);

            Graphics gr = Graphics.FromImage(new Bitmap(1, 1));
            SizeF    sz = gr.MeasureString(Element.Value.ToString(), fz, new Point(0,0), Format);
            SizeF    so = gr.MeasureString("m", fz, new Point(0, 0), Format);

            SetPrepareParams(sz.Width, sz.Height, so.Width, so.Height);
        }


        private void Defaults()
        {
            if (Element.Home.ViewZoom <= 0) Element.Home.ViewZoom = 1;
            if (Element.Material.Thickness <= 0) Element.Material.Thickness = 2;
            if (Element.Value == null) Element.Value = "";
        }


        public StringFormat ContentFontFormat { get { return Format; } set { Format = value; } }

        
        private GraphicsPath gPath;
        private IGraphicRenderer Renderer;


        public TextField()
        {
            Draw      = _Draw;

            Format = StringFormat.GenericTypographic;

            Renderer = new ZplGraphicRenderer();

            ResetBack();
        }


    }
}
