

using System.Windows.Forms;


namespace LabelDesigner.UI.Fields.Base
{
    public class ResizableControl : DraggableControl
    {


        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (clicked_size && e.Button == MouseButtons.Right)
            {
                Parent.SuspendLayout();

                var yy = (e.Y - old_y);
                var xx = (e.X - old_x);

                Height = old_h + yy;
                Width  = old_w + xx;

                if (Resized != null)
                {
                    Resized(Width, Height);
                }
                Parent.ResumeLayout();
            }

            base.OnMouseMove(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                clicked_size = true;

                old_y = e.Y;
                old_x = e.X;
                old_w = Width;
                old_h = Height;

                var m_x = ((double)Width / 2.0);
                var m_y = ((double)Height / 2.0);

                if ((Left <= (e.X + m_x)) && (Top <= (e.Y + m_y)))
                {
                    Quadrant = RESIZE_QUADRANT.START_TOP_RIGHT;
                }
                else
                {
                    // Outros quadrantes...
                }

            }

            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                clicked_size = false;
            }

            base.OnMouseUp(e);
        }


        private bool clicked_size;
        private int  old_x;
        private int  old_y;
        private int  old_w;
        private int  old_h;

        private RESIZE_QUADRANT Quadrant;

        public ResizableControl()
        {
            DoubleBuffered = true;
        }
    }
}
