

using Label;
using System;
using System.Windows.Forms;


namespace LabelDesigner.UI.Fields.Base
{
    public class DraggableControl : ControlElement
    {

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (clicked_pos && e.Button == MouseButtons.Left)
            {
                Parent.SuspendLayout();

                Top  += (e.Y - old_y);
                Left += (e.X - old_x);

                Parent.ResumeLayout();

                if(RePosition != null)
                {
                    RePosition(new LabelPosition() { X = Left, Y = Top });
                }
            }

            base.OnMouseMove(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clicked_pos = true;

                old_y = e.Y;
                old_x = e.X;
            }

            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clicked_pos = false;
            }

            base.OnMouseUp(e);
        }


        private bool clicked_pos;
        private int  old_x;
        private int  old_y;

        public DraggableControl()
        {
            DoubleClick += (object sender, EventArgs e) => { if (StartEdit != null) StartEdit(); };
            Click       += (object sender, EventArgs e) => { if (Selected != null)  Selected(); };
        }

    }
}
