

using System;


namespace Label.Renderers.Qrcode
{
    internal class Mode
    {

        public Names Name { get; private set; }


        public enum Names
        {
            TERMINATOR,
            NUMERIC,
            ALPHANUMERIC,
            STRUCTURED_APPEND,
            BYTE,
            ECI,
            KANJI,
            FNC1_FIRST_POSITION,
            FNC1_SECOND_POSITION,
            HANZI
        }


        public static readonly Mode TERMINATOR = new Mode(new int[] { 0, 0, 0 }, 0x00, Names.TERMINATOR); // Not really a mode...
        public static readonly Mode NUMERIC = new Mode(new int[] { 10, 12, 14 }, 0x01, Names.NUMERIC);
        public static readonly Mode ALPHANUMERIC = new Mode(new int[] { 9, 11, 13 }, 0x02, Names.ALPHANUMERIC);
        public static readonly Mode STRUCTURED_APPEND = new Mode(new int[] { 0, 0, 0 }, 0x03, Names.STRUCTURED_APPEND); // Not supported
        public static readonly Mode BYTE = new Mode(new int[] { 8, 16, 16 }, 0x04, Names.BYTE);
        public static readonly Mode ECI = new Mode(null, 0x07, Names.ECI); // character counts don't apply
        public static readonly Mode KANJI = new Mode(new int[] { 8, 10, 12 }, 0x08, Names.KANJI);
        public static readonly Mode FNC1_FIRST_POSITION = new Mode(null, 0x05, Names.FNC1_FIRST_POSITION);
        public static readonly Mode FNC1_SECOND_POSITION = new Mode(null, 0x09, Names.FNC1_SECOND_POSITION);
        public static readonly Mode HANZI = new Mode(new int[] { 8, 10, 12 }, 0x0D, Names.HANZI);

        private readonly int[] characterCountBitsForVersions;

        private Mode(int[] characterCountBitsForVersions, int bits, Names name)
        {
            this.characterCountBitsForVersions = characterCountBitsForVersions;
            Bits = bits;
            Name = name;
        }


        public static Mode forBits(int bits)
        {
            switch (bits)
            {
                case 0x0:
                    return TERMINATOR;
                case 0x1:
                    return NUMERIC;
                case 0x2:
                    return ALPHANUMERIC;
                case 0x3:
                    return STRUCTURED_APPEND;
                case 0x4:
                    return BYTE;
                case 0x5:
                    return FNC1_FIRST_POSITION;
                case 0x7:
                    return ECI;
                case 0x8:
                    return KANJI;
                case 0x9:
                    return FNC1_SECOND_POSITION;
                case 0xD:
                    // 0xD is defined in GBT 18284-2000, may not be supported in foreign country
                    return HANZI;
                default:
                    throw new ArgumentException();
            }
        }


        public int getCharacterCountBits(Version version)
        {
            if (characterCountBitsForVersions == null)
            {
                throw new ArgumentException("Character count doesn't apply to this mode");
            }
            int number = version.VersionNumber;
            int offset;
            if (number <= 9)
            {
                offset = 0;
            }
            else if (number <= 26)
            {
                offset = 1;
            }
            else
            {
                offset = 2;
            }
            return characterCountBitsForVersions[offset];
        }


        public int Bits { get; private set; }


        public override String ToString()
        {
            return Name.ToString();
        }
    }
}
