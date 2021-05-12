
namespace LabelDesigner.UI.Templates
{
    partial class LabelTextAddSelector
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.prAdd = new LabelDesigner.UI.Templates.Presser();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txValue = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lxTitle = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.prSelecionar = new LabelDesigner.UI.Templates.Presser();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(442, 48);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.prAdd);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(386, 17);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(28, 31);
            this.panel5.TabIndex = 4;
            // 
            // prAdd
            // 
            this.prAdd.BackColor = System.Drawing.Color.White;
            this.prAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prAdd.EnabledBorder = false;
            this.prAdd.Image = global::LabelDesigner.Properties.Resources.add_tr;
            this.prAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prAdd.Label = "";
            this.prAdd.Location = new System.Drawing.Point(0, 0);
            this.prAdd.Margin = new System.Windows.Forms.Padding(0);
            this.prAdd.Name = "prAdd";
            this.prAdd.Size = new System.Drawing.Size(28, 31);
            this.prAdd.TabIndex = 0;
            this.prAdd.Click += new System.EventHandler(this.prAdd_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel2.Location = new System.Drawing.Point(0, 17);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(386, 31);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.txValue);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panel3.Size = new System.Drawing.Size(386, 31);
            this.panel3.TabIndex = 1;
            // 
            // txValue
            // 
            this.txValue.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txValue.Location = new System.Drawing.Point(5, 5);
            this.txValue.Margin = new System.Windows.Forms.Padding(0);
            this.txValue.Multiline = true;
            this.txValue.Name = "txValue";
            this.txValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txValue.Size = new System.Drawing.Size(376, 21);
            this.txValue.TabIndex = 0;
            this.txValue.Text = "Valor g...";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.panel1.Controls.Add(this.lxTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.panel1.Size = new System.Drawing.Size(386, 17);
            this.panel1.TabIndex = 1;
            // 
            // lxTitle
            // 
            this.lxTitle.BackColor = System.Drawing.Color.White;
            this.lxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lxTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lxTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lxTitle.Location = new System.Drawing.Point(0, 0);
            this.lxTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lxTitle.Name = "lxTitle";
            this.lxTitle.Size = new System.Drawing.Size(386, 16);
            this.lxTitle.TabIndex = 2;
            this.lxTitle.Text = "Título";
            this.lxTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.prSelecionar);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(418, 20);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(20, 25);
            this.panel4.TabIndex = 3;
            // 
            // prSelecionar
            // 
            this.prSelecionar.BackColor = System.Drawing.Color.White;
            this.prSelecionar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prSelecionar.EnabledBorder = false;
            this.prSelecionar.Image = global::LabelDesigner.Properties.Resources.arrow_right_16;
            this.prSelecionar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prSelecionar.Label = "";
            this.prSelecionar.Location = new System.Drawing.Point(0, 0);
            this.prSelecionar.Margin = new System.Windows.Forms.Padding(0);
            this.prSelecionar.Name = "prSelecionar";
            this.prSelecionar.Size = new System.Drawing.Size(20, 25);
            this.prSelecionar.TabIndex = 0;
            this.prSelecionar.Click += new System.EventHandler(this.prSelecionar_Click);
            // 
            // LabelTextAddSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LabelTextAddSelector";
            this.Size = new System.Drawing.Size(442, 48);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txValue;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lxTitle;
        private System.Windows.Forms.Panel panel4;
        private Presser prSelecionar;
        private System.Windows.Forms.Panel panel5;
        private Presser prAdd;
    }
}
