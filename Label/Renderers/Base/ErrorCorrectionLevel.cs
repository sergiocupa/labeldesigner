

using System;


namespace Label.Renderers.Base
{
    public class ErrorCorrectionLevel
    {
        public static readonly ErrorCorrectionLevel L = new ErrorCorrectionLevel(0, 0x01, "L");
        /// <summary> M = ~15% correction</summary>
        public static readonly ErrorCorrectionLevel M = new ErrorCorrectionLevel(1, 0x00, "M");
        /// <summary> Q = ~25% correction</summary>
        public static readonly ErrorCorrectionLevel Q = new ErrorCorrectionLevel(2, 0x03, "Q");
        /// <summary> H = ~30% correction</summary>
        public static readonly ErrorCorrectionLevel H = new ErrorCorrectionLevel(3, 0x02, "H");

        private static readonly ErrorCorrectionLevel[] FOR_BITS = new[] { M, L, H, Q };

        private readonly int bits;

        private ErrorCorrectionLevel(int ordinal, int bits, String name)
        {
            this.ordinal_Renamed_Field = ordinal;
            this.bits = bits;
            this.name = name;
        }


        public int Bits
        {
            get
            {
                return bits;
            }
        }


        public String Name
        {
            get
            {
                return name;
            }
        }

        private readonly int ordinal_Renamed_Field;
        private readonly String name;


        public int ordinal()
        {
            return ordinal_Renamed_Field;
        }


        public override String ToString()
        {
            return name;
        }


        public static ErrorCorrectionLevel forBits(int bits)
        {
            if (bits < 0 || bits >= FOR_BITS.Length)
            {
                throw new ArgumentException();
            }
            return FOR_BITS[bits];
        }
    }
}
