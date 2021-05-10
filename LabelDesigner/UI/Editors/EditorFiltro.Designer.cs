using LabelDesigner.UI.Templates;

namespace LabelDesigner.UI.Editors
{
    partial class EditorFiltro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorFiltro));
            this.dgCampos = new System.Windows.Forms.DataGridView();
            this.bxAddEtiqueta = new Presser();
            this.btExcluir = new Presser();
            this.btUtilizar = new Presser();
            ((System.ComponentModel.ISupportInitialize)(this.dgCampos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgCampos
            // 
            this.dgCampos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCampos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCampos.Location = new System.Drawing.Point(12, 36);
            this.dgCampos.Name = "dgCampos";
            this.dgCampos.Size = new System.Drawing.Size(315, 448);
            this.dgCampos.TabIndex = 10;
            this.dgCampos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCampos_CellDoubleClick);
            // 
            // bxAddEtiqueta
            // 
            this.bxAddEtiqueta.BackColor = System.Drawing.Color.White;
            this.bxAddEtiqueta.EnabledBorder = false;
            
            this.bxAddEtiqueta.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bxAddEtiqueta.Label = "";
            this.bxAddEtiqueta.Location = new System.Drawing.Point(12, 16);
            this.bxAddEtiqueta.Margin = new System.Windows.Forms.Padding(0);
            this.bxAddEtiqueta.Name = "bxAddEtiqueta";
            this.bxAddEtiqueta.Size = new System.Drawing.Size(18, 18);
            this.bxAddEtiqueta.TabIndex = 19;
            this.bxAddEtiqueta.Click += new System.EventHandler(this.bxAddEtiqueta_Click);
            // 
            // btExcluir
            // 
            this.btExcluir.BackColor = System.Drawing.Color.White;
            this.btExcluir.EnabledBorder = false;
            
            this.btExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btExcluir.Label = "";
            this.btExcluir.Location = new System.Drawing.Point(33, 16);
            this.btExcluir.Margin = new System.Windows.Forms.Padding(0);
            this.btExcluir.Name = "btExcluir";
            this.btExcluir.Size = new System.Drawing.Size(18, 18);
            this.btExcluir.TabIndex = 20;
            this.btExcluir.Click += new System.EventHandler(this.btExcluir_Click);
            // 
            // btUtilizar
            // 
            this.btUtilizar.BackColor = System.Drawing.Color.White;
            this.btUtilizar.EnabledBorder = true;
            this.btUtilizar.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.btUtilizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(255)))));
            this.btUtilizar.Image = null;
            this.btUtilizar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btUtilizar.Label = "OK";
            this.btUtilizar.Location = new System.Drawing.Point(267, 15);
            this.btUtilizar.Margin = new System.Windows.Forms.Padding(0);
            this.btUtilizar.Name = "btUtilizar";
            this.btUtilizar.Size = new System.Drawing.Size(60, 18);
            this.btUtilizar.TabIndex = 76;
            this.btUtilizar.Click += new System.EventHandler(this.btUtilizar_Click);
            // 
            // EditorFiltro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(339, 497);
            this.Controls.Add(this.btUtilizar);
            this.Controls.Add(this.bxAddEtiqueta);
            this.Controls.Add(this.btExcluir);
            this.Controls.Add(this.dgCampos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditorFiltro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editor de Filtro";
            ((System.ComponentModel.ISupportInitialize)(this.dgCampos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgCampos;
        private Presser bxAddEtiqueta;
        private Presser btExcluir;
        private Presser btUtilizar;
    }
}