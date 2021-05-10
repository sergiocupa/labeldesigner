

using System.Windows.Forms;


namespace LabelDesigner.UI.Templates
{
    public partial class Area : UserControl
    {



        public string Title { get { return lxTitle.Text; } set { lxTitle.Text = value; } }

        public Area()
        {
            InitializeComponent();
        }
    }
}
