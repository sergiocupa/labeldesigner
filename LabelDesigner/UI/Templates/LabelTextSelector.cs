

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace LabelDesigner.UI.Templates
{
    [DefaultEvent("Click")]
    public partial class LabelTextSelector : UserControl
    {


        public string Title { get { return lxTitle.Text; } set { lxTitle.Text = value; } }
        public string Content { get { return txValue.Text; } set { txValue.Text = value; } }


        public LabelTextSelector()
        {
            InitializeComponent();
        }

        private void prSelecionar_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }
    }
}
