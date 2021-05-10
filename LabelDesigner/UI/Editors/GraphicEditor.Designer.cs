

using LabelDesigner.UI.Templates;


namespace LabelDesigner.UI.Editors
{
    partial class GraphicEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bxOk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lxTamanhoAltura = new LabelText();
            this.lxTamanhoLargura = new LabelText();
            this.area2 = new Area();
            this.area1 = new Area();
            this.area3 = new Area();
            this.lxEspessura = new LabelText();
            this.lxPosicaoY = new LabelText();
            this.lxPosicaoX = new LabelText();
            this.cxRefButtonLeft = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(457, 447);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(457, 447);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.bxOk);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 407);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(457, 40);
            this.panel3.TabIndex = 1;
            // 
            // bxOk
            // 
            this.bxOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bxOk.Location = new System.Drawing.Point(367, 9);
            this.bxOk.Name = "bxOk";
            this.bxOk.Size = new System.Drawing.Size(75, 23);
            this.bxOk.TabIndex = 0;
            this.bxOk.Text = "Ok";
            this.bxOk.UseVisualStyleBackColor = true;
            this.bxOk.Click += new System.EventHandler(this.bxOk_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(457, 407);
            this.panel2.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(457, 407);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cxRefButtonLeft);
            this.tabPage2.Controls.Add(this.lxPosicaoY);
            this.tabPage2.Controls.Add(this.lxPosicaoX);
            this.tabPage2.Controls.Add(this.lxEspessura);
            this.tabPage2.Controls.Add(this.area3);
            this.tabPage2.Controls.Add(this.lxTamanhoAltura);
            this.tabPage2.Controls.Add(this.lxTamanhoLargura);
            this.tabPage2.Controls.Add(this.area2);
            this.tabPage2.Controls.Add(this.area1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(449, 381);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Propriedades";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lxTamanhoAltura
            // 
            this.lxTamanhoAltura.BackColor = System.Drawing.Color.White;
            this.lxTamanhoAltura.Content = "0";
            this.lxTamanhoAltura.Location = new System.Drawing.Point(223, 173);
            this.lxTamanhoAltura.Margin = new System.Windows.Forms.Padding(0);
            this.lxTamanhoAltura.Name = "lxTamanhoAltura";
            this.lxTamanhoAltura.Size = new System.Drawing.Size(153, 39);
            this.lxTamanhoAltura.TabIndex = 6;
            this.lxTamanhoAltura.Title = "Altura";
            // 
            // lxTamanhoLargura
            // 
            this.lxTamanhoLargura.BackColor = System.Drawing.Color.White;
            this.lxTamanhoLargura.Content = "0";
            this.lxTamanhoLargura.Location = new System.Drawing.Point(56, 173);
            this.lxTamanhoLargura.Margin = new System.Windows.Forms.Padding(0);
            this.lxTamanhoLargura.Name = "lxTamanhoLargura";
            this.lxTamanhoLargura.Size = new System.Drawing.Size(153, 39);
            this.lxTamanhoLargura.TabIndex = 5;
            this.lxTamanhoLargura.Title = "Largura";
            // 
            // area2
            // 
            this.area2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.area2.BackColor = System.Drawing.Color.White;
            this.area2.Location = new System.Drawing.Point(41, 135);
            this.area2.Margin = new System.Windows.Forms.Padding(0);
            this.area2.Name = "area2";
            this.area2.Size = new System.Drawing.Size(359, 113);
            this.area2.TabIndex = 4;
            this.area2.Title = "Tamanho";
            // 
            // area1
            // 
            this.area1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.area1.BackColor = System.Drawing.Color.White;
            this.area1.Location = new System.Drawing.Point(41, 27);
            this.area1.Margin = new System.Windows.Forms.Padding(0);
            this.area1.Name = "area1";
            this.area1.Size = new System.Drawing.Size(359, 113);
            this.area1.TabIndex = 0;
            this.area1.Title = "Posição";
            // 
            // area3
            // 
            this.area3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.area3.BackColor = System.Drawing.Color.White;
            this.area3.Location = new System.Drawing.Point(41, 247);
            this.area3.Margin = new System.Windows.Forms.Padding(0);
            this.area3.Name = "area3";
            this.area3.Size = new System.Drawing.Size(359, 113);
            this.area3.TabIndex = 7;
            this.area3.Title = "Material";
            // 
            // lxEspessura
            // 
            this.lxEspessura.BackColor = System.Drawing.Color.White;
            this.lxEspessura.Content = "0";
            this.lxEspessura.Location = new System.Drawing.Point(56, 285);
            this.lxEspessura.Margin = new System.Windows.Forms.Padding(0);
            this.lxEspessura.Name = "lxEspessura";
            this.lxEspessura.Size = new System.Drawing.Size(153, 39);
            this.lxEspessura.TabIndex = 8;
            this.lxEspessura.Title = "Espessura";
            // 
            // lxPosicaoY
            // 
            this.lxPosicaoY.BackColor = System.Drawing.Color.White;
            this.lxPosicaoY.Content = "0";
            this.lxPosicaoY.Location = new System.Drawing.Point(153, 65);
            this.lxPosicaoY.Margin = new System.Windows.Forms.Padding(0);
            this.lxPosicaoY.Name = "lxPosicaoY";
            this.lxPosicaoY.Size = new System.Drawing.Size(93, 39);
            this.lxPosicaoY.TabIndex = 10;
            this.lxPosicaoY.Title = "Y";
            // 
            // lxPosicaoX
            // 
            this.lxPosicaoX.BackColor = System.Drawing.Color.White;
            this.lxPosicaoX.Content = "0";
            this.lxPosicaoX.Location = new System.Drawing.Point(56, 65);
            this.lxPosicaoX.Margin = new System.Windows.Forms.Padding(0);
            this.lxPosicaoX.Name = "lxPosicaoX";
            this.lxPosicaoX.Size = new System.Drawing.Size(88, 39);
            this.lxPosicaoX.TabIndex = 9;
            this.lxPosicaoX.Title = "X";
            // 
            // cxRefButtonLeft
            // 
            this.cxRefButtonLeft.AutoSize = true;
            this.cxRefButtonLeft.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.cxRefButtonLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.cxRefButtonLeft.Location = new System.Drawing.Point(268, 84);
            this.cxRefButtonLeft.Name = "cxRefButtonLeft";
            this.cxRefButtonLeft.Size = new System.Drawing.Size(122, 17);
            this.cxRefButtonLeft.TabIndex = 11;
            this.cxRefButtonLeft.Text = "Esquerdo-Abaixo";
            this.cxRefButtonLeft.UseVisualStyleBackColor = true;
            // 
            // GraphicEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 447);
            this.Controls.Add(this.panel1);
            this.Name = "GraphicEditor";
            this.Text = "GraphicEditor";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bxOk;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private LabelText lxTamanhoAltura;
        private LabelText lxTamanhoLargura;
        private Area area2;
        private Area area1;
        private LabelText lxEspessura;
        private Area area3;
        private System.Windows.Forms.CheckBox cxRefButtonLeft;
        private LabelText lxPosicaoY;
        private LabelText lxPosicaoX;
    }
}