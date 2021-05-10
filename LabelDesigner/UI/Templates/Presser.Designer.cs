
namespace LabelDesigner.UI.Templates
{
    partial class Presser
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbText = new System.Windows.Forms.Label();
            this.pnText = new System.Windows.Forms.Panel();
            this.pnText.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbText
            // 
            this.lbText.BackColor = System.Drawing.Color.White;
            this.lbText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbText.Location = new System.Drawing.Point(0, 0);
            this.lbText.Margin = new System.Windows.Forms.Padding(0);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(40, 20);
            this.lbText.TabIndex = 0;
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbText.MouseEnter += new System.EventHandler(this.lbText_MouseEnter);
            this.lbText.MouseLeave += new System.EventHandler(this.lbText_MouseLeave);
            // 
            // pnText
            // 
            this.pnText.Controls.Add(this.lbText);
            this.pnText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnText.Location = new System.Drawing.Point(0, 0);
            this.pnText.Margin = new System.Windows.Forms.Padding(0);
            this.pnText.Name = "pnText";
            this.pnText.Size = new System.Drawing.Size(40, 20);
            this.pnText.TabIndex = 1;
            // 
            // Presser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnText);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Presser";
            this.Size = new System.Drawing.Size(40, 20);
            this.pnText.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.Panel pnText;
    }
}
