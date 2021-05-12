

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace LabelDesigner.UI.Templates
{
    [DefaultEvent("Click")]
    public partial class Presser : UserControl
    {

        private void lbText_MouseEnter(object sender, EventArgs e)
        {
            if(_EnabledBorder)
            {
                lbText.BackColor = Color.FromArgb(240,240,255);
            }
        }

        private void lbText_MouseLeave(object sender, EventArgs e)
        {
            if (_EnabledBorder)
            {
                lbText.BackColor = Color.White;
            }
        }
        public bool EnabledBorder 
        { 
            get { return _EnabledBorder; } 
            set
            {
                _EnabledBorder = value;
                if(_EnabledBorder)
                {
                    pnText.Padding   = new Padding(1);
                    pnText.BackColor = Color.FromArgb(100, 120, 255);
                }
                else
                {
                    pnText.Padding   = new Padding(0);
                    pnText.BackColor = Color.White;
                }
            } 
        }


        public string Label { get { return lbText.Text; } set { lbText.Text = value; } }
        public Image Image { get { return lbText.Image; } set { lbText.Image = value; } }
        public ContentAlignment ImageAlign { get { return lbText.ImageAlign; } set { lbText.ImageAlign = value; } }


        private void LbText_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }


        private bool _EnabledBorder;

        public Presser()
        {
            InitializeComponent();

            pnText.BackColor = Color.FromArgb(100, 120, 255);

            lbText.Text        = "";
            lbText.Cursor      = Cursors.Hand;
            lbText.ImageAlign  = System.Drawing.ContentAlignment.MiddleCenter;
            lbText.Click      += LbText_Click;
        }

        
    }
}
