

using LabelDesigner.UI.Templates;

namespace LabelDesigner.UI.Editors
{
    partial class TextFieldEditor
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lxFonteTamanho = new LabelText();
            this.txFonte = new LabelText();
            this.cxReverse = new System.Windows.Forms.CheckBox();
            this.lxFormat = new LabelText();
            this.fdContent = new LabelTextSelector();
            this.fdModo = new LabelCombo();
            this.fdName = new LabelText();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cxRotacao = new LabelCombo();
            this.cxRefButtonLeft = new System.Windows.Forms.CheckBox();
            this.lxTamanhoAltura = new LabelText();
            this.lxTamanhoLargura = new LabelText();
            this.area2 = new Area();
            this.lxPosicaoY = new LabelText();
            this.lxPosicaoX = new LabelText();
            this.area1 = new Area();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(454, 451);
            this.panel1.TabIndex = 0;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(454, 451);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.bxOk);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 411);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(454, 40);
            this.panel3.TabIndex = 1;
            // 
            // bxOk
            // 
            this.bxOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bxOk.Location = new System.Drawing.Point(364, 9);
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
            this.panel2.Size = new System.Drawing.Size(454, 411);
            this.panel2.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(454, 411);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lxFonteTamanho);
            this.tabPage1.Controls.Add(this.txFonte);
            this.tabPage1.Controls.Add(this.cxReverse);
            this.tabPage1.Controls.Add(this.lxFormat);
            this.tabPage1.Controls.Add(this.fdContent);
            this.tabPage1.Controls.Add(this.fdModo);
            this.tabPage1.Controls.Add(this.fdName);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(446, 385);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Conteúdo";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lxFonteTamanho
            // 
            this.lxFonteTamanho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lxFonteTamanho.BackColor = System.Drawing.Color.White;
            this.lxFonteTamanho.Content = "";
            this.lxFonteTamanho.Location = new System.Drawing.Point(148, 138);
            this.lxFonteTamanho.Margin = new System.Windows.Forms.Padding(0);
            this.lxFonteTamanho.Name = "lxFonteTamanho";
            this.lxFonteTamanho.Size = new System.Drawing.Size(93, 39);
            this.lxFonteTamanho.TabIndex = 8;
            this.lxFonteTamanho.Title = "Tamanho";
            // 
            // txFonte
            // 
            this.txFonte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txFonte.BackColor = System.Drawing.Color.White;
            this.txFonte.Content = "";
            this.txFonte.Location = new System.Drawing.Point(38, 138);
            this.txFonte.Margin = new System.Windows.Forms.Padding(0);
            this.txFonte.Name = "txFonte";
            this.txFonte.Size = new System.Drawing.Size(93, 39);
            this.txFonte.TabIndex = 7;
            this.txFonte.Title = "Fonte";
            // 
            // cxReverse
            // 
            this.cxReverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cxReverse.AutoSize = true;
            this.cxReverse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.cxReverse.Location = new System.Drawing.Point(250, 353);
            this.cxReverse.Name = "cxReverse";
            this.cxReverse.Size = new System.Drawing.Size(140, 17);
            this.cxReverse.TabIndex = 6;
            this.cxReverse.Text = "Preenchimento invertido";
            this.cxReverse.UseVisualStyleBackColor = true;
            // 
            // lxFormat
            // 
            this.lxFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lxFormat.BackColor = System.Drawing.Color.White;
            this.lxFormat.Content = "";
            this.lxFormat.Location = new System.Drawing.Point(36, 333);
            this.lxFormat.Margin = new System.Windows.Forms.Padding(0);
            this.lxFormat.Name = "lxFormat";
            this.lxFormat.Size = new System.Drawing.Size(195, 39);
            this.lxFormat.TabIndex = 5;
            this.lxFormat.Title = "Formato";
            // 
            // fdContent
            // 
            this.fdContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fdContent.BackColor = System.Drawing.Color.White;
            this.fdContent.Content = "";
            this.fdContent.Location = new System.Drawing.Point(38, 197);
            this.fdContent.Margin = new System.Windows.Forms.Padding(0);
            this.fdContent.Name = "fdContent";
            this.fdContent.Size = new System.Drawing.Size(373, 122);
            this.fdContent.TabIndex = 4;
            this.fdContent.Title = "Conteúdo";
            this.fdContent.Click += new System.EventHandler(this.fdContent_Click);
            // 
            // fdModo
            // 
            this.fdModo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fdModo.BackColor = System.Drawing.Color.White;
            this.fdModo.DataSource = null;
            this.fdModo.DisplayMember = "";
            this.fdModo.Enabled = false;
            this.fdModo.Location = new System.Drawing.Point(38, 23);
            this.fdModo.Margin = new System.Windows.Forms.Padding(0);
            this.fdModo.Name = "fdModo";
            this.fdModo.Size = new System.Drawing.Size(373, 42);
            this.fdModo.TabIndex = 2;
            this.fdModo.Title = "Modo";
            this.fdModo.ValueMember = "";
            // 
            // fdName
            // 
            this.fdName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fdName.BackColor = System.Drawing.Color.White;
            this.fdName.Content = "";
            this.fdName.Enabled = false;
            this.fdName.Location = new System.Drawing.Point(38, 81);
            this.fdName.Margin = new System.Windows.Forms.Padding(0);
            this.fdName.Name = "fdName";
            this.fdName.Size = new System.Drawing.Size(373, 39);
            this.fdName.TabIndex = 0;
            this.fdName.Title = "Nome";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cxRotacao);
            this.tabPage2.Controls.Add(this.cxRefButtonLeft);
            this.tabPage2.Controls.Add(this.lxTamanhoAltura);
            this.tabPage2.Controls.Add(this.lxTamanhoLargura);
            this.tabPage2.Controls.Add(this.area2);
            this.tabPage2.Controls.Add(this.lxPosicaoY);
            this.tabPage2.Controls.Add(this.lxPosicaoX);
            this.tabPage2.Controls.Add(this.area1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(446, 385);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Propriedades";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cxRotacao
            // 
            this.cxRotacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cxRotacao.BackColor = System.Drawing.Color.White;
            this.cxRotacao.DataSource = null;
            this.cxRotacao.DisplayMember = "";
            this.cxRotacao.Location = new System.Drawing.Point(56, 122);
            this.cxRotacao.Margin = new System.Windows.Forms.Padding(0);
            this.cxRotacao.Name = "cxRotacao";
            this.cxRotacao.Size = new System.Drawing.Size(153, 42);
            this.cxRotacao.TabIndex = 14;
            this.cxRotacao.Title = "Rotação";
            this.cxRotacao.ValueMember = "";
            // 
            // cxRefButtonLeft
            // 
            this.cxRefButtonLeft.AutoSize = true;
            this.cxRefButtonLeft.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.cxRefButtonLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.cxRefButtonLeft.Location = new System.Drawing.Point(228, 144);
            this.cxRefButtonLeft.Name = "cxRefButtonLeft";
            this.cxRefButtonLeft.Size = new System.Drawing.Size(122, 17);
            this.cxRefButtonLeft.TabIndex = 12;
            this.cxRefButtonLeft.Text = "Esquerdo-Abaixo";
            this.cxRefButtonLeft.UseVisualStyleBackColor = true;
            // 
            // lxTamanhoAltura
            // 
            this.lxTamanhoAltura.BackColor = System.Drawing.Color.White;
            this.lxTamanhoAltura.Content = "0";
            this.lxTamanhoAltura.Location = new System.Drawing.Point(223, 233);
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
            this.lxTamanhoLargura.Location = new System.Drawing.Point(56, 233);
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
            this.area2.Location = new System.Drawing.Point(41, 195);
            this.area2.Margin = new System.Windows.Forms.Padding(0);
            this.area2.Name = "area2";
            this.area2.Size = new System.Drawing.Size(362, 113);
            this.area2.TabIndex = 4;
            this.area2.Title = "Tamanho";
            // 
            // lxPosicaoY
            // 
            this.lxPosicaoY.BackColor = System.Drawing.Color.White;
            this.lxPosicaoY.Content = "0";
            this.lxPosicaoY.Location = new System.Drawing.Point(223, 65);
            this.lxPosicaoY.Margin = new System.Windows.Forms.Padding(0);
            this.lxPosicaoY.Name = "lxPosicaoY";
            this.lxPosicaoY.Size = new System.Drawing.Size(153, 39);
            this.lxPosicaoY.TabIndex = 3;
            this.lxPosicaoY.Title = "Y";
            // 
            // lxPosicaoX
            // 
            this.lxPosicaoX.BackColor = System.Drawing.Color.White;
            this.lxPosicaoX.Content = "0";
            this.lxPosicaoX.Location = new System.Drawing.Point(56, 65);
            this.lxPosicaoX.Margin = new System.Windows.Forms.Padding(0);
            this.lxPosicaoX.Name = "lxPosicaoX";
            this.lxPosicaoX.Size = new System.Drawing.Size(153, 39);
            this.lxPosicaoX.TabIndex = 2;
            this.lxPosicaoX.Title = "X";
            // 
            // area1
            // 
            this.area1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.area1.BackColor = System.Drawing.Color.White;
            this.area1.Location = new System.Drawing.Point(41, 27);
            this.area1.Margin = new System.Windows.Forms.Padding(0);
            this.area1.Name = "area1";
            this.area1.Size = new System.Drawing.Size(362, 146);
            this.area1.TabIndex = 0;
            this.area1.Title = "Posição";
            // 
            // TextFieldEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(454, 451);
            this.Controls.Add(this.panel1);
            this.Name = "TextFieldEditor";
            this.Text = "Texto";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Area area1;
        private LabelText lxTamanhoAltura;
        private LabelText lxTamanhoLargura;
        private Area area2;
        private LabelText lxPosicaoY;
        private LabelText lxPosicaoX;
        public LabelText fdName;
        public LabelCombo fdModo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bxOk;
        private System.Windows.Forms.CheckBox cxRefButtonLeft;
        public LabelTextSelector fdContent;
        public LabelText lxFormat;
        private System.Windows.Forms.CheckBox cxReverse;
        public LabelCombo cxRotacao;
        public LabelText txFonte;
        public LabelText lxFonteTamanho;
    }
}