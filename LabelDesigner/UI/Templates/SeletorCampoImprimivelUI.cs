

using Label;
using Label.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace LabelDesigner.UI.Templates
{
    public partial class SeletorCampoImprimivelUI : Form
    {



        void PrepararGrid()
        {
            DataGridViewTemplate.EsquemaBrancoLinhaAlternada(dgCampos);
            dgCampos.Columns.Add(DataGridViewTemplate.CriarColunaTexto("Name", "Nome"));
            dgCampos.Columns.Add(DataGridViewTemplate.CriarColunaTexto("Format", "Formato"));
            dgCampos.Columns.Add(DataGridViewTemplate.CriarColunaTexto("TypeName", "Tipo", 140));

            dgCampos.DataSource = Fields;

            dgCampos.SuspendLayout();
            foreach(var item in Map)
            {
                Fields.Add(item);
            }
            dgCampos.Refresh();
            dgCampos.ResumeLayout();
        }


        public PrintableField Selecionado { get; private set; }


        List<PrintableField> Map;
        BindingList<PrintableField> Fields;
        bool ModoEdicao;


        public SeletorCampoImprimivelUI(bool modoEdicao)
        {
            InitializeComponent();

            ModoEdicao = modoEdicao;

            //var map = PrintableFieldAttUtil.ObterCamposImprimiveis<VolumeLinhaProducaoPrint>();
            //Map = map.OrderBy(g => g.Name).ToList();

            Fields = new BindingList<PrintableField>();

            PrepararGrid();
        }

        private void dgCampos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(dgCampos.SelectedRows.Count > 0)
                {
                    var sel     = (PrintableField)dgCampos.SelectedRows[0].DataBoundItem;
                    Selecionado = sel;

                    if(!ModoEdicao)
                    {
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (System.Exception)
            {

            }
        }

    }
}
