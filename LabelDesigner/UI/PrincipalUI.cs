

using Label;
using LabelDesigner.UI.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace LabelDesigner.UI
{
    public partial class PrincipalUI : Form
    {


        private void prVinculos_Click(object sender, EventArgs e)
        {
            Ctrl.AddVinculacao();
        }


        private void dgElements_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgElements.SelectedRows.Count > 0)
            {
                var data = (Artifact)dgElements.SelectedRows[0].DataBoundItem;
                Ctrl.Select(data);
                Ctrl.AddFieldVinculado(data);
            }
        }
        private void dgElements_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgElements.SelectedRows.Count > 0)
            {
                var data = (Artifact)dgElements.SelectedRows[0].DataBoundItem;
                Ctrl.SelectedEdit(data);
            }
        }



        internal void AtivarEdicao(bool ativo)
        {
            prText.Enabled       = ativo;
            prBox.Enabled        = ativo;
            prBar39.Enabled      = ativo;
            prBar128.Enabled     = ativo;
            prQrCode.Enabled     = ativo;
            prDatamatrix.Enabled = ativo;
            prImage.Enabled      = ativo;
            prSub.Enabled        = ativo;
            presser3.Enabled     = ativo;
        }



        private void labelTextAddSelector1_Click(object sender, EventArgs e)
        {
            Ctrl.SelecionarEtiqueta();
        }

        private void labelTextAddSelector1_ClickAdd()
        {
            Ctrl.AddEtiqueta();
        }

        private void prText_Click(object sender, EventArgs e)
        {
            Ctrl.AddText(null);
        }

        private void prBox_Click(object sender, EventArgs e)
        {
            Ctrl.AddGraphic(null);
        }

        private void prBar39_Click(object sender, EventArgs e)
        {
            Ctrl.AddBarcode39(null);
        }

        private void prBar128_Click(object sender, EventArgs e)
        {
            Ctrl.AddBarcode128(null);
        }

        private void prQrCode_Click(object sender, EventArgs e)
        {
            Ctrl.AddQrcode(null);
        }

        private void prDatamatrix_Click(object sender, EventArgs e)
        {
            Ctrl.AddDatamatrix(null);
        }

        private void prImage_Click(object sender, EventArgs e)
        {
            Ctrl.AddImage(null);
        }
        private void presser3_Click(object sender, EventArgs e)
        {
            Ctrl.AddRenderedText(null);
        }

        private void bxSave_Click(object sender, EventArgs e)
        {
            Ctrl.Save();
        }

        private void prSub_Click(object sender, EventArgs e)
        {
            Ctrl.Delete();
        }

        private void offsetX_KeyUpEvent(KeyEventArgs obj)
        {
            if (obj.KeyCode == Keys.Enter)
            {
                offsetX.Content = offsetX.Content.Replace("\r\n", "");
                Ctrl.PosicaoAlteradaX(offsetX.Content);
            }
        }

        private void offsetY_KeyUpEvent(KeyEventArgs obj)
        {
            if (obj.KeyCode == Keys.Enter)
            {
                offsetY.Content = offsetY.Content.Replace("\r\n", "");
                Ctrl.PosicaoAlteradaY(offsetY.Content);
            }
        }


        private void txZoom_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txZoom.Text = txZoom.Text.Replace("\r\n", "");

                int z = 0;
                if (int.TryParse(txZoom.Text, out z))
                {
                    Ctrl.Zoom(z);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SizeF aSf = new SizeF(2, 1.5f);
                pnContent.Scale(aSf);
            }
            catch (Exception ex)
            {

            }
        }


        internal void AtualizarGrid(List<Artifact> elements)
        {
            dgElements.SuspendLayout();
            BElements.Clear();

            if (elements != null)
            {
                foreach (var item in elements)
                {
                    BElements.Add(item);
                }
            }

            dgElements.Refresh();

            foreach (DataGridViewRow r in dgElements.Rows)
            {
                var data = (Artifact)r.DataBoundItem;
                data.Row = r;
            }
            dgElements.ResumeLayout();
        }

        private void PrepararGrid()
        {
            DataGridViewTemplate.EsquemaBrancoLinhaAlternada(dgElements);
            dgElements.Columns.Add(DataGridViewTemplate.CriarColunaImagem("Icon", "", 25));
            dgElements.Columns.Add(DataGridViewTemplate.CriarColunaTexto("CaptionGrid", "Conteúdo"));
            dgElements.DataBindingComplete += (object sender, DataGridViewBindingCompleteEventArgs e) => { dgElements.ClearSelection(); };
            dgElements.DataSource = BElements;
        }


        private BindingList<Artifact> BElements;
        private Principal Ctrl;


        public PrincipalUI()
        {
            InitializeComponent();

            BElements = new BindingList<Artifact>();
            Ctrl = new Principal(this);

            PrepararGrid();

            DoubleBuffered = true;

            AtivarEdicao(false);
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            Ctrl.ExportZpl();
        }
    }
}
