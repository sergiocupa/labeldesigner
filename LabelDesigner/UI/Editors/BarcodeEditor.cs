

using Label;
using Label.Model;
using LabelDesigner.UI.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace LabelDesigner.UI.Editors
{
    public partial class BarcodeEditor : Form
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
            UpdaterFieldUI.BasicElementData(_Field, lxPosicaoX, lxPosicaoY, lxTamanhoLargura, lxTamanhoAltura, lxEspessura);

            _Field.Value = fdContent.Content;
            _Field.UseFNC1 = cxFnc1.Checked;
            _Field.Position.BottomLeftJustification = cxRefButtonLeft.Checked;

            var rotate = cxRotacao.GetSelectedItem();
            if (rotate != null)
            {
                var lp = (LabelParam)rotate;
                _Field.RotateQuadrant = (LabelRotateQuadrant)lp.Data;
            }
        }


        private void Popula()
        {
            fdContent.Content = _Field.Value != null ? _Field.Value.ToString() : "";
            cxFnc1.Checked    = _Field.UseFNC1;

            cxRefButtonLeft.Checked  = _Field.Position.BottomLeftJustification;

            lxPosicaoX.Content       = _Field.Position.X.ToString();
            lxPosicaoY.Content       = _Field.Position.Y.ToString();
            lxTamanhoLargura.Content = _Field.Size.Width.ToString();
            lxTamanhoAltura.Content  = _Field.Size.Height.ToString();
            lxEspessura.Content      = _Field.Material.Thickness.ToString();

            var ConteudoTipos = LabelParam.ObterPorEnum(typeof(LabelElementType));
            var e1 = ConteudoTipos.Where(w => (LabelElementType)w.Data == LabelElementType.CODIGO_BARRA_39).First();
            var e2 = ConteudoTipos.Where(w => (LabelElementType)w.Data == LabelElementType.CODIGO_BARRA_128).First();
            var lista = new List<LabelParam>();
            lista.Add(e1);
            lista.Add(e2);
            fdModo.DataSource = lista;
            fdModo.DisplayMember = "Description";
            fdModo.ValueMember = "Data";

            var item = lista.Where(w => (LabelElementType)w.Data == _Field.ElementType).FirstOrDefault();
            fdModo.SetSelectedItem(item);

            // Rotacao
            Rotacoes = LabelParam.ObterPorEnum(typeof(LabelRotateQuadrant));

            cxRotacao.ValueMember = "ID";
            cxRotacao.DisplayMember = "Description";
            cxRotacao.DataSource = Rotacoes;

            var rot = Rotacoes.Where(w => (LabelRotateQuadrant)w.Data == _Field.RotateQuadrant).FirstOrDefault();
            cxRotacao.SetSelectedItem(rot);
        }


        private List<LabelParam> Rotacoes;
        private LabelElementType LabelType;
        private LabelElement _Field;
        private PrintableField DataField;

        public BarcodeEditor(LabelElement field)
        {
            InitializeComponent();

            LabelType = field.ElementType;

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

        private void cxReverse_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
