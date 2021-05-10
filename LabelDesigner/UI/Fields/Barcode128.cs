

using Label;
using Label.Renderers.Code128;
using LabelDesigner.UI.Fields.Base;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace LabelDesigner.UI.Fields
{


    public class Barcode128 : ResizableControl
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

            if (Element.Home.ViewZoom <= 0)      Element.Home.ViewZoom = 1;
            if (Element.Material.Thickness <= 0) Element.Material.Thickness = 2;
            if (Element.Value == null)           Element.Value = "";

            var enc = new Code128Encoder
            (
                (int)Element.Material.Thickness,
                (int)Element.Size.Height,
                zoom: Element.Home.ViewZoom, 
                renderContent: false
            );
            var rects = enc.Render(Element.Value.ToString());

            if (rects.Rectangles.Count > 0)
            {
                if (Element.RotateQuadrant == LabelRotateQuadrant.NORMAL || Element.RotateQuadrant == LabelRotateQuadrant.RETATE_180)
                {
                    Width = rects.Width;
                    Height = rects.Height;
                }
                else
                {
                    Width = rects.Height;
                    Height = rects.Width;
                }

                foreach (var rec in rects.Rectangles)
                {
                    gPath.AddRectangle(rec);
                }

                Rotacionar(gPath);


                if (Element.Position.BottomLeftJustification)
                {
                    if (Element.RotateQuadrant == LabelRotateQuadrant.NORMAL)
                    {
                        Left = (int)((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom);
                        Top = (int)((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom) - Height;
                    }
                    else if (Element.RotateQuadrant == LabelRotateQuadrant.RETATE_180)
                    {
                        Left = (int)((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom) - Width;
                        Top = (int)((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom);
                    }
                    else if (Element.RotateQuadrant == LabelRotateQuadrant.RETATE_90)
                    {
                        Left = (int)(((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom));
                        Top = (int)((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom);
                    }
                    else if (Element.RotateQuadrant == LabelRotateQuadrant.RETATE_270)
                    {
                        Left = (int)(((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom) - Width);
                        Top = (int)(((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom) - Height);
                    }
                }
                else
                {
                    Left = (int)((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom);
                    Top = (int)((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom);
                }
            }

            Region = new Region(gPath);
            ResumeLayout();
        }


        private GraphicsPath gPath;


        public Barcode128()
        {
            Draw = _Draw;

            ResetBack();
        }
    }
}
