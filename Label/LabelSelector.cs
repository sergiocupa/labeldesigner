

using Label.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace Label
{

    public class SeletorEtiqueta
    {
        public string ID { get; set; }
        public string OrigemDoID { get; set; }

        public List<Etiqueta> Etiquetas { get; set; }

        public SeletorEtiqueta()
        {
            Etiquetas = new List<Etiqueta>();
        }
    }


    public class Etiqueta
    {
        public int ID { get; set; }
        public string NomeArquivoEtiqueta { get; set; }
        public string CaminhoArquivoEtiqueta { get; set; }

        public List<EtiquetaCampoSeletor> CamposSeletor { get; set; }

        public Etiqueta()
        {
            CamposSeletor = new List<EtiquetaCampoSeletor>();
        }

    }

    public class EtiquetaCampoSeletor
    {
        public int ID { get; set; }
        public string CampoNome { get; set; }
        public string ValorSelecao { get; set; }
        public int IdConexaoImpressora { get; set; }
        public string NomeImpressoraOuHostConexao { get; set; }
        public string Formato { get; set; }
        public string TipoDado { get; set; }
        public Etiqueta Parent { get; set; }
    }


    public class CombinacaoGrupo
    {
        public static string Montar(ResultadoExtracao fields, List<CombinacaoGrupo> grupos, Action<string> EventoFalhas)
        {
            StringBuilder sb = new StringBuilder();
            PrintableField printf = null;
            CombinacaoGrupo grupo = null;

            foreach (var _grupo in grupos)
            {
                if (fields.Fields.TryGetValue(_grupo.CampoPrincipal, out printf))
                {
                    grupo = _grupo;
                    break;
                }
            }

            if (grupo != null)
            {
                if (!File.Exists(grupo.LocalArquivo)) { EventoFalhas("Não existe arquivo " + grupo.LocalArquivo); return ""; }
                var text = File.ReadAllText(grupo.LocalArquivo);

                foreach (var frag in grupo.Combinacoes)// Utilizado pra etiquetas indicadoras de eventos, exemplo ERRO
                {
                    if (frag.CampoComum.ValorSelecao == "****")
                    {
                        PrintableField pri = null;
                        if (fields.Fields.TryGetValue(frag.CampoComum.CampoNome, out pri))
                        {
                            if (pri.Data != null)
                            {
                                string ds = pri.Data.ToString();
                                if (!string.IsNullOrEmpty(ds))
                                {
                                    sb.Append(text);
                                    return sb.ToString();
                                }
                            }
                        }
                    }

                    var alvos =
                    (
                        from f in fields.ListaCampos
                        join c in frag.Campos on f.Name equals c.CampoNome
                        where f.Data != null && !string.IsNullOrEmpty(c.ValorSelecao)
                        select f
                    )
                    .GroupBy(g => g.Name).Select(g => g.First()).ToList();

                    var founds =
                    (
                        from s in frag.Campos
                        join f in alvos on new { a = s.CampoNome, b = s.ValorSelecao } equals new { a = f.Name, b = f.Data.ToString() }
                        select s
                    )
                    .Count();

                    if (founds == frag.Campos.Count)
                    {
                        if (!File.Exists(frag.LocalFragmento)) { EventoFalhas("Não existe fragmento " + frag.LocalArquivo); return ""; };

                        var tfrag = File.ReadAllText(frag.LocalFragmento);

                        string te = Combinacao.InserirFragmento(frag, text, tfrag);
                        sb.Append(te);
                    }
                }
            }
            return sb.ToString();
        }


        public CombinacaoGrupo(string id)
        {
            ID = id;
            Combinacoes = new List<Combinacao>();
        }
        private CombinacaoGrupo() { }

        public string ID { get; set; }

        public string CampoPrincipal { get; set; }
        public string EtiquetaBase { get; set; }
        public string LocalArquivo { get; set; }
        public List<Combinacao> Combinacoes { get; set; }
    }


    public class Combinacao
    {

        public string GetTag()
        {
            return "##@Atak.Print.Zpl.Fragment(" + ID + ")##";
        }


        public static string InserirFragmento(Combinacao combi, string etiqueta, string fragmento)
        {
            var TAG = combi.GetTag();
            int start = etiqueta.IndexOf(TAG, StringComparison.Ordinal);
            if (start >= 0)
            {
                string before = etiqueta.Substring(0, start);
                string after = etiqueta.Substring(start + TAG.Length);

                before += fragmento;
                before += after;
                return before;
            }
            return etiqueta;
        }

        public static string InserirTagFragmento(Combinacao combi, CombinacaoGrupo grupo)
        {
            string TAG = combi.GetTag();
            var text = File.ReadAllText(grupo.LocalArquivo);
            int start = text.IndexOf(TAG, StringComparison.Ordinal);

            if (start >= 0)// remove se existe
            {
                string _before = text.Substring(0, start);
                string _after = text.Substring(start + TAG.Length);

                text = _before + _after;
            }

            int position = combi.PosicaoParaInsercao < text.Length ? combi.PosicaoParaInsercao : (text.Length - 1);

            string before = text.Substring(0, position);
            string after = text.Substring(position);

            before += TAG;
            before += after;

            using (StreamWriter wr = new StreamWriter(grupo.LocalArquivo, false, Encoding.Default))
            {
                wr.Write(before);
                wr.Close();
            }
            return before;
        }


        public string Campo { get { return CampoComum != null ? CampoComum.CampoNome : ""; } set { } }
        public string EtiquetaBase { get; set; }
        public string LocalArquivo { get; set; }
        public string Fragmento { get; set; }
        public string LocalFragmento { get; set; }
        public int PosicaoParaInsercao { get; set; }
        public CombinacaoCampo CampoComum { get; set; }
        public List<CombinacaoCampo> Campos { get; set; }
        public string ID { get; set; }
        public string IdGrupo { get; set; }
        public bool IndicacaoGrupo { get; set; }

        public Combinacao()
        {
            Campos = new List<CombinacaoCampo>();
        }
    }


    public class CombinacaoCampo
    {
        public CombinacaoCampo(string id)
        {
            ID = id;
        }

        public CombinacaoCampo() { }

        public string IDCombinacao { get; set; }
        public string ID { get; set; }
        public string CampoNome { get; set; }
        public string ValorSelecao { get; set; }
        public bool EhPrincipal { get; set; }
    }

    public class ResultadoExtracao
    {
        public Dictionary<string, PrintableField> Fields { get; set; }
        public List<PrintableField> ListaCampos { get; set; }

        public ResultadoExtracao()
        {
            Fields = new Dictionary<string, PrintableField>();
            ListaCampos = new List<PrintableField>();
        }
    }
}
