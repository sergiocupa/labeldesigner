

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace LabelDesigner.UI.Templates
{

    [DefaultEvent("KeyUpEvent")]
    public partial class LabelTextHorizontal : UserControl
    {

        private void AjustarLargura()
        {
            if (_LarguraFixa)
            {
                table.ColumnStyles[0].Width = _LarguraTitulo;
            }
            else
            {
                Graphics gr = Graphics.FromImage(new Bitmap(1, 1));
                var s = gr.MeasureString(lxTitle.Text, lxTitle.Font, new Point(0, 0), StringFormat.GenericDefault);
                table.ColumnStyles[0].Width = s.Width + 6;
            }
        }


        public string Content { get { return txContent.Text; } set { txContent.Text = value; } }


        public string Title 
        {
            get { return lxTitle.Text; }
            set
            {
                lxTitle.Text = value;
                AjustarLargura();
            } 
        }

        public int LarguraTitulo
        {
            get { return _LarguraTitulo; } 
            set 
            { 
                _LarguraTitulo = value;
                AjustarLargura();
            }
        }

        public bool LarguraFixa 
        { 
            get { return _LarguraFixa; } 
            set 
            {
                _LarguraFixa = value;
                AjustarLargura();
            }
        }


        private void txContent_KeyUp(object sender, KeyEventArgs e)
        {
            if (KeyUpEvent != null) KeyUpEvent(e);
        }
        public event Action<KeyEventArgs> KeyUpEvent;


        private int _LarguraTitulo;
        private bool _LarguraFixa;

        public LabelTextHorizontal()
        {
            _LarguraFixa   = true;
            _LarguraTitulo = 100;

            InitializeComponent();

            txContent.KeyUp += txContent_KeyUp;
        }

    }

}
