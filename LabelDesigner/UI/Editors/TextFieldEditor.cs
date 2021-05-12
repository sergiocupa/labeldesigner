

using Label;
using Label.Model;
using LabelDesigner.UI.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace LabelDesigner.UI.Editors
{
    public partial class TextFieldEditor : Form
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
            _Field.Value = fdContent.Content;

            int fs = 10;
            int.TryParse(lxFonteTamanho.Content, out fs);
            _Field.Font.Size = fs;
            _Field.Font.FamilyName = txFonte.Content;

            _Field.Position.BottomLeftJustification  = cxRefButtonLeft.Checked;
            _Field.Material.FillReverse              = cxReverse.Checked;
            _Field.Format                            = lxFormat.Content;

            UpdaterFieldUI.BasicElementData(_Field, lxPosicaoX, lxPosicaoY, lxTamanhoLargura, lxTamanhoAltura, null);

            var otipo = fdModo.GetSelectedItem();
            if(otipo != null)
            {
                var lp = (LabelParam)otipo;
                _Field.ElementType = (LabelElementType)lp.Data;
            }

            if (DataField != null)
            {
                _Field.ContentType = LabelContentType.DATASOURCE;
            }
            else
            {
                _Field.ContentType = LabelContentType.PRESET_TEXT;
            }

            var rotate = cxRotacao.GetSelectedItem();
            if(rotate != null)
            {
                var lp = (LabelParam)rotate;
                _Field.RotateQuadrant = (LabelRotateQuadrant)lp.Data;
            }
        }

        private void Popula()
        {
            if(_Field != null)
            {
                if (_Field.ContentType == LabelContentType.DATASOURCE)
                {
                    DataField = new PrintableField();
                }

                fdContent.Content = _Field.Value != null ? _Field.Value.ToString() : "";

                if (_Field.Font.Size <= 0) _Field.Font.Size = 10;
                if (string.IsNullOrEmpty(_Field.Font.FamilyName)) _Field.Font.FamilyName = "0";

                txFonte.Content        = _Field.Font.FamilyName;
                lxFonteTamanho.Content = _Field.Font.Size.ToString();

                fdName.Content           = _Field.Name;

                cxRefButtonLeft.Checked  = _Field.Position.BottomLeftJustification;
                lxPosicaoX.Content       = _Field.Position.X.ToString();
                lxPosicaoY.Content       = _Field.Position.Y.ToString();
                lxTamanhoAltura.Content  = _Field.Size.Height.ToString();
                lxTamanhoLargura.Content = _Field.Size.Width.ToString();
                cxReverse.Checked        = _Field.Material.FillReverse;
                lxFormat.Content         = _Field.Format;

                // Tipo Campo
                var ConteudoTipos = LabelParam.ObterPorEnum(typeof(LabelElementType));
                var e1 = ConteudoTipos.Where(w => (LabelElementType)w.Data == LabelElementType.PRINTER_FONT).First();
                var e2 = ConteudoTipos.Where(w => (LabelElementType)w.Data == LabelElementType.RENDERED_TEXT).First();
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
            else
            {
                fdContent.Content = "";
                txFonte.Content = "";
                lxFonteTamanho.Content = "";
                fdName.Content    = "";

                fdModo.DataSource    = null;

                lxPosicaoX.Content      = "";
                lxPosicaoY.Content      = "";
                lxTamanhoAltura.Content = "";
                lxTamanhoAltura.Content = "";
            }
        }


        private LabelElement _Field;
        private PrintableField DataField;
        private List<LabelParam> Rotacoes;

        public TextFieldEditor(LabelElement field)
        {
            InitializeComponent();

            _Field = field;
            if(_Field == null) throw new Exception("Campo inválido");

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
