

using Label.Model;
using Label.Renderers;
using Label.Renderers.Print.Zpl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace LabelDesigner.UI.Editors
{
    public partial class EditorImagem : Form
    {


        private Bitmap Render(bool OnOpen)
        {
            if (!string.IsNullOrEmpty(Imagem_Caminho))
            {
                int w = 0, h = 0;
                if (!OnOpen)
                {
                    w = Largura; h = Altura;
                }

                Edicao = ImageAssembler.ImageToCompressedBase64(Imagem_Arquivo, Imagem_Caminho, w, h, Rotate, false, Proporcao, Threshold);

                var wo = (int)((double)Edicao.Original.Width * ZoomOriginal);
                var ho = (int)((double)Edicao.Original.Height * ZoomOriginal);
                Bitmap resized = new Bitmap(Edicao.Original, new Size(wo, ho));
                pcOriginal.Image = resized;

                var we = (int)((double)Edicao.Image.Width * ZoomEditado);
                var he = (int)((double)Edicao.Image.Height * ZoomEditado);
                Bitmap resized2 = new Bitmap(Edicao.Image, new Size(we, he));
                pcResultado.Image = resized2;
                return Edicao.Original;
            }
            return null;
        }


        public RenderedGraphic Edicao { get; private set; }

        private bool LiberarSelecao;
        private List<LabelParam> Rotacoes;
        private string File;
        private string Content;
        private string Imagem_Caminho;
        private string Imagem_Arquivo;
        private int Rotate;
        private int Largura;
        private int Altura;
        private int Threshold;
        private double ZoomOriginal;
        private double ZoomEditado;
        private string ID;
        private int Proporcao;

        public EditorImagem(string id)
        {
            InitializeComponent();

            ID           = id;
            Rotate       = 0;
            ZoomOriginal = 1;
            ZoomEditado  = 1;
            Proporcao    = 1;
            Threshold    = taThreshold.Value;
            Rotacoes     = LabelParam.ObterPorEnum(typeof(LabelRotateQuadrant));

            cxRotacao.ValueMember   = "ID";
            cxRotacao.DisplayMember = "Description";
            cxRotacao.DataSource    = Rotacoes;
        }

        private void EditorImagem_Shown(object sender, EventArgs e)
        {
            LiberarSelecao = true;
        }

        private void cxRotacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!LiberarSelecao) return;

            try
            {
                if(cxRotacao.SelectedIndex >= 0)
                {
                    var rotate = (LabelParam)cxRotacao.SelectedItem;
                    Rotate = (int)((LabelRotateQuadrant)rotate.Data);
                    Render(false);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bxSelecaoFonte_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                var result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Imagem_Arquivo = ofd.SafeFileName;
                    Imagem_Caminho = ofd.FileName;

                    var img = Render(true);

                    if(img != null)
                    {
                        txLargura.Text = img.Width.ToString("0");
                        txAltura.Text  = img.Height.ToString("0");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void taOriginalZoom_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (Edicao != null)
                {
                    ZoomOriginal = ((double)taOriginalZoom.Value / 100.0);

                    var wo = (int)((double)Edicao.Original.Width * ZoomOriginal);
                    var ho = (int)((double)Edicao.Original.Height * ZoomOriginal);
                    Bitmap resized = new Bitmap(Edicao.Original, new Size(wo, ho));
                    pcOriginal.Image = resized;
                }
            }
            catch (Exception)
            {
            }
        }

        private void taResultZoom_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (Edicao != null)
                {
                    ZoomEditado = ((double)taResultZoom.Value / 100.0);

                    var we = (int)((double)Edicao.Image.Width * ZoomEditado);
                    var he = (int)((double)Edicao.Image.Height * ZoomEditado);
                    Bitmap resized2 = new Bitmap(Edicao.Image, new Size(we, he));
                    pcResultado.Image = resized2;
                }
            }
            catch (Exception)
            {
            }
        }

        private void txLargura_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode == Keys.Enter)
                {
                    int.TryParse(txLargura.Text, out Largura);
                    int.TryParse(txAltura.Text, out Altura);
                    Render(false);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void txProporcao_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int.TryParse(txProporcao.Text, out Proporcao);

                    if(Proporcao < 1)
                    {
                        Proporcao        = 1;
                        txProporcao.Text = "1";
                    }
                    if(Proporcao > 10)
                    {
                        Proporcao        = 10;
                        txProporcao.Text = "10";
                    }

                    Render(false);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txAltura_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int.TryParse(txLargura.Text, out Largura);
                    int.TryParse(txAltura.Text, out Altura);
                    Render(false);

                }
            }
            catch (Exception ex)
            {
            }
        }



       


        private void taThreshold_Scroll(object sender, EventArgs e)
        {
            try
            {
                Threshold = taThreshold.Value;
                Render(false);
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
            }
            catch (Exception)
            {
            }
        }


    }
}
