

using Label;
using Label.Model;
using LabelDesigner.UI.Templates;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;


namespace LabelDesigner.UI.Editors
{
    public partial class Vinculacoes : Form
    {

        private void cxSelecionar_CheckedChanged(object sender, EventArgs e)
        {
            if(!cxSelecionar.Checked)
            {
                Owner.RenderizarElementos(null);
            }
            else
            {
                if(SelectedGroup != null)
                {
                    AtualizarGrid_Elements();
                    var ele = Label.Elements.Where(w => w.UidGroup == SelectedGroup.UID).ToList();
                    Owner.RenderizarElementos(ele);
                }
            }
        }
        

        private void dgLigacoes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgLigacoes.SelectedRows.Count > 0)
            {
                SelectedGroup = (AlternativeBindingGroup)dgLigacoes.SelectedRows[0].DataBoundItem;
                AtualizarGrid_Elements();

                if (cxSelecionar.Checked)
                {
                    var ele = Label.Elements.Where(w => w.UidGroup == SelectedGroup.UID).ToList();
                    Owner.RenderizarElementos(ele);
                }
            }
        }


        private void prAddVinculacao_Click(object sender, EventArgs e)
        {
            IncluirVinculador();
        }

        private void __FieldAdded(LabelElement element)
        {
            try
            {
                if (Label == null) return;
                if (SelectedGroup == null) { MessageBox.Show("Selecione um grupo"); return; }

                var ele = Label.Elements.Where(w => w.UID == element.UID).FirstOrDefault();
                if(ele != null)
                {
                    ele.UidGroup = SelectedGroup.UID;
                    Label.Save();

                    AtualizarGrid_Elements();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void prSub_Click(object sender, EventArgs e)
        {
            try
            {
                if((SelectedGroup != null) && (Label != null))
                {
                    var g = Label.AlternativeBindings.Where(w => w.UID == SelectedGroup.UID).FirstOrDefault();
                    if (g != null)
                    {
                        var eles = Label.Elements.Where(w => w.UidGroup == g.UID).ToList();
                        eles.ForEach(f => f.UidGroup = null);

                        Label.AlternativeBindings.Remove(g);
                        Label.Save();

                        dgLigacoes.SuspendLayout();
                        BGroups.Remove(SelectedGroup);
                        SelectedGroup = null;
                        dgLigacoes.Refresh();
                        dgLigacoes.ResumeLayout();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void prRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgCampos.SelectedRows.Count > 0)
                {
                    if (Label != null && SelectedGroup != null)
                    {
                        var row = (LabelElement)dgCampos.SelectedRows[0].DataBoundItem;
                        var ele = Label.Elements.Where(w => w.UID == row.UID).FirstOrDefault();
                        if(ele != null)
                        {
                            ele.UidGroup = null;
                            Label.Save();

                            dgCampos.SuspendLayout();
                            BElements.Remove(row);
                            dgCampos.Refresh();
                            dgCampos.ResumeLayout();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }



        private void IncluirVinculador()
        {
            try
            {
                if (Label == null)
                {
                    MessageBox.Show("Selecione uma etiqueta"); return;
                }

                EditorFiltro filtro = new EditorFiltro(null);
                var result = filtro.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    AlternativeBindingGroup gr = new AlternativeBindingGroup();
                    gr.UID = Guid.NewGuid().ToString();

                    gr.CamposFiltro = filtro.GetCampos();
                    gr.CamposFiltro.ForEach(f => f.IdGrupo = gr.UID);

                    Label.AlternativeBindings.Add(gr);
                    Label.Save();

                    AtualizarGrid_FonteAlternativa();
                }
            }
            catch (Exception ex)
            {

            }
        }



        private void AtualizarGrid_FonteAlternativa()
        {
            dgLigacoes.SuspendLayout();
            BGroups.Clear();

            if(Label != null)
            {
                foreach (var item in Label.AlternativeBindings)
                {
                    BGroups.Add(item);
                }
            }

            dgLigacoes.Refresh();
            dgLigacoes.ResumeLayout();
        }

        private void AtualizarGrid_Elements()
        {
            dgCampos.SuspendLayout();
            BElements.Clear();

            if (Label != null && SelectedGroup != null)
            {
                var ele = Label.Elements.Where(w => w.UidGroup == SelectedGroup.UID).ToList();

                foreach (var item in ele)
                {
                    BElements.Add(item);
                }
            }

            dgCampos.Refresh();
            dgCampos.ResumeLayout();
        }
       


        private void PrepararGrid()
        {
            DataGridViewTemplate.EsquemaBrancoLinhaAlternada(dgLigacoes);
            dgLigacoes.Columns.Add(DataGridViewTemplate.CriarColunaTexto("ValoresCampos", "Valores para o filtro"));
            dgLigacoes.DataBindingComplete += (object sender, DataGridViewBindingCompleteEventArgs e) => { dgLigacoes.ClearSelection(); };
            dgLigacoes.DataSource = BGroups;

            DataGridViewTemplate.EsquemaBrancoLinhaAlternada(dgCampos);
            dgCampos.Columns.Add(DataGridViewTemplate.CriarColunaTexto("ElementType", "Tipo"));
            dgCampos.Columns.Add(DataGridViewTemplate.CriarColunaTexto("Value", "Conteúdo"));
            dgCampos.DataBindingComplete += (object sender, DataGridViewBindingCompleteEventArgs e) => { dgCampos.ClearSelection(); };
            dgCampos.DataSource = BElements;
        }


        private PrintLabel Label;
        BindingList<AlternativeBindingGroup> BGroups;
        BindingList<LabelElement> BElements;
        VinculacoesFieldInvoker FieldAdded;
        AlternativeBindingGroup SelectedGroup;
        Principal Owner;


        public Vinculacoes(PrintLabel label, Principal owner, VinculacoesFieldInvoker fieldAdded)
        {
            InitializeComponent();

            BGroups    = new BindingList<AlternativeBindingGroup>();
            BElements  = new BindingList<LabelElement>();
            Label      = label;
            Owner      = owner;
            FieldAdded = fieldAdded;

            FieldAdded.SetAction(__FieldAdded);

            PrepararGrid();
            AtualizarGrid_FonteAlternativa();
        }

        private void Vinculacoes_FormClosing(object sender, FormClosingEventArgs e)
        {
            FieldAdded.SetAction(null);

            try
            {
                Owner.RenderizarElementos(null);
            }
            catch (Exception ex)
            {

            }
        }


    }

    public class VinculacoesFieldInvoker
    {
        public void AddElement(LabelElement element)
        {
            if(_Action != null)
            {
                _Action(element);
            }
        }

        public void SetAction(Action<LabelElement> action)
        {
            _Action = action;
        }

        private Action<LabelElement> _Action;
    }
}
