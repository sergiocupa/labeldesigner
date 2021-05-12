

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace LabelDesigner.UI.Templates
{

    [DefaultEvent("MouseDoubleClick")]
    public class MultiSelectTreeView : TreeView
    {


        private void trView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            bool control = (ModifierKeys == Keys.Control);
            bool shift = (ModifierKeys == Keys.Shift);

            if (control)
            {
                SelectedNodes.Add(e.Node);
                e.Node.BackColor = SELECTED;
                e.Node.ForeColor = Color.White;
            }
            else if (shift)
            {
                if (PreviousNode != null)
                {
                    if (PreviousNode.Index > e.Node.Index)
                    {
                        foreach (TreeNode i in Nodes)
                        {
                            if (i.Index >= e.Node.Index && i.Index <= PreviousNode.Index)
                            {
                                SelectedNodes.Add(e.Node);
                                i.BackColor = SELECTED;
                                i.ForeColor = Color.White;
                            }
                            if (i.Index > PreviousNode.Index) break;
                        }
                    }
                    else if (PreviousNode.Index < e.Node.Index)
                    {
                        foreach (TreeNode i in Nodes)
                        {
                            if (i.Index >= PreviousNode.Index && i.Index <= e.Node.Index)
                            {
                                SelectedNodes.Add(e.Node);
                                i.BackColor = SELECTED;
                                i.ForeColor = Color.White;
                            }
                            if (i.Index > e.Node.Index) break;
                        }
                    }
                    else
                    {
                        SelectedNodes.Add(e.Node);
                        e.Node.BackColor = SELECTED;
                        e.Node.ForeColor = Color.White;
                    }
                }
                else
                {
                    SelectedNodes.Add(e.Node);
                    e.Node.BackColor = SELECTED;
                    e.Node.ForeColor = Color.White;
                }
            }
            else
            {
                foreach (TreeNode i in Nodes)
                {
                    i.BackColor = Color.White;
                    i.ForeColor = Color.Black;
                }

                SelectedNodes.Clear();
                SelectedNodes.Add(e.Node);
                e.Node.BackColor = SELECTED;
                e.Node.ForeColor = Color.White;
            }

            PreviousNode = e.Node;
        }


        private void _NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(MouseDoubleClick != null)
            {
                MouseDoubleClick(e);
            }
        }
        private void _NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (MouseClick != null)
            {
                MouseClick(e);
            }
        }


        public List<TreeNode> SelectedNodes { get; private set; }

        public event Action<TreeNodeMouseClickEventArgs> MouseDoubleClick;
        public event Action<TreeNodeMouseClickEventArgs> MouseClick;
        private TreeNode PreviousNode;
        private Color SELECTED = Color.FromArgb(120, 160, 255);


        public MultiSelectTreeView()
        {
            SelectedNodes = new List<TreeNode>();

            NodeMouseDoubleClick += _NodeMouseDoubleClick;
            NodeMouseClick       += _NodeMouseClick;
        }

    }
}
