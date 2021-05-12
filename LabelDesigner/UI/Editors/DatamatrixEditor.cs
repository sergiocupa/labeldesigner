

using Label;
using Label.Model;
using LabelDesigner.UI.Templates;
using System;
using System.Windows.Forms;


namespace LabelDesigner.UI.Editors
{
    public partial class DatamatrixEditor : Form
    {

        private void SourceSelect()
        {
            try
            {
                SeletorCampoImprimivelUI sel = new SeletorCampoImprimivelUI(false);
                var r = sel.ShowDialog();
                if (r == DialogResult.OK)
                {
                    DataField = sel.Selecionado;

                    if (DataField != null)
                    {
                        _Field.ContentType = LabelContentType.DATASOURCE;
                        _Field.Value = DataField.Name;
                        fdContent.Content = "##" + DataField.Name + "##";
                    }
                    else
                    {
                        _Field.ContentType = LabelContentType.PRESET_TEXT;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateElementData()
        {
            _Field.Value   = fdContent.Content;
            _Field.Position.BottomLeftJustification = cxRefButtonLeft.Checked;

            UpdaterFieldUI.BasicElementData(_Field, lxPosicaoX, lxPosicaoY, lxTamanhoLargura, lxTamanhoAltura, lxEspessura);
        }

        private void Popula()
        {
            fdContent.Content = _Field.Value != null ? _Field.Value.ToString() : "";

            cxRefButtonLeft.Checked  = _Field.Position.BottomLeftJustification;
            lxPosicaoX.Content       = _Field.Position.X.ToString();
            lxPosicaoY.Content       = _Field.Position.Y.ToString();
            lxTamanhoLargura.Content = _Field.Size.Width.ToString();
            lxTamanhoAltura.Content  = _Field.Size.Height.ToString();
            lxEspessura.Content      = _Field.Material.Thickness.ToString();
        }


        private LabelElement   _Field;
        private PrintableField DataField;

        public DatamatrixEditor(LabelElement field)
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

        private void fdContent_Click(object sender, EventArgs e)
        {
            SourceSelect();
        }
    }
}
