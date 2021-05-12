

using LabelDesigner.UI.Fields.Base;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace LabelDesigner.UI.Fields
{
    public class ImageField : ResizableControl
    {


        private void Compress_Draw()
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

            if (Element.Home.ViewZoom <= 0) Element.Home.ViewZoom = 1;

            if (Element.Rectangles != null && Element.Rectangles.Count > 0)
            {
                Width  = (int)(Element.Size.Width * Element.Home.ViewZoom);
                Height = (int)(Element.Size.Height * Element.Home.ViewZoom);
                Left   = (int)((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom);
                Top    = (int)((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom);

                //var recs = ImageConverter.MatrixToCompressRectangles(Element.Matrix);
                var arec = Element.Rectangles.ToArray();
                gPath.AddRectangles(arec);
            }

            Region = new Region(gPath);
            ResumeLayout();
        }





        private void Raw_Draw()
        {
            SuspendLayout();

            //if (gPath == null)
            //{
            //    gPath = new GraphicsPath();
            //}
            //else
            //{
            //    gPath.Reset();
            //}

            //if (Element.Home.ViewZoom <= 0) Element.Home.ViewZoom = 1;

            //int ir = 0;
            //if (Element.Matrix != null)
            //{
            //    int CX = Element.Matrix.GetLength(0);
            //    int CY = Element.Matrix.GetLength(1);

            //    Width  = (int)(Element.Size.Width * Element.Home.ViewZoom);
            //    Height = (int)(Element.Size.Height * Element.Home.ViewZoom);
            //    Left   = (int)((Element.Position.X + Element.Home.X) * Element.Home.ViewZoom);
            //    Top    = (int)((Element.Position.Y + Element.Home.Y) * Element.Home.ViewZoom);

            //    int x = 0; int y = 0;
            //    while(y < CY)
            //    {
            //        x = 0;
            //        while(x < CX)
            //        {
            //            int v = Element.Matrix[x, y];
            //            if(v == 0)
            //            {
            //                var x_z = x * Element.Home.ViewZoom;
            //                var y_z = y * Element.Home.ViewZoom;

            //                var r = new Rectangle((int)x_z, (int)y_z, 1, 1);
            //                gPath.AddRectangle(r);
            //                ir++;
            //            }
            //            x++;
            //        }
            //        y++;
            //    }
            //}

            //Region = new Region(gPath);
            ResumeLayout();
        }




        




        private GraphicsPath gPath;

        public ImageField()
        {
            DoubleBuffered = true;
            Draw       = Compress_Draw;

            ResetBack();
        }


    }
}
