

using Label;
using LabelDesigner.UI.Templates;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace LabelDesigner.UI.Editors
{
    public partial class SelecaoCampoValor : Form
    {

        public AlternativeBindingGroupCampoFiltro Selecionado { get; private set; }

        private List<PrintableField> Map;


        public SelecaoCampoValor(List<PrintableField> map, AlternativeBindingGroupCampoFiltro campo)
        {
            InitializeComponent();
            Map = map;
            Selecionado = campo;

            if(Selecionado != null)
            {
                txCampo.Text      = Selecionado.CampoNome;
                txCampoValor.Text = Selecionado.ValorSelecao;
            }
        }

        private void bxOk_Click(object sender, EventArgs e)
        {
            try
            {
                if(Selecionado == null)
                {
                    MessageBox.Show("Não foi selecionado campo"); return;
                }

                if(string.IsNullOrEmpty(txCampoValor.Text))
                {
                    MessageBox.Show("Não foi definido valor para o campo"); return;
                }

                Selecionado.ValorSelecao = txCampoValor.Text;
                DialogResult             = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeletorCampoImprimivelUI sel = new SeletorCampoImprimivelUI(false);
            var result = sel.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var sela = sel.Selecionado;
                Selecionado = new AlternativeBindingGroupCampoFiltro() { CampoNome = sela.Name };
                if (Selecionado != null)
                {
                    txCampo.Text = Selecionado.CampoNome;
                }
                else
                {
                    txCampo.Text = "";
                }
            }
        }
    }
}
