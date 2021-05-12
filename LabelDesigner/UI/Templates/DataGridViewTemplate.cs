

using System.Drawing;
using System.Windows.Forms;


namespace LabelDesigner.UI.Templates
{
    public class DataGridViewTemplate
    {
        public static void EsquemaBrancoLinhaInferior(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            grid.AutoGenerateColumns = false;
            grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.RowHeadersVisible = false;
            grid.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            grid.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            grid.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            grid.GridColor = Color.FromArgb(230, 240, 250);
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular);
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.RowsDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            grid.ReadOnly = true;
        }


        public static void EsquemaBrancoLinhaAlternada(DataGridView grid, bool CabecalhoVisivel = true)
        {
            grid.BackgroundColor = Color.White;// System.Drawing.SystemColors.Control;
            grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            grid.ColumnHeadersVisible = CabecalhoVisivel;
            grid.AutoGenerateColumns = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.RowHeadersVisible = false;
            grid.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            grid.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            grid.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            grid.AdvancedCellBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            grid.DefaultCellStyle.BackColor = Color.White;// System.Drawing.SystemColors.Control;
            grid.DefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular);
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.RowsDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 245, 255);
            grid.ReadOnly = true;
        }


        public static DataGridViewTextBoxColumn CriarColunaTexto(string nomeCampo, string textoCabecalho, int Largura)
        {
            if (Largura > 0)
            {
                return new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = nomeCampo,
                    HeaderText = textoCabecalho,
                    Name = nomeCampo,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    Width = Largura,
                    ReadOnly = true
                };
            }
            else
            {
                return new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = nomeCampo,
                    HeaderText = textoCabecalho,
                    Name = nomeCampo,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    ReadOnly = true
                };
            }
        }
        public static DataGridViewImageColumn CriarColunaImagem(string nomeCampo, string textoCabecalho, int Largura = 0)
        {
            if (Largura > 0)
            {
                return new DataGridViewImageColumn()
                {
                    DataPropertyName = nomeCampo,
                    HeaderText = textoCabecalho,
                    Name = nomeCampo,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    Width = Largura,
                    ReadOnly = true
                };
            }
            else
            {
                return new DataGridViewImageColumn()
                {
                    DataPropertyName = nomeCampo,
                    HeaderText = textoCabecalho,
                    Name = nomeCampo,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    ReadOnly = true
                };
            }
        }
        public static DataGridViewTextBoxColumn CriarColunaTexto(string nomeCampo, string textoCabecalho)
        {
            return new DataGridViewTextBoxColumn()
            {
                DataPropertyName = nomeCampo,
                HeaderText = textoCabecalho,
                Name = nomeCampo,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
        }
        public static DataGridViewCheckBoxColumn CriarColunaSelecao(string nomeCampo, string textoCabecalho, int Largura = 0)
        {
            if (Largura > 0)
            {
                return new DataGridViewCheckBoxColumn()
                {
                    DataPropertyName = nomeCampo,
                    HeaderText = textoCabecalho,
                    Name = nomeCampo,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    Width = Largura,
                    ReadOnly = true
                };
            }
            else
            {
                return new DataGridViewCheckBoxColumn()
                {
                    DataPropertyName = nomeCampo,
                    HeaderText = textoCabecalho,
                    Name = nomeCampo,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    ReadOnly = true
                };
            }

        }
    }
}
