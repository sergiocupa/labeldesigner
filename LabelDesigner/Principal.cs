using Label;
using LabelDesigner.Artifacts;
using LabelDesigner.UI;
using LabelDesigner.UI.Editors;
using LabelDesigner.UI.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;

namespace LabelDesigner
{
    public class Principal
    {

        internal void AddFieldVinculado(Artifact row)
        {
            if (row != null)
            {
                FieldAddedToBind.AddElement(row.Element);
            }
        }

        internal void AddVinculacao()
        {
            try
            {
                if (SelectedLabel != null)
                {
                    Vinculacoes vinc = new Vinculacoes(SelectedLabel, this, FieldAddedToBind);
                    vinc.Show(Owner);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        internal void SelecionarAssociados(bool somente_associados)
        {
            try
            {
                if (somente_associados)
                {
                    var sm = Artifacts.Where(f => string.IsNullOrEmpty(f.Element.UidGroup)).ToList();
                }
                else
                {
                    var sm = Artifacts.Where(f => string.IsNullOrEmpty(f.Element.UidGroup)).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        internal void Zoom(int zoom)
        {
            double proporcao = (double)zoom / 100.0;

            if (SelectedLabel != null)
            {
                SelectedLabel.Home.ViewZoom = proporcao;
                SelectedLabel.Save();
                RefreshUi();
            }
        }


        internal void PosicaoAlteradaX(string conteudo)
        {
            try
            {
                int x = 0;
                if (int.TryParse(conteudo, out x))
                {
                    if (SelectedLabel != null)
                    {
                        SelectedLabel.Home.X = x;
                        SelectedLabel.Save();
                        RefreshUi();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        internal void PosicaoAlteradaY(string conteudo)
        {
            try
            {
                int y = 0;
                if (int.TryParse(conteudo, out y))
                {
                    if (SelectedLabel != null)
                    {
                        SelectedLabel.Home.Y = y;
                        SelectedLabel.Save();
                        RefreshUi();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        internal void Select(Artifact art)
        {
            SelectedArt = art;

            foreach (DataGridViewRow r in Owner.dgElements.Rows)
            {
                var a = (Artifact)r.DataBoundItem;
                a.UI.ResetBack();
            }

            if (SelectedArt != null)
            {
                SelectedArt.UI.SelectedBack();
            }
        }

        internal void Delete()
        {
            try
            {
                if (SelectedLabel != null)
                {
                    if (SelectedArt != null)
                    {
                        var element = SelectedLabel.Elements.Where(w => w.UID == SelectedArt.Element.UID).FirstOrDefault();
                        if (element != null)
                        {
                            SelectedLabel.Elements.Remove(element);
                            SelectedLabel.Save();

                            Artifacts.Remove(SelectedArt);
                            SelectedArt = null;

                            foreach (Control ui in Owner.pnContent.Controls)
                            {
                                var uip = (Artifact)ui.Tag;
                                if (uip.Element.UID == element.UID)
                                {
                                    Owner.pnContent.Controls.Remove(ui);
                                    break;
                                }
                            }

                            Owner.AtualizarGrid(Artifacts);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        internal void Save()
        {
            try
            {
                if (SelectedLabel != null)
                {
                    SelectedLabel.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        internal void AddEtiqueta()
        {
            try
            {
                SelecaoNomeValorUI descrivaoUI = new SelecaoNomeValorUI
                (
                    "Etiqueta",
                    "Identificador", "",
                    "Nome", ""
                );
                var ok = descrivaoUI.ShowDialog(Owner);

                if (ok == DialogResult.OK)
                {
                    SelectedLabel = new PrintLabel();
                    SelectedLabel.UID = descrivaoUI.Nome;
                    SelectedLabel.Name = descrivaoUI.Valor;
                    SelectedLabel.FileName = descrivaoUI.Nome;

                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    var result = fbd.ShowDialog(Owner);
                    if (result == DialogResult.OK)
                    {
                        SelectedLabel.Local = fbd.SelectedPath;
                        SelectedLabel.Save();

                        Owner.lxEtiqueta.Content = SelectedLabel.UID + " - " + SelectedLabel.Name;
                        Owner.AtivarEdicao(true);
                        RenderizarElementos(null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void SelecionarEtiqueta()
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                var r = fd.ShowDialog();
                if (r == DialogResult.OK)
                {
                    var path = fd.FileName.Replace(fd.SafeFileName, "");
                    path = path.Replace("\\", "/");
                    if (path.Last() == '/') path = path.Substring(0, (path.Length - 1));

                    SelectedLabel = PrintLabel.Load(path, fd.SafeFileName);

                    Owner.lxEtiqueta.Content = SelectedLabel.UID + " - " + SelectedLabel.Name;
                    Owner.AtivarEdicao(true);
                    RenderizarElementos(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        internal void RefreshUi()
        {
            try
            {
                if (SelectedLabel != null)
                {
                    Owner.pnContent.SuspendLayout();

                    foreach (Control ui in Owner.pnContent.Controls)
                    {
                        ui.SuspendLayout();
                        var uip = (Artifact)ui.Tag;
                        uip.Refresh();
                        ui.ResumeLayout();
                    }

                    Owner.pnContent.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        internal void RenderizarElementos(List<LabelElement> elements)
        {
            Owner.pnContent.Controls.Clear();
            Owner.AtualizarGrid(null);
            Artifacts.Clear();

            if (SelectedLabel != null)
            {
                Owner.offsetX.Content = SelectedLabel.Home.X.ToString();
                Owner.offsetY.Content = SelectedLabel.Home.Y.ToString();
                Owner.txZoom.Text = (SelectedLabel.Home.ViewZoom * 100.0).ToString();

                if (SelectedLabel.Elements.Count > 0)
                {
                    List<LabelElement> eles = null;

                    if (elements != null) eles = elements;
                    else eles = SelectedLabel.Elements;

                    var textos = eles.Where(w => w.ElementType == LabelElementType.PRINTER_FONT || w.ElementType == LabelElementType.RENDERED_TEXT).ToList();
                    var outros = eles.Where(w => !(w.ElementType == LabelElementType.PRINTER_FONT || w.ElementType == LabelElementType.RENDERED_TEXT)).ToList();

                    foreach (var ele in textos)
                    {
                        if (ele.ElementType == LabelElementType.PRINTER_FONT)
                        {
                            AddText(ele);
                        }
                        else
                        {
                            AddRenderedText(ele);
                        }
                    }

                    foreach (var ele in outros)
                    {
                        switch (ele.ElementType)
                        {
                            case LabelElementType.PRELOADED_IMAGE:
                                {
                                    AddImage(ele);
                                }
                                break;
                            case LabelElementType.CODIGO_BARRA_39:
                                {
                                    AddBarcode39(ele);
                                }
                                break;
                            case LabelElementType.CODIGO_BARRA_128:
                                {
                                    AddBarcode128(ele);
                                }
                                break;
                            case LabelElementType.QR_CODE:
                                {
                                    AddQrcode(ele);
                                }
                                break;
                            case LabelElementType.DATAMATRIX:
                                {
                                    AddDatamatrix(ele);
                                }
                                break;
                            case LabelElementType.GRAPHIC_BOX:
                                {
                                    AddGraphic(ele);
                                }
                                break;
                        }
                    }

                    Owner.AtualizarGrid(Artifacts);
                }
            }
        }



        internal void SelectedEdit(Artifact art)
        {
            try
            {
                var txt = art as TextElement;
                if (txt != null)
                {
                    OpenTextEditor(txt);
                }
                else
                {
                    var box = art as BoxElement;
                    if (box != null)
                    {
                        OpenBoxEditor(box);
                    }
                    else
                    {
                        var code39 = art as Barcode39Element;
                        if (code39 != null)
                        {
                            OpenBarcode39Editor(code39);
                        }
                        else
                        {
                            var qrcode = art as QrCodeElement;
                            if (qrcode != null)
                            {
                                OpenQrCodeEditor(qrcode);
                            }
                            else
                            {
                                var datamatrix = art as DatamatrixElement;
                                if (datamatrix != null)
                                {
                                    OpenDatamatrixEditor(datamatrix);
                                }
                                else
                                {
                                    var image = art as ImageElement;
                                    if (image != null)
                                    {
                                        OpenImageEditor(image);
                                    }
                                    else
                                    {
                                        var render = art as RenderedTextElement;
                                        if (render != null)
                                        {
                                            OpenRenderedTextEditor(render);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        internal void AddControl(Artifact art, bool refresh_grid)
        {
            Owner.pnContent.Controls.Add((Control)art.UI);
            Artifacts.Add(art);

            if (refresh_grid)
            {
                Owner.AtualizarGrid(Artifacts);
            }
        }


        private void SelecionarLinha(Artifact t)
        {
            try
            {
                Owner.dgElements.ClearSelection();
                if (t.Row != null)
                {
                    ((DataGridViewRow)t.Row).Selected = true;
                    Select(t);
                }
            }
            catch (Exception)
            {
            }

        }


        internal void AddRenderedText(LabelElement ele)
        {
            var te = new RenderedTextElement(ele, SelectedLabel, OpenRenderedTextEditor, SelecionarLinha);
            AddControl(te, (ele == null));
        }

        internal void AddText(LabelElement ele)
        {
            var te = new TextElement(ele, SelectedLabel, OpenTextEditor, SelecionarLinha);
            AddControl(te, (ele == null));
        }

        internal void AddGraphic(LabelElement ele)
        {
            var te = new BoxElement(ele, SelectedLabel, OpenBoxEditor, SelecionarLinha);
            AddControl(te, (ele == null));
        }

        internal void AddBarcode39(LabelElement ele)
        {
            var te = new Barcode39Element(ele, SelectedLabel, OpenBarcode39Editor, SelecionarLinha);
            AddControl(te, (ele == null));
        }

        internal void AddBarcode128(LabelElement ele)
        {
            var te = new Barcode128Element(ele, SelectedLabel, OpenBarcode128Editor, SelecionarLinha);
            AddControl(te, (ele == null));
        }

        internal void AddQrcode(LabelElement ele)
        {
            var te = new QrCodeElement(ele, SelectedLabel, OpenQrCodeEditor, SelecionarLinha);
            AddControl(te, (ele == null));
        }

        internal void AddDatamatrix(LabelElement ele)
        {
            var te = new DatamatrixElement(ele, SelectedLabel, OpenDatamatrixEditor, SelecionarLinha);
            AddControl(te, (ele == null));
        }



        internal void OpenImageEditor(Artifact t)
        {
            var im = new EditorImagem("");
            var result = im.ShowDialog(Owner);
            if (result == DialogResult.OK)
            {
                t.Element.Size.Height = im.Edicao.Matrix.GetLength(0);
                t.Element.Size.Width = im.Edicao.Matrix.GetLength(1);
                t.Element.Rectangles = ImageConverter.MatrixCompressRectangles(im.Edicao.Matrix);

                t.Element.Image = new CompressedImage()
                {
                    CompressedData = ImageConverter.RectanglesToCompressedBase64(t.Element.Rectangles),
                    PrintCommand = ImageConverter.CommandToBase64(im.Edicao.ZplContent)
                };

                SelectedLabel.Save();
                t.Refresh();

                Owner.AtualizarGrid(Artifacts);
            }
        }

        internal void AddImage(LabelElement ele)
        {
            var ima = new ImageElement(ele, SelectedLabel, OpenImageEditor, SelecionarLinha);
            AddControl(ima, (ele == null));
        }



        internal void OpenRenderedTextEditor(Artifact t)
        {
            var editor = new RenderedTextFieldEditor(t.Element);
            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                editor.UpdateElementData();
                if (SelectedLabel != null) SelectedLabel.Save();
                t.Refresh();
            }
        }

        internal void OpenTextEditor(Artifact t)
        {
            var editor = new TextFieldEditor(t.Element);
            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                editor.UpdateElementData();
                if (SelectedLabel != null) SelectedLabel.Save();
                t.Refresh();
            }
        }

        internal void OpenBoxEditor(Artifact t)
        {
            var editor = new GraphicEditor(t.Element);
            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                editor.UpdateElementData();
                if (SelectedLabel != null) SelectedLabel.Save();
                t.Refresh();
            }
        }

        internal void OpenBarcode39Editor(Artifact art)
        {
            var editor = new BarcodeEditor(art.Element);
            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                editor.UpdateElementData();
                if (SelectedLabel != null) SelectedLabel.Save();
                art.Refresh();
            }
        }

        internal void OpenBarcode128Editor(Artifact t)
        {
            var editor = new BarcodeEditor(t.Element);
            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                editor.UpdateElementData();
                if (SelectedLabel != null) SelectedLabel.Save();
                t.Refresh();
            }
        }

        internal void OpenQrCodeEditor(Artifact t)
        {
            var editor = new QrCodeEditor(t.Element);
            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                editor.UpdateElementData();
                if (SelectedLabel != null) SelectedLabel.Save();
                t.Refresh();
            }
        }

        internal void OpenDatamatrixEditor(Artifact t)
        {
            var editor = new DatamatrixEditor(t.Element);
            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                editor.UpdateElementData();
                if (SelectedLabel != null) SelectedLabel.Save();
                t.Refresh();
            }
        }


        private VinculacoesFieldInvoker FieldAddedToBind;
        private PrintLabel SelectedLabel;
        private PrincipalUI Owner;
        private List<Artifact> Artifacts;
        private Artifact SelectedArt;

        public Principal(PrincipalUI owner)
        {
            Owner = owner;

            Artifacts = new List<Artifact>();

            FieldAddedToBind = new VinculacoesFieldInvoker();
        }

    }
}
