

using System;
using System.Collections.Generic;


namespace Label.Renderers.Print.Zpl
{
    public class Zpl2Command
    {
        public eZplCommand Code { get; private set; }
        public string Name { get; private set; }
        public string Token { get; private set; }

        public static Dictionary<string, Zpl2Command> Commands { get; private set; }
        internal static Dictionary<eZplCommand, Zpl2Command> ECommands { get; private set; }

        public const string PREFIX1 = "^";
        public const string PREFIX2 = "~";
        public static readonly char[] PREFIX_CH = new char[] { '^' };
        public const string RELIMITER_VALUE = ",";
        public static readonly char[] RELIMITER_VALUE_CH = new char[] { ',' };


        static Zpl2Command()
        {
            var tamanho = Enum.GetNames(typeof(eZplCommand)).Length;
            var itens = Enum.GetValues(typeof(eZplCommand));

            Commands = new Dictionary<string, Zpl2Command>();
            ECommands = new Dictionary<eZplCommand, Zpl2Command>();

            int i = 0;
            while (i < tamanho)
            {
                var item = (eZplCommand)itens.GetValue(i);

                var ids = item.ToString();
                var token = PREFIX1 + item.ToString();
                if (ids.StartsWith("_"))
                {
                    token = PREFIX2 + item.ToString();
                }
                var nome = ids.Replace("_", "");

                var cmd = new Zpl2Command() { Code = item, Name = nome, Token = token };

                Commands.Add(nome, cmd);
                ECommands.Add(cmd.Code, cmd);
                i++;
            }

            //Hexs = Zpl2.InicializarHex();
            //Acentuados = Zpl2.InicializarAcentuados();
        }
    }

    public enum eZplCommand
    {
        XA = 5,
        XZ = 6,
        MC = 7,
        LH = 8,
        FO = 9,
        FT = 10,
        FS = 11,
        LR = 12,
        FW = 13,
        CF = 14,
        CI = 15,
        PR = 16,
        PO = 17,
        PM = 18,
        GB = 19,
        PQ = 20,
        BC = 21,
        BY = 22,
        B3 = 23,
        LL = 24,
        FR = 25,
        SN = 26,
        XG = 27,
        GS = 28,
        MT = 29,
        MM = 30,
        MN = 31,
        MD = 32,
        A = 33,
        FD = 35,
        ID = 36,
        IL = 37,
        FX = 38,
        _DG = 39,

        CO = 40,
        CT = 41,
        FB = 42,
        FH = 43,

        CW = 44,
        IS = 45,
        PF = 46,
        GF = 47,

        JS = 48,
        BQ = 49,
        BX = 50
    }
}
