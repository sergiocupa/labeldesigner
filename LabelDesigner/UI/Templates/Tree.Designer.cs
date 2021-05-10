
namespace LabelDesigner.UI.Templates
{
    partial class Tree
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
            this.pnBody = new System.Windows.Forms.Panel();
            this.pnArea = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnBody.SuspendLayout();
            this.pnArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.AutoScroll = true;
            this.pnBody.Controls.Add(this.pnArea);
            this.pnBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnBody.Location = new System.Drawing.Point(0, 0);
            this.pnBody.Margin = new System.Windows.Forms.Padding(0);
            this.pnBody.Name = "pnBody";
            this.pnBody.Size = new System.Drawing.Size(360, 309);
            this.pnBody.TabIndex = 0;
            // 
            // pnArea
            // 
            this.pnArea.Controls.Add(this.panel1);
            this.pnArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnArea.Location = new System.Drawing.Point(0, 0);
            this.pnArea.Name = "pnArea";
            this.pnArea.Size = new System.Drawing.Size(360, 200);
            this.pnArea.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(128, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(171, 43);
            this.panel1.TabIndex = 0;
            // 
            // Tree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnBody);
            this.Name = "Tree";
            this.Size = new System.Drawing.Size(360, 309);
            this.pnBody.ResumeLayout(false);
            this.pnArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnBody;
        private System.Windows.Forms.Panel pnArea;
        private System.Windows.Forms.Panel panel1;
    }
}
