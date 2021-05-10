

using System;
using System.Text;


namespace Label.Renderers.Print.Zpl
{
    public class ZplLabelAssembler : ILabelAssembler
    {


        public StringBuilder AssembleElement(LabelElement element, PrintableField field)
        {
            StringBuilder sb = new StringBuilder();

            switch (element.ElementType)
            {
                case LabelElementType.PRINTER_FONT:
                    {
                        AssembleText(element, field, sb);
                    }
                    break;
                case LabelElementType.RENDERED_TEXT:
                case LabelElementType.PRELOADED_IMAGE:
                    {
                        AssembleRendered(element, sb);
                    }
                    break;
                case LabelElementType.CODIGO_BARRA_39:
                    {
                        AssembleBarcode39(element, field, sb);
                    }
                    break;
                case LabelElementType.CODIGO_BARRA_128:
                    {
                        AssembleBarcode128(element, field, sb);
                    }
                    break;
                case LabelElementType.QR_CODE:
                    {
                        AssembleQrcode(element, field, sb);
                    }
                    break;
                case LabelElementType.DATAMATRIX:
                    {
                        AssembleDatamatrix(element, field, sb);
                    }
                    break;
                case LabelElementType.GRAPHIC_BOX:
                    {
                        AssembleBox(element, sb);
                    }
                    break;
            }
            return sb;
        }



        private static void AssembleBox(LabelElement element, StringBuilder sbuilder)
        {
            var FOT = element.Position.BottomLeftJustification ? Assemble_FT(element) : Assemble_FO(element);
            var GB = Assemble_GB(element);
            var FS = Assemble_FS();

            sbuilder.Append(FOT);
            sbuilder.Append(GB);
            sbuilder.Append(FS);
            sbuilder.Append(Environment.NewLine);
        }
        private static void AssembleText(LabelElement element, PrintableField field, StringBuilder sbuilder)
        {
            var FOT = element.Position.BottomLeftJustification ? Assemble_FT(element) : Assemble_FO(element);
            var A   = Assemble_A(element);
            var FD  = PrepareData(element, field);
            var FS  = Assemble_FS();

            sbuilder.Append(FOT);
            sbuilder.Append(A);
            sbuilder.Append(FD);
            sbuilder.Append(FS);
            sbuilder.Append(Environment.NewLine);
        }
        private void AssembleRendered(LabelElement element, StringBuilder sbuilder)
        {
            var FOT = element.Position.BottomLeftJustification ? Assemble_FT(element) : Assemble_FO(element);
            var XG  = Assemble_XG(element);
            var FS  = Assemble_FS();

            sbuilder.Append(FOT);
            sbuilder.Append(XG);
            sbuilder.Append(FS);
            sbuilder.Append(Environment.NewLine);
        }
        private void AssembleBarcode39(LabelElement element, PrintableField field, StringBuilder sbuilder)
        {
            var FOT = element.Position.BottomLeftJustification ? Assemble_FT(element) : Assemble_FO(element);
            var B3  = Assemble_B3(element);
            var FD  = PrepareData(element, field);
            var FS  = Assemble_FS();

            sbuilder.Append(FOT);
            sbuilder.Append(B3);
            sbuilder.Append(FD);
            sbuilder.Append(FS);
            sbuilder.Append(Environment.NewLine);
        }
        private void AssembleBarcode128(LabelElement element, PrintableField field, StringBuilder sbuilder)
        {
            var FOT = element.Position.BottomLeftJustification ? Assemble_FT(element) : Assemble_FO(element);
            var BC  = Assemble_BC(element);
            var FD  = PrepareData(element, field);
            var FS  = Assemble_FS();

            sbuilder.Append(FOT);
            sbuilder.Append(BC);
            sbuilder.Append(FD);
            sbuilder.Append(FS);
            sbuilder.Append(Environment.NewLine);
        }
        private void AssembleQrcode(LabelElement element, PrintableField field, StringBuilder sbuilder)
        {
            var FOT = element.Position.BottomLeftJustification ? Assemble_FT(element) : Assemble_FO(element);
            var BQ  = Assemble_BQ(element);
            var FD  = PrepareData(element, field);
            var FS  = Assemble_FS();

            sbuilder.Append(FOT);
            sbuilder.Append(BQ);
            sbuilder.Append(FD);
            sbuilder.Append(FS);
            sbuilder.Append(Environment.NewLine);
        }
        private void AssembleDatamatrix(LabelElement element, PrintableField field, StringBuilder sbuilder)
        {
            var FOT = element.Position.BottomLeftJustification ? Assemble_FT(element) : Assemble_FO(element);
            var BX  = Assemble_BX(element);
            var FD  = PrepareData(element, field);
            var FS  = Assemble_FS();

            sbuilder.Append(FOT);
            sbuilder.Append(BX);
            sbuilder.Append(FD);
            sbuilder.Append(FS);
            sbuilder.Append(Environment.NewLine);
        }



        private static StringBuilder PrepareData(LabelElement element, PrintableField field)
        {
            StringBuilder FD = null;
            if (field != null)
            {
                if (!string.IsNullOrEmpty(element.Format))
                {
                    var formatado = field.Formatter.Format(field.Data, element.Format);
                    FD = Assemble_FD(formatado);
                }
                else
                {
                    string data = field.Data != null ? field.Data.ToString() : "";
                    FD = Assemble_FD(data);
                }
            }
            else
            {
                FD = Assemble_FD(element);
            }
            return FD;
        }


        private static StringBuilder Assemble_GB(LabelElement element)
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.GB].Token);
            sbuilder.Append(element.Size.Width.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Size.Height.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Material.Thickness.ToString("0"));
            return sbuilder;
        }

        private static StringBuilder Assemble_A(LabelElement element)
        {
            StringBuilder sbuilder = new StringBuilder();

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
            return sbuilder;
        }


        private static StringBuilder Assemble_BX(LabelElement element)
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.BX].Token);
            sbuilder.Append(GetRotation(element.RotateQuadrant));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Size.Height.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append("200");

            return sbuilder;
        }


        private static StringBuilder Assemble_BQ(LabelElement element)
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.BQ].Token);
            sbuilder.Append(GetRotation(element.RotateQuadrant));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append("2");
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Size.Height.ToString("0"));
            return sbuilder;
        }


        private static StringBuilder Assemble_BC(LabelElement element)
        {
            StringBuilder sbuilder = new StringBuilder();

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
            return sbuilder;
        }


        private static StringBuilder Assemble_B3(LabelElement element)
        {
            StringBuilder sbuilder = new StringBuilder();

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
            return sbuilder;
        }



        private static StringBuilder Assemble_XG(LabelElement element)
        {
            StringBuilder sbuilder = new StringBuilder();

            var m = element.Size.Magnification;
            if (m < 1) m = 1;
            if (m > 10) m = 10;

            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.XG].Token);
            sbuilder.Append(element.Name);
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(m.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(m.ToString("0"));
            return sbuilder;
        }
        private static StringBuilder Assemble_FT(LabelElement element)
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.FT].Token);
            sbuilder.Append(element.Position.X.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Position.Y.ToString("0"));
            return sbuilder;
        }
        private static StringBuilder Assemble_FO(LabelElement element)
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.FO].Token);
            sbuilder.Append(element.Position.X.ToString("0"));
            sbuilder.Append(Zpl2Command.RELIMITER_VALUE);
            sbuilder.Append(element.Position.Y.ToString("0"));
            return sbuilder;
        }

        private static StringBuilder Assemble_FD(string data)
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.FD].Token);
            sbuilder.Append(data);
            return sbuilder;
        }
        private static StringBuilder Assemble_FD(LabelElement element)
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append(Zpl2Command.ECommands[eZplCommand.FD].Token);
            sbuilder.Append(element.Value);
            return sbuilder;
        }


        private static string Assemble_FS()
        {
            return Zpl2Command.ECommands[eZplCommand.FS].Token;
        }
        private static string Assemble_XA()
        {
            return Zpl2Command.ECommands[eZplCommand.XA].Token;
        }
        private static string Assemble_XZ()
        {
            return Zpl2Command.ECommands[eZplCommand.XZ].Token;
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
