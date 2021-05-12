

using System.Windows.Forms;


namespace LabelDesigner.UI.Templates
{
    public partial class LabelText : UserControl
    {



        public string Title { get { return lxTitle.Text; } set { lxTitle.Text = value; } }
        public string Content { get { return txValue.Text; } set { txValue.Text = value; } }

        public LabelText()
        {
            InitializeComponent();
        }
    }

    public delegate void ResizedDelegate(int width, int height);
}
