

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace ATAK.Framework.Utils
{

    public class Objecter
    {

        public static void Popular<T>(T source, T target)
        {
            if (source != null && target != null)
            {
                var tSource = source.GetType();
                var props_s = tSource.GetProperties(BindingFlags.Public);

                foreach (var prop in props_s)
                {
                    var va = prop.GetValue(source);
                    prop.SetValue(target, va);
                }
            }
        }


        public static T Populars<T,S>(S source)
        {
            T target = default(T);

            if (source != null)
            {
                var tSource = source.GetType();
                var props_s = tSource.GetProperties(BindingFlags.Public);

                var tTarget = typeof(T);
                var prop_t  = tTarget.GetProperties(BindingFlags.Public);
                target = Activator.CreateInstance<T>();

                foreach (var prop in props_s)
                {
                    var va = prop.GetValue(source);

                    var propT = prop_t.Where(g => g.Name == prop.Name).FirstOrDefault();
                    if (propT != null)
                    {
                        propT.SetValue(target, va);
                    }
                }
            }
            return target;
        }


        public static void Populars<S,T>(S source, T target)
        {
            if (source != null && target != null)
            {
                var tSource = source.GetType();
                var props_s = tSource.GetProperties(BindingFlags.Public);

                var tTarget = target.GetType();
                var prop_t = tTarget.GetProperties(BindingFlags.Public);

                foreach (var prop in props_s)
                {
                    var va = prop.GetValue(source);

                    var propT = prop_t.Where(g => g.Name == prop.Name).FirstOrDefault();
                    if(propT != null)
                    {
                        propT.SetValue(target, va);
                    }
                }
            }
        }


        public static T Copy<T,I>(I source)
        {
            if (source == null) return default(T);

            var tEntrada = typeof(I);
            var tSaida   = typeof(T);

            if(tEntrada.Equals(tSaida))
            {
                if (tSaida.IsValueType)
                {
                    throw new Exception("Não foi implementado");
                    //return default(T); // TO-DO: imoplementar conversor
                }
                else if (source is string)
                {
                    var vg = source as string;
                    var cp = string.Copy(vg);
                    return (T)Convert.ChangeType(cp, tSaida);
                }
                else
                {
                    Dictionary<int, object> instances = new Dictionary<int, object>();
                    var result = (T)_Copy(source, instances);
                    return result;
                }
            }
            else
            {
                throw new Exception("Não foi implementado");
                //return default(T); // TO-DO: implementar copiador com tipo diferente
            }
        }

        public static T Copy<T>(T source)
        {
            if (source == null) return default(T);

            Type tp = source.GetType();
            //if (tp.IsPrimitive && tp.IsValueType)
            if (tp.IsValueType)
            {
                return source;
            }
            else if (source is string)
            {
                var vg = source as string;
                var cp = string.Copy(vg);
                return (T)Convert.ChangeType(cp, tp);
            }
            else
            {
                Dictionary<int, object> instances = new Dictionary<int, object>();
                var result = (T)_Copy(source, instances);
                return result;
            }
        }



        

        private static object _Copy(object source, Dictionary<int, object> instances)
        {
            object new_obj = null;
            if (source == null) return null;
            Type tp = source.GetType();

            //if (tp.IsPrimitive && tp.IsValueType)
            if (tp.IsValueType)
            {
                return source;
            }
            else if (source is string)
            {
                var vg = source as string;
                var cp = string.Copy(vg);
                return cp;
            }
            else if(tp.IsGenericType)
            {
                var novo = CopyList(source, tp, instances);
                return novo;
            }
            else if(tp.IsArray)
            {
                var novo = CopyArray(source, tp, instances);
                return novo;
            }
            else
            {
                var newo = Instanciator(source, tp, instances);
                if (newo.Cancel) return source;
                new_obj = newo.Instance;
                var prop = tp.GetProperties();

                foreach (PropertyInfo pi in prop)
                {
                    var valor = pi.GetValue(source, null);
                    if (valor == null) continue;

                    if (pi.CanRead && pi.CanWrite)
                    {

                        var vr = _Copy(valor, instances);
                        pi.SetValue(new_obj, vr, null);
                        //else if (valor is JObject)
                        //{
                        //    pi.SetValue(new_obj, valor, null);// ??? nao copia
                        //}
                    }
                }
            }
            return new_obj;
        }



        private static object CopyArray(object source, Type tp, Dictionary<int, object> instances)
        {
            Array novo = null;
            if (tp.BaseType.Name == "Array")
            {
                var array = (Array)source;
                var CNT = array.Length;
                var element = tp.GetElementType();
                novo = Array.CreateInstance(element, CNT);

                int i = 0;
                while (i < CNT)
                {
                    var valor_ = array.GetValue(i);
                    var copia = _Copy(valor_, instances);
                    novo.SetValue(copia, i);
                    i++;
                }
            }
            return novo;
        }


        private static object CopyList(object source, Type tp, Dictionary<int, object> instances)
        {
            object newo = null;
            var enumerable = source as IEnumerable;
            if (enumerable != null)
            {
                newo = Activator.CreateInstance(tp, true);
                var addm = tp.GetMethod("Add");
                var prope = tp.GetProperties().Where(g => g.GetIndexParameters().Length > 0).FirstOrDefault();
                if (prope != null)
                {
                    foreach (var item in enumerable)
                    {
                        var copia = _Copy(item, instances);
                        addm.Invoke(newo, new object[] { copia });
                    }
                }
            }
            return newo;
        }



        public static T To<T>(object _input_)
        {
            var TipoRetorno = typeof(T);
            Dictionary<int, object> instances = new Dictionary<int, object>();
            var result = (T)_To(_input_, TipoRetorno, instances);
            return result;
        }



        private static object _To(object _input_, Type TipoRetorno, Dictionary<int, object> instances)
        {
            if (_input_ != null)
            {
                var newo = Instanciator(_input_, TipoRetorno, instances);
                if (newo.Cancel)
                {
                    return newo.Instance;
                }

                var insta        = newo.Instance;
                var tOrigem      = _input_.GetType();
                var tRetorno     = TipoRetorno;
                var propRetorno  = tRetorno.GetProperties();
                var propriedades = tOrigem.GetProperties();
                var nomeRetorno  = propRetorno.Select(g => g.Name).ToArray();
                var propExOrigem = propriedades.Where(g => nomeRetorno.Contains(g.Name)).ToList();

                object obj = null;

                foreach (var item in propExOrigem)
                {
                    var propRetornoAtual = propRetorno.Where(g => g.Name == item.Name).FirstOrDefault();

                    if (propRetornoAtual != null && propRetornoAtual.SetMethod != null)
                    {
                        obj = item.GetValue(_input_);
                        if (obj != null)
                        {
                            var tipoOrigem = obj.GetType();
                            var tipoRetorno = propRetornoAtual.PropertyType;

                            if (!tipoOrigem.IsValueType && !tipoRetorno.IsValueType)
                            {
                                if (tipoOrigem.IsGenericType && tipoRetorno.IsGenericType)
                                {
                                    var enumerable = obj as IEnumerable;
                                    if (enumerable != null)
                                    {
                                        object copyList = null;
                                        try { copyList = Activator.CreateInstance(tipoRetorno, true); }
                                        catch (Exception ex) { throw new Exception("Objecter.To()\r\nNão foi possível instanciar objeto da lista.\r\n" + ex.Message); }

                                        var addm = tipoRetorno.GetMethod("Add");
                                        var argType = tipoRetorno.GetGenericArguments().FirstOrDefault();
                                        var prope = tipoRetorno.GetProperties().Where(g => g.GetIndexParameters().Length > 0).FirstOrDefault();
                                        if (prope != null)
                                        {
                                            foreach (var iteml in enumerable)
                                            {
                                                addm.Invoke(copyList, new object[] { iteml });
                                            }
                                        }
                                        if (propRetornoAtual.CanRead && propRetornoAtual.CanWrite)
                                        {
                                            propRetornoAtual.SetValue(insta, copyList);
                                        }
                                    }
                                }
                                else
                                {
                                    if (tipoRetorno == tipoOrigem)
                                    {
                                        propRetornoAtual.SetValue(insta, obj);
                                    }
                                    else if (tipoRetorno.BaseType == tipoOrigem)
                                    {
                                        var convertido = _To(obj, tipoRetorno, instances);
                                        propRetornoAtual.SetValue(insta, convertido);
                                    }
                                    else
                                    {
                                        propRetornoAtual.SetValue(insta, obj);
                                    }
                                }
                            }
                            else
                            {
                                propRetornoAtual.SetValue(insta, obj);
                            }
                        }
                    }
                }
                return insta;
            }
            else
            {
                return null;
            }
        }



        private static CreateInstanceResult Instanciator(object source, Type type, Dictionary<int, object> instances)
        {
            CreateInstanceResult newo = new CreateInstanceResult();
            try
            {
                object exist;
                int HID = source.GetHashCode();
                if (instances.TryGetValue(HID, out exist))
                {
                    newo.Cancel = true;
                }
                else
                {
                    instances.Add(HID, source);
                    newo.Instance = Activator.CreateInstance(type,true);
                }
            }
            catch (Exception ex) { throw new Exception("Objecter.Copy()\r\nNão foi possível instanciar objeto.\r\n" + ex.Message); }

            return newo;
        }


        public static string CSharpName(Type type)
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


        class CreateInstanceResult
        {
            public bool Cancel { get; set; }
            public object Instance { get; set; }
        }


    }

}
