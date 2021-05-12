
using LabelDesigner.UI.Templates;

namespace LabelDesigner.UI.Editors
{
    partial class Vinculacoes
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
            this.dgLigacoes = new System.Windows.Forms.DataGridView();
            this.prAddVinculacao = new Presser();
            this.prSub = new Presser();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cxSelecionar = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgCampos = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.prRemove = new Presser();
            ((System.ComponentModel.ISupportInitialize)(this.dgLigacoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCampos)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgLigacoes
            // 
            this.dgLigacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLigacoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLigacoes.Location = new System.Drawing.Point(0, 0);
            this.dgLigacoes.Name = "dgLigacoes";
            this.dgLigacoes.Size = new System.Drawing.Size(365, 352);
            this.dgLigacoes.TabIndex = 14;
            this.dgLigacoes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLigacoes_CellClick);
            // 
            // prAddVinculacao
            // 
            this.prAddVinculacao.BackColor = System.Drawing.Color.White;
            this.prAddVinculacao.EnabledBorder = false;
           // this.prAddVinculacao.Image = global::EditorDER.Properties.Resources.add_tr;
            this.prAddVinculacao.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prAddVinculacao.Label = "";
            this.prAddVinculacao.Location = new System.Drawing.Point(3, 4);
            this.prAddVinculacao.Margin = new System.Windows.Forms.Padding(0);
            this.prAddVinculacao.Name = "prAddVinculacao";
            this.prAddVinculacao.Size = new System.Drawing.Size(18, 18);
            this.prAddVinculacao.TabIndex = 17;
            this.prAddVinculacao.Click += new System.EventHandler(this.prAddVinculacao_Click);
            // 
            // prSub
            // 
            this.prSub.BackColor = System.Drawing.Color.White;
            this.prSub.EnabledBorder = false;
           // this.prSub.Image = global::EditorDER.Properties.Resources.sub_tr;
            this.prSub.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prSub.Label = "";
            this.prSub.Location = new System.Drawing.Point(24, 4);
            this.prSub.Margin = new System.Windows.Forms.Padding(0);
            this.prSub.Name = "prSub";
            this.prSub.Size = new System.Drawing.Size(18, 18);
            this.prSub.TabIndex = 18;
            this.prSub.Click += new System.EventHandler(this.prSub_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(602, 376);
            this.splitContainer1.SplitterDistance = 365;
            this.splitContainer1.TabIndex = 20;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(365, 376);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgLigacoes);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(365, 352);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cxSelecionar);
            this.panel1.Controls.Add(this.prAddVinculacao);
            this.panel1.Controls.Add(this.prSub);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 24);
            this.panel1.TabIndex = 0;
            // 
            // cxSelecionar
            // 
            this.cxSelecionar.AutoSize = true;
            this.cxSelecionar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(255)))));
            this.cxSelecionar.Location = new System.Drawing.Point(65, 6);
            this.cxSelecionar.Name = "cxSelecionar";
            this.cxSelecionar.Size = new System.Drawing.Size(76, 17);
            this.cxSelecionar.TabIndex = 78;
            this.cxSelecionar.Text = "Selecionar";
            this.cxSelecionar.UseVisualStyleBackColor = true;
            this.cxSelecionar.CheckedChanged += new System.EventHandler(this.cxSelecionar_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(233, 376);
            this.tableLayoutPanel2.TabIndex = 22;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgCampos);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 24);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(233, 352);
            this.panel3.TabIndex = 1;
            // 
            // dgCampos
            // 
            this.dgCampos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCampos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCampos.Location = new System.Drawing.Point(0, 0);
            this.dgCampos.Name = "dgCampos";
            this.dgCampos.Size = new System.Drawing.Size(233, 352);
            this.dgCampos.TabIndex = 14;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.prRemove);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(233, 24);
            this.panel4.TabIndex = 0;
            // 
            // prRemove
            // 
            this.prRemove.BackColor = System.Drawing.Color.White;
            this.prRemove.EnabledBorder = false;
          //  this.prRemove.Image = global::EditorDER.Properties.Resources.sub_tr;
            this.prRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prRemove.Label = "";
            this.prRemove.Location = new System.Drawing.Point(3, 4);
            this.prRemove.Margin = new System.Windows.Forms.Padding(0);
            this.prRemove.Name = "prRemove";
            this.prRemove.Size = new System.Drawing.Size(18, 18);
            this.prRemove.TabIndex = 18;
            this.prRemove.Click += new System.EventHandler(this.prRemove_Click);
            // 
            // Vinculacoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(602, 376);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Vinculacoes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vinculacoes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Vinculacoes_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgLigacoes)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCampos)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgLigacoes;
        private Presser prAddVinculacao;
        private Presser prSub;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgCampos;
        private System.Windows.Forms.Panel panel4;
        private Presser prRemove;
        private System.Windows.Forms.CheckBox cxSelecionar;
    }
}