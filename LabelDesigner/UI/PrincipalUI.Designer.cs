

using LabelDesigner.Properties;
using LabelDesigner.UI.Templates;

namespace LabelDesigner.UI
{
    partial class PrincipalUI
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

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btExport = new System.Windows.Forms.Button();
            this.bxSave = new System.Windows.Forms.Button();
            this.lxEtiqueta = new LabelDesigner.UI.Templates.LabelTextAddSelector();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.dgElements = new System.Windows.Forms.DataGridView();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.presser3 = new LabelDesigner.UI.Templates.Presser();
            this.label2 = new System.Windows.Forms.Label();
            this.prVinculos = new LabelDesigner.UI.Templates.Presser();
            this.prImage = new LabelDesigner.UI.Templates.Presser();
            this.prText = new LabelDesigner.UI.Templates.Presser();
            this.prSub = new LabelDesigner.UI.Templates.Presser();
            this.prBox = new LabelDesigner.UI.Templates.Presser();
            this.prBar39 = new LabelDesigner.UI.Templates.Presser();
            this.prDatamatrix = new LabelDesigner.UI.Templates.Presser();
            this.prBar128 = new LabelDesigner.UI.Templates.Presser();
            this.prQrCode = new LabelDesigner.UI.Templates.Presser();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.pnContent = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lxTitle = new System.Windows.Forms.Label();
            this.offsetY = new LabelDesigner.UI.Templates.LabelTextHorizontal();
            this.offsetX = new LabelDesigner.UI.Templates.LabelTextHorizontal();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.txZoom = new System.Windows.Forms.TextBox();
            this.btSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgElements)).BeginInit();
            this.panel11.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1180, 576);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btExport);
            this.panel1.Controls.Add(this.bxSave);
            this.panel1.Controls.Add(this.lxEtiqueta);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1180, 68);
            this.panel1.TabIndex = 0;
            // 
            // btExport
            // 
            this.btExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btExport.Location = new System.Drawing.Point(531, 29);
            this.btExport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(123, 27);
            this.btExport.TabIndex = 11;
            this.btExport.Text = "Export to ZPL";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // bxSave
            // 
            this.bxSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bxSave.Location = new System.Drawing.Point(435, 29);
            this.bxSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bxSave.Name = "bxSave";
            this.bxSave.Size = new System.Drawing.Size(88, 27);
            this.bxSave.TabIndex = 10;
            this.bxSave.Text = "Save";
            this.bxSave.UseVisualStyleBackColor = true;
            this.bxSave.Click += new System.EventHandler(this.bxSave_Click);
            // 
            // lxEtiqueta
            // 
            this.lxEtiqueta.BackColor = System.Drawing.Color.White;
            this.lxEtiqueta.Content = "";
            this.lxEtiqueta.Location = new System.Drawing.Point(10, 8);
            this.lxEtiqueta.Margin = new System.Windows.Forms.Padding(0);
            this.lxEtiqueta.Name = "lxEtiqueta";
            this.lxEtiqueta.Size = new System.Drawing.Size(394, 48);
            this.lxEtiqueta.TabIndex = 0;
            this.lxEtiqueta.Title = "Etiqueta";
            this.lxEtiqueta.ClickAdd += new System.Action(this.labelTextAddSelector1_ClickAdd);
            this.lxEtiqueta.Click += new System.EventHandler(this.labelTextAddSelector1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 68);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1180, 508);
            this.panel2.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel4);
            this.splitContainer1.Size = new System.Drawing.Size(1180, 508);
            this.splitContainer1.SplitterDistance = 424;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 11;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel12, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel11, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(424, 508);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.dgElements);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 53);
            this.panel12.Margin = new System.Windows.Forms.Padding(0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(424, 455);
            this.panel12.TabIndex = 13;
            // 
            // dgElements
            // 
            this.dgElements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgElements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgElements.Location = new System.Drawing.Point(0, 0);
            this.dgElements.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgElements.Name = "dgElements";
            this.dgElements.Size = new System.Drawing.Size(424, 455);
            this.dgElements.TabIndex = 0;
            this.dgElements.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgElements_CellClick);
            this.dgElements.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgElements_CellDoubleClick);
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel11.Controls.Add(this.panel3);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Margin = new System.Windows.Forms.Padding(0);
            this.panel11.Name = "panel11";
            this.panel11.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.panel11.Size = new System.Drawing.Size(424, 53);
            this.panel11.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.presser3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.prVinculos);
            this.panel3.Controls.Add(this.prImage);
            this.panel3.Controls.Add(this.prText);
            this.panel3.Controls.Add(this.prSub);
            this.panel3.Controls.Add(this.prBox);
            this.panel3.Controls.Add(this.prBar39);
            this.panel3.Controls.Add(this.prDatamatrix);
            this.panel3.Controls.Add(this.prBar128);
            this.panel3.Controls.Add(this.prQrCode);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(424, 52);
            this.panel3.TabIndex = 0;
            // 
            // presser3
            // 
            this.presser3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.presser3.BackColor = System.Drawing.Color.White;
            this.presser3.EnabledBorder = false;
            this.presser3.Image = global::LabelDesigner.Properties.Resources.text_render;
            this.presser3.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.presser3.Label = "";
            this.presser3.Location = new System.Drawing.Point(26, 27);
            this.presser3.Margin = new System.Windows.Forms.Padding(0);
            this.presser3.Name = "presser3";
            this.presser3.Size = new System.Drawing.Size(21, 21);
            this.presser3.TabIndex = 2;
            this.presser3.Click += new System.EventHandler(this.presser3_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(269, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "Fonte Alternativa";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // prVinculos
            // 
            this.prVinculos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.prVinculos.BackColor = System.Drawing.Color.White;
            this.prVinculos.EnabledBorder = false;
            this.prVinculos.Image = global::LabelDesigner.Properties.Resources.arrow_right_16;
            this.prVinculos.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prVinculos.Label = "";
            this.prVinculos.Location = new System.Drawing.Point(399, 28);
            this.prVinculos.Margin = new System.Windows.Forms.Padding(0);
            this.prVinculos.Name = "prVinculos";
            this.prVinculos.Size = new System.Drawing.Size(21, 21);
            this.prVinculos.TabIndex = 9;
            this.prVinculos.Click += new System.EventHandler(this.prVinculos_Click);
            // 
            // prImage
            // 
            this.prImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prImage.BackColor = System.Drawing.Color.White;
            this.prImage.EnabledBorder = false;
            this.prImage.Image = global::LabelDesigner.Properties.Resources.picture;
            this.prImage.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prImage.Label = "";
            this.prImage.Location = new System.Drawing.Point(159, 27);
            this.prImage.Margin = new System.Windows.Forms.Padding(0);
            this.prImage.Name = "prImage";
            this.prImage.Size = new System.Drawing.Size(21, 21);
            this.prImage.TabIndex = 7;
            this.prImage.Click += new System.EventHandler(this.prImage_Click);
            // 
            // prText
            // 
            this.prText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prText.BackColor = System.Drawing.Color.White;
            this.prText.EnabledBorder = false;
            this.prText.Image = global::LabelDesigner.Properties.Resources.text;
            this.prText.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prText.Label = "";
            this.prText.Location = new System.Drawing.Point(4, 27);
            this.prText.Margin = new System.Windows.Forms.Padding(0);
            this.prText.Name = "prText";
            this.prText.Size = new System.Drawing.Size(21, 21);
            this.prText.TabIndex = 1;
            this.prText.Click += new System.EventHandler(this.prText_Click);
            // 
            // prSub
            // 
            this.prSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prSub.BackColor = System.Drawing.Color.White;
            this.prSub.EnabledBorder = false;
            this.prSub.Image = global::LabelDesigner.Properties.Resources.sub_tr;
            this.prSub.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prSub.Label = "";
            this.prSub.Location = new System.Drawing.Point(187, 27);
            this.prSub.Margin = new System.Windows.Forms.Padding(0);
            this.prSub.Name = "prSub";
            this.prSub.Size = new System.Drawing.Size(21, 21);
            this.prSub.TabIndex = 8;
            this.prSub.Click += new System.EventHandler(this.prSub_Click);
            // 
            // prBox
            // 
            this.prBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prBox.BackColor = System.Drawing.Color.White;
            this.prBox.EnabledBorder = false;
            this.prBox.Image = global::LabelDesigner.Properties.Resources.graphics;
            this.prBox.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prBox.Label = "";
            this.prBox.Location = new System.Drawing.Point(48, 27);
            this.prBox.Margin = new System.Windows.Forms.Padding(0);
            this.prBox.Name = "prBox";
            this.prBox.Size = new System.Drawing.Size(21, 21);
            this.prBox.TabIndex = 2;
            this.prBox.Click += new System.EventHandler(this.prBox_Click);
            // 
            // prBar39
            // 
            this.prBar39.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prBar39.BackColor = System.Drawing.Color.White;
            this.prBar39.EnabledBorder = false;
            this.prBar39.Image = global::LabelDesigner.Properties.Resources.bar_code_39;
            this.prBar39.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prBar39.Label = "";
            this.prBar39.Location = new System.Drawing.Point(70, 27);
            this.prBar39.Margin = new System.Windows.Forms.Padding(0);
            this.prBar39.Name = "prBar39";
            this.prBar39.Size = new System.Drawing.Size(21, 21);
            this.prBar39.TabIndex = 3;
            this.prBar39.Click += new System.EventHandler(this.prBar39_Click);
            // 
            // prDatamatrix
            // 
            this.prDatamatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prDatamatrix.BackColor = System.Drawing.Color.White;
            this.prDatamatrix.EnabledBorder = false;
            this.prDatamatrix.Image = global::LabelDesigner.Properties.Resources.datamatrix_16;
            this.prDatamatrix.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prDatamatrix.Label = "";
            this.prDatamatrix.Location = new System.Drawing.Point(136, 27);
            this.prDatamatrix.Margin = new System.Windows.Forms.Padding(0);
            this.prDatamatrix.Name = "prDatamatrix";
            this.prDatamatrix.Size = new System.Drawing.Size(21, 21);
            this.prDatamatrix.TabIndex = 6;
            this.prDatamatrix.Click += new System.EventHandler(this.prDatamatrix_Click);
            // 
            // prBar128
            // 
            this.prBar128.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prBar128.BackColor = System.Drawing.Color.White;
            this.prBar128.EnabledBorder = false;
            this.prBar128.Image = global::LabelDesigner.Properties.Resources.bar_code_128;
            this.prBar128.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prBar128.Label = "";
            this.prBar128.Location = new System.Drawing.Point(92, 27);
            this.prBar128.Margin = new System.Windows.Forms.Padding(0);
            this.prBar128.Name = "prBar128";
            this.prBar128.Size = new System.Drawing.Size(21, 21);
            this.prBar128.TabIndex = 4;
            this.prBar128.Click += new System.EventHandler(this.prBar128_Click);
            // 
            // prQrCode
            // 
            this.prQrCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prQrCode.BackColor = System.Drawing.Color.White;
            this.prQrCode.EnabledBorder = false;
            this.prQrCode.Image = global::LabelDesigner.Properties.Resources.qr_code_16;
            this.prQrCode.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.prQrCode.Label = "";
            this.prQrCode.Location = new System.Drawing.Point(114, 27);
            this.prQrCode.Margin = new System.Windows.Forms.Padding(0);
            this.prQrCode.Name = "prQrCode";
            this.prQrCode.Size = new System.Drawing.Size(21, 21);
            this.prQrCode.TabIndex = 5;
            this.prQrCode.Click += new System.EventHandler(this.prQrCode_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panel13, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel14, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(751, 508);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.pnContent);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(0, 53);
            this.panel13.Margin = new System.Windows.Forms.Padding(0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(751, 455);
            this.panel13.TabIndex = 13;
            // 
            // pnContent
            // 
            this.pnContent.AutoScroll = true;
            this.pnContent.BackColor = System.Drawing.Color.White;
            this.pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnContent.Location = new System.Drawing.Point(0, 0);
            this.pnContent.Margin = new System.Windows.Forms.Padding(0);
            this.pnContent.Name = "pnContent";
            this.pnContent.Size = new System.Drawing.Size(751, 455);
            this.pnContent.TabIndex = 1;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel14.Controls.Add(this.panel8);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Margin = new System.Windows.Forms.Padding(0);
            this.panel14.Name = "panel14";
            this.panel14.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.panel14.Size = new System.Drawing.Size(751, 53);
            this.panel14.TabIndex = 12;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.label1);
            this.panel8.Controls.Add(this.lxTitle);
            this.panel8.Controls.Add(this.offsetY);
            this.panel8.Controls.Add(this.offsetX);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(751, 52);
            this.panel8.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 18);
            this.label1.TabIndex = 16;
            this.label1.Text = "Posição";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lxTitle
            // 
            this.lxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lxTitle.BackColor = System.Drawing.Color.White;
            this.lxTitle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lxTitle.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lxTitle.Location = new System.Drawing.Point(579, 28);
            this.lxTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lxTitle.Name = "lxTitle";
            this.lxTitle.Size = new System.Drawing.Size(90, 18);
            this.lxTitle.TabIndex = 15;
            this.lxTitle.Text = "Zoom";
            this.lxTitle.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // offsetY
            // 
            this.offsetY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.offsetY.BackColor = System.Drawing.Color.White;
            this.offsetY.Content = "0";
            this.offsetY.LarguraFixa = false;
            this.offsetY.LarguraTitulo = 100;
            this.offsetY.Location = new System.Drawing.Point(182, 24);
            this.offsetY.Margin = new System.Windows.Forms.Padding(0);
            this.offsetY.Name = "offsetY";
            this.offsetY.Size = new System.Drawing.Size(77, 25);
            this.offsetY.TabIndex = 12;
            this.offsetY.Title = "Y";
            this.offsetY.KeyUpEvent += new System.Action<System.Windows.Forms.KeyEventArgs>(this.offsetY_KeyUpEvent);
            // 
            // offsetX
            // 
            this.offsetX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.offsetX.BackColor = System.Drawing.Color.White;
            this.offsetX.Content = "0";
            this.offsetX.LarguraFixa = false;
            this.offsetX.LarguraTitulo = 100;
            this.offsetX.Location = new System.Drawing.Point(97, 24);
            this.offsetX.Margin = new System.Windows.Forms.Padding(0);
            this.offsetX.Name = "offsetX";
            this.offsetX.Size = new System.Drawing.Size(77, 25);
            this.offsetX.TabIndex = 11;
            this.offsetX.Title = "X";
            this.offsetX.KeyUpEvent += new System.Action<System.Windows.Forms.KeyEventArgs>(this.offsetX_KeyUpEvent);
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Location = new System.Drawing.Point(672, 23);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(1);
            this.panel9.Size = new System.Drawing.Size(69, 27);
            this.panel9.TabIndex = 11;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.White;
            this.panel10.Controls.Add(this.txZoom);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(1, 1);
            this.panel10.Margin = new System.Windows.Forms.Padding(0);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.panel10.Size = new System.Drawing.Size(67, 25);
            this.panel10.TabIndex = 12;
            // 
            // txZoom
            // 
            this.txZoom.BackColor = System.Drawing.Color.White;
            this.txZoom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txZoom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txZoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txZoom.Location = new System.Drawing.Point(5, 2);
            this.txZoom.Margin = new System.Windows.Forms.Padding(0);
            this.txZoom.Multiline = true;
            this.txZoom.Name = "txZoom";
            this.txZoom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txZoom.Size = new System.Drawing.Size(60, 21);
            this.txZoom.TabIndex = 0;
            this.txZoom.Text = "100";
            this.txZoom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txZoom_KeyUp);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(467, 24);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 11;
            this.btSave.Text = "Salvar";
            this.btSave.UseVisualStyleBackColor = true;
            // 
            // PrincipalUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 576);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PrincipalUI";
            this.Text = "Editor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgElements)).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Presser bxField;
        private Presser bxGraphic;
        private Presser btBarcode39;
        private Presser btBarcode128;
        private Presser bxQrcode;
        private Presser bxDatamatrix;
        private Presser bxPìcture;
        private Presser presser1;
        private Presser presser2;
        private LabelText lxProporcao;
        private System.Windows.Forms.Button btSave;
        private Presser prText;
        private Presser prImage;
        private Presser prDatamatrix;
        private Presser prQrCode;
        private Presser prBar128;
        private Presser prBar39;
        private Presser prBox;
        private Presser prSub;
        internal LabelTextAddSelector lxEtiqueta;
        private System.Windows.Forms.Button bxSave;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        internal LabelTextHorizontal offsetX;
        internal LabelTextHorizontal offsetY;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        internal System.Windows.Forms.Panel pnContent;
        private System.Windows.Forms.Label lxTitle;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txZoom;
        private Presser prVinculos;
        private System.Windows.Forms.Label label2;
        private Presser presser3;
        internal System.Windows.Forms.DataGridView dgElements;
        private System.Windows.Forms.Button btExport;
    }
}

