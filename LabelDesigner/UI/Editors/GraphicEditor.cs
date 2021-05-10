

using Label;
using System;
using System.Windows.Forms;


namespace LabelDesigner.UI.Editors
{
    public partial class GraphicEditor : Form
    {



        public void UpdateElementData()
        {
            _Field.Position.BottomLeftJustification = cxRefButtonLeft.Checked;

            UpdaterFieldUI.BasicElementData(_Field, lxPosicaoX, lxPosicaoY, lxTamanhoLargura, lxTamanhoAltura, lxEspessura);
        }

        private void Popula()
        {
            cxRefButtonLeft.Checked  = _Field.Position.BottomLeftJustification;
            lxPosicaoX.Content       = _Field.Position.X.ToString();
            lxPosicaoY.Content       = _Field.Position.Y.ToString();
            lxTamanhoLargura.Content = _Field.Size.Width.ToString();
            lxTamanhoAltura.Content  = _Field.Size.Height.ToString();
            lxEspessura.Content      = _Field.Material.Thickness.ToString();
        }



        private LabelElement _Field;

        public GraphicEditor(LabelElement field)
        {
            InitializeComponent();

            _Field = field;
            if (_Field == null) throw new Exception("Campo inválido");

            Popula();
        }

        private void bxOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
