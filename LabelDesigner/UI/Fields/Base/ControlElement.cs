


using Label;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace LabelDesigner.UI.Fields.Base
{
    public class ControlElement : Control, IControlElement
    {

        public void ResetBack()
        {
            BackColor = Color.Black;
        }
        public void SelectedBack()
        {
            BackColor = Color.Coral;
        }

        public LabelElement Element { get; set; }
        public Action StartEdit { get; set; }
        public Action Selected { get; set; }
        public Action<LabelPosition> RePosition { get; set; }
        public Action Draw { get; set; }
        public Font ContentFont { get; set; }
        public StringFormat Format { get; set; }
        public ResizedDelegate Resized { get; set; }
        public Artifact Artifact { get; set; }

        public void Rotacionar(GraphicsPath gPath)
        {
            if (Element.RotateQuadrant != LabelRotateQuadrant.NORMAL)
            {
                Matrix m = new Matrix();
                m.Rotate((int)Element.RotateQuadrant);

                if (Element.RotateQuadrant == LabelRotateQuadrant.RETATE_90)
                {
                    m.Translate(0, -Width);
                }
                else if (Element.RotateQuadrant == LabelRotateQuadrant.RETATE_270)
                {
                    m.Translate(-Height, 0);
                }
                else if (Element.RotateQuadrant == LabelRotateQuadrant.RETATE_180)
                {
                    m.Translate(-Width, -Height);
                }
                gPath.Transform(m);
            }
        }


        public void SetPrepareParams(double width, double height, double offset_width, double offset_height)
        {
            var compEspaco = (offset_width * 0.6);

            if (Element.RotateQuadrant == LabelRotateQuadrant.NORMAL || Element.RotateQuadrant == LabelRotateQuadrant.RETATE_180)
            {
                Width  = (int)(width - compEspaco);
                Height = (int)(height * 0.83);
            }
            else
            {
                Width  = (int)(height * 0.83);
                Height = (int)(width - compEspaco);
            }

            if (Element.Position.BottomLeftJustification)
            {
                if (Element.RotateQuadrant == LabelRotateQuadrant.NORMAL)
                {
                    Left = (int)((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom);
                    Top  = (int)((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom) - Height;
                }
                else if (Element.RotateQuadrant == LabelRotateQuadrant.RETATE_180)
                {
                    Left = (int)((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom) - Width;
                    Top  = (int)((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom);
                }
                else if (Element.RotateQuadrant == LabelRotateQuadrant.RETATE_90)
                {
                    Left = (int)(((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom) - (compEspaco / 2));
                    Top  = (int)((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom);
                }
                else if (Element.RotateQuadrant == LabelRotateQuadrant.RETATE_270)
                {
                    Left = (int)(((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom) - Width + (offset_height * 0.2));
                    Top  = (int)(((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom) - Height + (compEspaco / 4));
                }
            }
            else
            {
                Left = (int)((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom);
                Top  = (int)((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom);
            }
        }
    }
}
