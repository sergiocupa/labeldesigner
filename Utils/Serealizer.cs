

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Utils
{


    public class Serealizer
    {

        public static void Save<T>(T instance, string local, string sub_pasta, string arquivo)
        {
            string path = local + "/" + (!string.IsNullOrEmpty(sub_pasta) ? (sub_pasta + "/") : "") + arquivo + ".json";

            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.WriteIndented = true;
            var serei = JsonSerializer.Serialize<T>(instance, jso);

            using (StreamWriter wr = new StreamWriter(path, false, Encoding.UTF8))
            {
                wr.Write(serei);
                wr.Close();
            }
        }

        public static void Save<T>(T instance, string sub_pasta, string arquivo)
        {
            var local  = ObterCaminhoExecucao();
            var dir    = local + "/" + (!string.IsNullOrEmpty(sub_pasta) ? (sub_pasta + "/") : "");
            var pathf  = dir + arquivo + ".json";

            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.WriteIndented = true;
            var serei = JsonSerializer.Serialize<T>(instance, jso);

            using (StreamWriter wr = new StreamWriter(pathf, false, Encoding.UTF8))
            {
                wr.Write(serei);
                wr.Close();
            }
        }

        public static void Save<T>(T instance, string sub_pasta)
        {
            var path = ObterLocal<T>(sub_pasta);

            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.WriteIndented = true;
            var serei = JsonSerializer.Serialize<T>(instance, jso);

            using (StreamWriter wr = new StreamWriter(path, false, Encoding.UTF8))
            {
                wr.Write(serei);
                wr.Close();
            }
        }

        public static T Obter<T>(string sub_pasta)
        {
            var path = ObterLocal<T>(sub_pasta);
            var obj = _Obter<T>(path);
            return obj;
        }

        public static T Obter<T>(string local, string sub_pasta, string arquivo)
        {
            string path = local + "/" + (!string.IsNullOrEmpty(sub_pasta) ? (sub_pasta + "/") : "") + arquivo;
            var obj = _Obter<T>(path);
            return obj;
        }

        private static T _Obter<T>(string path)
        {
            if (File.Exists(path))
            {
                string content = "";
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    content = sr.ReadToEnd();
                    sr.Close();
                }
                if (!string.IsNullOrEmpty(content))
                {
                    var obj = JsonSerializer.Deserialize<T>(content);
                    return obj;
                }
                else
                {
                    return Activator.CreateInstance<T>();
                }
            }
            else
            {
                var type = typeof(T);
                var act = (T)Activator.CreateInstance(type, true);
                return act;
            }
        }

        private static string ObterLocal<T>(string sub_pasta)
        {
            var nomeTipo = CSharpName(typeof(T));
            var fname = nomeTipo.Replace("<", "[").Replace(">", "]");

            Assembly ass = Assembly.GetExecutingAssembly();
            var exep = Path.GetDirectoryName(ass.ManifestModule.FullyQualifiedName).Replace("\\", "/");

            var path      = exep + (!string.IsNullOrEmpty(sub_pasta) ? ("/" + sub_pasta) : "");
            var path_file = path + "/" + fname + ".json";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path_file;
        }

        private static string CSharpName(Type type)
        {
            string typeName = type.Name;

            if (type.IsGenericType)
            {
                var genArgs = type.GetGenericArguments();

                if (genArgs.Count() > 0)
                {
                    typeName = typeName.Substring(0, typeName.Length - 2);
                    string args = "";

                    foreach (var argType in genArgs)
                    {
                        string argName = argType.Name;
                        if (argType.IsGenericType) argName = CSharpName(argType);
                        args += argName + ", ";
                    }

                    typeName = string.Format("{0}<{1}>", typeName, args.Substring(0, args.Length - 2));
                }
            }

            return typeName;
        }


        public static string ObterCaminhoExecucao()
        {
            if(CAMINHO_EXECUCAO == null)
            {
                Assembly ass = Assembly.GetExecutingAssembly();
                CAMINHO_EXECUCAO = Path.GetDirectoryName(ass.ManifestModule.FullyQualifiedName).Replace("\\", "/");
            }
            return CAMINHO_EXECUCAO;
            
        }
        private static string CAMINHO_EXECUCAO;




        //public static void Save<T>(T instance, string arquivo, bool se_mesmo_caminho_padrao_nao_salvar)
        //{
        //    var arquivo_padrao = ObterLocal<T>(null, null);

        //    if(se_mesmo_caminho_padrao_nao_salvar)
        //    {
        //        if(!string.IsNullOrEmpty(arquivo) && arquivo_padrao.Equals(arquivo))
        //        {
        //            return;
        //        }
        //    }

        //    var serei = JsonConvert.SerializeObject(instance, Formatting.Indented);
        //    using (StreamWriter wr = new StreamWriter(arquivo, false, Encoding.UTF8))
        //    {
        //        wr.Write(serei);
        //        wr.Close();
        //    }
        //}

        //public static void Save<T>(T instance, string name, string local)
        //{
        //    var arquivo = ObterLocal<T>(local,name);

        //    var serei = JsonConvert.SerializeObject(instance, Formatting.Indented);
        //    using (StreamWriter wr = new StreamWriter(arquivo, false, Encoding.UTF8))
        //    {
        //        wr.Write(serei);
        //        wr.Close();
        //    }
        //}




        //public static ResultadoSerelizacao<T> Obter<T>()
        //{
        //    ResultadoSerelizacao<T> sere = new ResultadoSerelizacao<T>();

        //    string Arquivo = ObterLocal<T>(null,null);
        //    if (!File.Exists(Arquivo))
        //    {
        //        var type = typeof(T);
        //        sere.CreatedFile = true;
        //        sere.Data = (T)Activator.CreateInstance(type, true);

        //        var serei = JsonConvert.SerializeObject(sere.Data, Formatting.Indented);

        //        using (StreamWriter wr = new StreamWriter(Arquivo, false, Encoding.UTF8))
        //        {
        //            wr.Write(serei);
        //            wr.Close();
        //        }
        //    }
        //    else
        //    {
        //        string content = "";
        //        using (StreamReader sr = new StreamReader(Arquivo, Encoding.UTF8))
        //        {
        //            content = sr.ReadToEnd();
        //            sr.Close();
        //        }

        //        if (!string.IsNullOrEmpty(content))
        //        {
        //            sere.Data = JsonConvert.DeserializeObject<T>(content);
        //        }
        //    }
        //    sere.File = Arquivo;
        //    return sere;
        //}




        //private static string ObterLocal<T>(string local, string name)
        //{
        //    string fname = "";
        //    if(!string.IsNullOrEmpty(name))
        //    {
        //        fname = name;
        //    }
        //    else
        //    {
        //        var nomeTipo = CSharpName(typeof(T));
        //        fname = nomeTipo.Replace("<", "[").Replace(">", "]");
        //    }

        //    var caminho = local;
        //    if (string.IsNullOrEmpty(local))
        //    {
        //        Assembly ass = Assembly.GetExecutingAssembly();
        //        caminho = Path.GetDirectoryName(ass.ManifestModule.FullyQualifiedName).Replace("\\", "/");
        //    }

        //    string arquivo = "";
        //    if (!string.IsNullOrEmpty(SUB_PASTA))
        //    {
        //        arquivo = caminho + "/" + SUB_PASTA;

        //        if (!Directory.Exists(arquivo))
        //        {
        //            Directory.CreateDirectory(arquivo);
        //        }

        //        arquivo += "/" + fname + ".json";
        //    }
        //    else
        //    {
        //        arquivo = caminho + "/" + fname + ".json";
        //    }

        //    return arquivo;
        //}



    }



    //public class ResultadoSerelizacao<T>
    //{
    //    public string File { get; set; }
    //    public bool CreatedFile { get; set; }
    //    public T Data { get; set; }
    //    public string Content { get; set; }
    //}
}