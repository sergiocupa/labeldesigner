

using Label;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;


namespace LabelDesigner.UI.Editors
{
    public partial class EditorFiltro : Form
    {


        public List<AlternativeBindingGroupCampoFiltro> GetCampos()
        {
            return Campos.ToList();
        }

        List<PrintableField> Map;
        BindingList<AlternativeBindingGroupCampoFiltro> Campos;

        public EditorFiltro(List<AlternativeBindingGroupCampoFiltro> campos)
        {
            InitializeComponent();

            if(campos != null)
            {
                Campos = new BindingList<AlternativeBindingGroupCampoFiltro>(campos);
            }
            else
            {
                Campos = new BindingList<AlternativeBindingGroupCampoFiltro>();
            }

            //var map = PrintableFieldAttUtil.ObterCamposImprimiveis<VolumeLinhaProducaoPrint>();
            //Map = map.OrderBy(g => g.Name).ToList();

            //DataGridViewTemplate.EsquemaBrancoLinhaAlternada(dgCampos);
            //dgCampos.Columns.Add(DataGridViewTemplate.CriarColunaTexto("CampoNome", "Nome"));
            //dgCampos.Columns.Add(DataGridViewTemplate.CriarColunaTexto("ValorSelecao", "valor"));
            //dgCampos.DataBindingComplete += (object sender, DataGridViewBindingCompleteEventArgs e) => { dgCampos.ClearSelection(); };
            //dgCampos.DataSource = Campos;

        }

        private void bxAddEtiqueta_Click(object sender, EventArgs e)
        {
            try
            {
                SelecaoCampoValor sel = new SelecaoCampoValor(Map,null);
                var result = sel.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    var campo = sel.Selecionado;
                    if (campo != null)
                    {
                        dgCampos.SuspendLayout();
                        Campos.Add(campo);
                        dgCampos.Refresh();
                        dgCampos.ResumeLayout();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dgCampos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(dgCampos.SelectedRows.Count > 0)
                {
                    var row = (AlternativeBindingGroupCampoFiltro)dgCampos.SelectedRows[0].DataBoundItem;
                    SelecaoCampoValor sel = new SelecaoCampoValor(Map, row);
                    var result = sel.ShowDialog(this);
                    if (result == DialogResult.OK)
                    {
                        var campo = sel.Selecionado;
                        if (campo != null)
                        {
                            var linha = Campos.Where(w => w.CampoNome == campo.CampoNome).FirstOrDefault();
                            if(linha != null)
                            {
                                dgCampos.SuspendLayout();
                                linha.ValorSelecao = campo.ValorSelecao;
                                dgCampos.Refresh();
                                dgCampos.ResumeLayout();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgCampos.SelectedRows.Count > 0)
                {
                    var row = (AlternativeBindingGroupCampoFiltro)dgCampos.SelectedRows[0].DataBoundItem;
                    var linha = Campos.Where(w => w.CampoNome == row.CampoNome).FirstOrDefault();
                    if (linha != null)
                    {
                        dgCampos.SuspendLayout();
                        Campos.Remove(linha);
                        dgCampos.Refresh();
                        dgCampos.ResumeLayout();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btUtilizar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
