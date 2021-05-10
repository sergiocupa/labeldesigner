

using System;
using System.Windows.Forms;


namespace LabelDesigner.UI.Templates
{
    public partial class SelecaoNomeValorUI : Form
    {

        public string Nome { get; private set; }
        public string Valor { get; private set; }


        public SelecaoNomeValorUI(string Titulo, string CampoNome_Rotulo, string CampoNome_Valor, string CampoValor_Rotulo, string CampoValor_Valor)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;

            Text = Titulo;

            lxTituloNome.Text = CampoNome_Rotulo;
            txCampoNome.Text = CampoNome_Valor;

            lxTituloValor.Text = CampoValor_Rotulo;
            txCampoValor.Text = CampoValor_Valor;

            Nome  = CampoNome_Valor;
            Valor = CampoValor_Valor;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (txCampoValor.Text.Length > 0)
            {
                Nome  = txCampoNome.Text;
                Valor = txCampoValor.Text;
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void txTag_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(txCampoValor.Text.Length > 0)
                {
                    if (txCampoValor.Text.Length > 0)
                    {
                        Nome  = txCampoNome.Text;
                        Valor = txCampoValor.Text;
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        DialogResult = DialogResult.Cancel;
                    }
                }
            }
        }
    }
}
