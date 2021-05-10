

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace LabelDesigner.UI.Templates
{
    [DefaultEvent("Click")]
    public partial class FontSelector : UserControl
    {

        public Font SelectedFont 
        { 
            get
            {
                return _SelectedFont;
            } 
            set 
            {
                _SelectedFont = value;

                if(_SelectedFont != null)
                {
                    txValue.Text = _SelectedFont.FontFamily.Name + ", " + _SelectedFont.Size + ", " + _SelectedFont.Style;
                }
                else
                {
                    txValue.Text = "";
                }
            }
        }

        public string Title { get { return lxTitle.Text; } set { lxTitle.Text = value; } }
        public string Content { get { return txValue.Text; } set { txValue.Text = value; } }


        private Font _SelectedFont;

        public FontSelector()
        {
            InitializeComponent();
        }

        private void prSelecionar_Click(object sender, EventArgs e)
        {
            try
            {
                var fd = new FontDialog();
                fd.Font = _SelectedFont;
                var d = fd.ShowDialog();
                if(d == DialogResult.OK)
                {
                    SelectedFont = fd.Font;
                }
                OnClick(e);
            }
            catch (Exception)
            {

            }
        }
    }
}
