

using Label.Model;
using System.Collections.Generic;
using System.Linq;
using Utils;


namespace Label.Model
{

   


    public class PrintLabel
    {

        public LabelPosition Home { get; set; }

        public string UID { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Local { get; set; }
        public List<LabelElement> Elements { get; set; }
        public string Content { get; set; }
        public string FixedContent { get; set; }
        public List<AlternativeBindingGroup> AlternativeBindings { get; set; }


        public PrintLabel()
        {
            Elements            = new List<LabelElement>();
            AlternativeBindings = new List<AlternativeBindingGroup>();
            Home                = new LabelPosition();

            if (Home.ViewZoom <= 0) Home.ViewZoom = 1;
        }


        public void Save()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                throw new System.Exception("Não foi definido arquivo para salvar etiqueta");
            }

            Serealizer.Save(this, LABELS_HOME, FileName);
            SaveDefault();
        }

        private void SaveDefault()
        {
            if (!string.IsNullOrEmpty(Local) && !string.IsNullOrEmpty(FileName))
            {
                var exec         = Serealizer.ObterCaminhoExecucao();
                var path_default = exec + LABELS_HOME + "/" + FileName;
                var path_local   = Local + LABELS_HOME + "/" + FileName;

                if (!path_default.Equals(path_local))
                {
                    Serealizer.Save(this, Local, null, FileName);
                }
            }
            else
            {
                throw new System.Exception("Não foi definido arquivo para salvar etiqueta");
            }
        }


        public static PrintLabel Load()
        {
            var result = Serealizer.Obter<PrintLabel>(LABELS_HOME);
            var label = Preparar(result);
            return label;
        }

        public static PrintLabel Load(string path, string file)
        {
            var result = Serealizer.Obter<PrintLabel>(path, null, file);
            var label  = Preparar(result);
            return label;
        }

        private static PrintLabel Preparar(PrintLabel label)
        {
            if (label.Home.ViewZoom <= 0.0) label.Home.ViewZoom = 1;
            if (label.AlternativeBindings == null) label.AlternativeBindings = new List<AlternativeBindingGroup>();

            if (label.Elements != null && label.Elements.Count > 0)
            {
                foreach (var item in label.Elements)
                {
                    if (item.Image != null)
                    {
                        item.Rectangles = ImageConverter.CompressedBase64ToRectangles(item.Image.CompressedData);
                    }

                    item.Home = label.Home;
                }
            }
            return label;
        }


        static PrintLabel()
        {
            LABELS_HOME = "etiquetas";
        }

        public static readonly string LABELS_HOME;
    }




    public class AlternativeBindingGroup
    {

        public List<AlternativeBindingGroupCampoFiltro> CamposFiltro { get; set; }

        public string UID { get; set; }


        public string ValoresCampos
        {
            get
            {
                string saida = "";
                if (CamposFiltro != null)
                {
                    saida = string.Join(", ",  CamposFiltro.Select(g => g.CampoNome + " = " + g.ValorSelecao));
                }
                return saida;
            }
            set { }
        }

        public AlternativeBindingGroup()
        {
            CamposFiltro = new List<AlternativeBindingGroupCampoFiltro>();
        }
    }


    public class LabelElementIDControl
    {


        public static string GetElementImageName()
        {
            var id = GetElementImageID();
            return id.ToString("00000000");
        }



        public static long GetElementImageID()
        {
            var atual = LabelElementIDControl.Load();

            if (atual.ElementImageID >= MAX)
            {
                atual.ElementImageID = 0;
            }

            atual.ElementImageID++;
            atual.Save();
            return atual.ElementImageID;
        }


        private static LabelElementIDControl Load()
        {
            var atual = Serealizer.Obter<LabelElementIDControl>(PrintLabel.LABELS_HOME);
            return atual;
        }


        private void Save()
        {
            Serealizer.Save(this, PrintLabel.LABELS_HOME);
        }

        private static long MAX = 99999999;

        public long ElementImageID { get; set; }
    }

    public class AlternativeBindingGroupCampoFiltro
    {
        public int ID { get; set; }
        public string IdGrupo { get; set; }
        public string CampoNome { get; set; }
        public string ValorSelecao { get; set; }
    }
}
