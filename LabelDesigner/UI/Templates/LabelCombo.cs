

using System.Windows.Forms;


namespace LabelDesigner.UI.Templates
{
    public partial class LabelCombo : UserControl
    {

        public void SetSelectedItem(object data)
        {
            comboBox1.SelectedItem = data;
            comboBox1.Refresh();
        }

        public object GetSelectedItem()
        {
            if(comboBox1.SelectedIndex >= 0)
            {
                return comboBox1.Items[comboBox1.SelectedIndex];
            }
            return null;
        }

        public string DisplayMember { get { return comboBox1.DisplayMember; } set { comboBox1.DisplayMember = value; } }
        public string ValueMember { get { return comboBox1.ValueMember; } set { comboBox1.ValueMember = value; } }

        public string Title { get { return lxTitle.Text; } set { lxTitle.Text = value; } }

        public object DataSource { get { return comboBox1.DataSource; } set { comboBox1.DataSource = value; } }

        public LabelCombo()
        {
            InitializeComponent();
        }
    }
}
