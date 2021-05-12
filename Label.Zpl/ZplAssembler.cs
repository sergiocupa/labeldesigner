

using Label.Model;
using System;
using System.Text;


namespace Label.Zpl
{

    public class ZplAssembler : ILabelAssembler
    {


        public string AssembleLabel(PrintLabel label)
        {
            StringBuilder sb = new StringBuilder();

            foreach(var element in label.Elements)
            {
                var field       = new PrintableField();
                field.Data      = element.Value;
                field.Formatter = Formatter.GetFormatter(typeof(string));

                AssembleElement(element, field, sb);
            }

            return sb.ToString();
        }

        // Implementacao de montagem com fonte de dados por instancia de objeto informado
        public string AssembleLabel(PrintLabel label, object data)
        {
            throw new NotImplementedException();
        }


        public void AssembleElement(LabelElement element, PrintableField field, StringBuilder sb)
        {
            switch (element.ElementType)
            {
                case LabelElementType.PRINTER_FONT:
                    {
                        Text(element, field, sb);
                    }
                    break;
                case LabelElementType.RENDERED_TEXT:
                case LabelElementType.PRELOADED_IMAGE:
                    {
                        Rendered(element, sb);
                    }
                    break;
                case LabelElementType.CODIGO_BARRA_39:
                    {
                        Barcode39(element, field, sb);
                    }
                    break;
                case LabelElementType.CODIGO_BARRA_128:
                    {
                        Barcode128(element, field, sb);
                    }
                    break;
                case LabelElementType.QR_CODE:
                    {
                        Qrcode(element, field, sb);
                    }
                    break;
                case LabelElementType.DATAMATRIX:
                    {
                        Datamatrix(element, field, sb);
                    }
                    break;
                case LabelElementType.GRAPHIC_BOX:
                    {
                       Box(element, sb);
                    }
                    break;
            }
        }




        private static void Box(LabelElement element, StringBuilder sbuilder)
        {
            _FOT(element, sbuilder);
            _GB(element, sbuilder);
            _FS(sbuilder);
        }
        private static void Text(LabelElement element, PrintableField field, StringBuilder sbuilder)
        {
            _FOT(element, sbuilder);
            _A(element, sbuilder);
            _Data(element, field, sbuilder);
            _FS(sbuilder);
        }
        private void Rendered(LabelElement element, StringBuilder sbuilder)
        {
            _FOT(element, sbuilder);
            _XG(element, sbuilder);
            _FS(sbuilder);
        }
        private void Barcode39(LabelElement element, PrintableField field, StringBuilder sbuilder)
        {
            _FOT(element, sbuilder);
            _B3(element, sbuilder);
            _Data(element, field, sbuilder);
            _FS(sbuilder);
        }
        private void Barcode128(LabelElement element, PrintableField field, StringBuilder sbuilder)
        {
            _FOT(element, sbuilder);
            _BC(element, sbuilder);
            _Data(element, field, sbuilder);
            _FS(sbuilder);
        }
        private void Qrcode(LabelElement element, PrintableField field, StringBuilder sbuilder)
        {
            _FOT(element, sbuilder);
            _BQ(element, sbuilder);
            _Data(element, field, sbuilder);
            _FS(sbuilder);
        }
        private void Datamatrix(LabelElement element, PrintableField field, StringBuilder sbuilder)
        {
            _FOT(element, sbuilder);
            _BX(element, sbuilder);
            _Data(element, field, sbuilder);
            _FS(sbuilder);
        }



        private static void _Data(LabelElement element, PrintableField field, StringBuilder sbuild)
        {
            StringBuilder FD = null;
            if (field != null)
            {
                if (!string.IsNullOrEmpty(element.Format))
                {
                    var formatado = field.Formatter.Format(field.Data, element.Format);
                    _FD(formatado, sbuild);
                }
                else
                {
                    string data = field.Data != null ? field.Data.ToString() : "";
                    _FD(data, sbuild);
                }
            }
            else
            {
                _FD(element, sbuild);
            }
        }


        private static void _GB(LabelElement element, StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.GB].Token);
            sbuilder.Append(element.Size.Width.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Size.Height.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Material.Thickness.ToString("0"));
        }

        private static void _A(LabelElement element, StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.A].Token);

            if (!string.IsNullOrEmpty(element.Font.FamilyName))
            {
                sbuilder.Append(element.Font.FamilyName);
            }
            else
            {
                sbuilder.Append("0");
            }

            sbuilder.Append(GetRotation(element.RotateQuadrant));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Size.Height.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Size.Width.ToString("0"));
        }


        private static void _BX(LabelElement element, StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.BX].Token);
            sbuilder.Append(GetRotation(element.RotateQuadrant));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Size.Height.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append("200");
        }


        private static void _BQ(LabelElement element, StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.BQ].Token);
            sbuilder.Append(GetRotation(element.RotateQuadrant));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append("2");
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Size.Height.ToString("0"));
        }


        private static void _BC(LabelElement element, StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.BC].Token);
            sbuilder.Append(GetRotation(element.RotateQuadrant));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Size.Height.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append("N");
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append("N");
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append("N");
        }


        private static void _B3(LabelElement element, StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.B3].Token);
            sbuilder.Append(GetRotation(element.RotateQuadrant));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append("N");
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Size.Height.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append("N");
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append("N");
        }



        private static void _XG(LabelElement element, StringBuilder sbuilder)
        {
            var m = element.Size.Magnification;
            if (m < 1) m = 1;
            if (m > 10) m = 10;

            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.XG].Token);
            sbuilder.Append(element.Name);
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(m.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(m.ToString("0"));
        }
        private static void _FT(LabelElement element, StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.FT].Token);
            sbuilder.Append(element.Position.X.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Position.Y.ToString("0"));
        }
        private static void _FO(LabelElement element, StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.FO].Token);
            sbuilder.Append(element.Position.X.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Position.Y.ToString("0"));
        }

        private static void _FD(string data, StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.FD].Token);
            sbuilder.Append(data);
        }
        private static void _FD(LabelElement element, StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.FD].Token);
            sbuilder.Append(element.Value);
        }


        private static void _FOT(LabelElement element, StringBuilder sbuilder)
        {
            if (element.Position.BottomLeftJustification) _FT(element, sbuilder);
                                                     else _FO(element, sbuilder);
        }
        private static void _FS(StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.FS].Token);
            sbuilder.Append(Environment.NewLine);
        }
        private static void _XA(StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.XA].Token);
            sbuilder.Append(Environment.NewLine);
        }
        private static void _XZ(StringBuilder sbuilder)
        {
            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.XZ].Token);
            sbuilder.Append(Environment.NewLine);
        }





        private static string GetRotation(LabelRotateQuadrant rota)
        {
            string saida = "N";
            switch (rota)
            {
                case LabelRotateQuadrant.RETATE_90: return "R";
                case LabelRotateQuadrant.RETATE_180: return "I";
                case LabelRotateQuadrant.RETATE_270: return "B";
            }
            return saida;
        }

        
    }

 
}
