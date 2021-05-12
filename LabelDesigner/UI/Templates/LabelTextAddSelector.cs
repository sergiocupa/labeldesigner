

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace LabelDesigner.UI.Templates
{
    [DefaultEvent("Click")]
    public partial class LabelTextAddSelector : UserControl
    {


        public string Title { get { return lxTitle.Text; } set { lxTitle.Text = value; } }
        public string Content { get { return txValue.Text; } set { txValue.Text = value; } }

        public event Action ClickAdd;


        public LabelTextAddSelector()
        {
            InitializeComponent();
        }



        private void prSelecionar_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void prAdd_Click(object sender, EventArgs e)
        {
            if(ClickAdd != null)
            {
                ClickAdd();
            }
        }
    }
}
