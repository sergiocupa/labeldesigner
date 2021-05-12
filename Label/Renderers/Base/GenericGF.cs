

using System;


namespace Label.Renderers.Base
{
    public sealed class GenericGF
    {

        public static GenericGF AZTEC_DATA_12 = new GenericGF(0x1069, 4096, 1); // x^12 + x^6 + x^5 + x^3 + 1
        public static GenericGF AZTEC_DATA_10 = new GenericGF(0x409, 1024, 1); // x^10 + x^3 + 1
        public static GenericGF AZTEC_DATA_6 = new GenericGF(0x43, 64, 1); // x^6 + x + 1
        public static GenericGF AZTEC_PARAM = new GenericGF(0x13, 16, 1); // x^4 + x + 1
        public static GenericGF QR_CODE_FIELD_256 = new GenericGF(0x011D, 256, 0); // x^8 + x^4 + x^3 + x^2 + 1
        public static GenericGF DATA_MATRIX_FIELD_256 = new GenericGF(0x012D, 256, 1); // x^8 + x^5 + x^3 + x^2 + 1
        public static GenericGF AZTEC_DATA_8 = DATA_MATRIX_FIELD_256;
        public static GenericGF MAXICODE_FIELD_64 = AZTEC_DATA_6;

        private readonly int[] expTable;
        private readonly int[] logTable;
        private readonly GenericGFPoly zero;
        private readonly GenericGFPoly one;
        private readonly int size;
        private readonly int primitive;
        private readonly int generatorBase;


        public GenericGF(int primitive, int size, int genBase)
        {
            this.primitive = primitive;
            this.size = size;
            this.generatorBase = genBase;

            expTable = new int[size];
            logTable = new int[size];
            int x = 1;
            for (int i = 0; i < size; i++)
            {
                expTable[i] = x;
                x <<= 1; // x = x * 2; we're assuming the generator alpha is 2
                if (x >= size)
                {
                    x ^= primitive;
                    x &= size - 1;
                }
            }
            for (int i = 0; i < size - 1; i++)
            {
                logTable[expTable[i]] = i;
            }
            // logTable[0] == 0 but this should never be used
            zero = new GenericGFPoly(this, new int[] { 0 });
            one = new GenericGFPoly(this, new int[] { 1 });
        }

        internal GenericGFPoly Zero
        {
            get
            {
                return zero;
            }
        }

        internal GenericGFPoly One
        {
            get
            {
                return one;
            }
        }


        internal GenericGFPoly buildMonomial(int degree, int coefficient)
        {
            if (degree < 0)
            {
                throw new ArgumentException();
            }
            if (coefficient == 0)
            {
                return zero;
            }
            int[] coefficients = new int[degree + 1];
            coefficients[0] = coefficient;
            return new GenericGFPoly(this, coefficients);
        }


        internal static int addOrSubtract(int a, int b)
        {
            return a ^ b;
        }


        internal int exp(int a)
        {
            return expTable[a];
        }


        internal int log(int a)
        {
            if (a == 0)
            {
                throw new ArgumentException();
            }
            return logTable[a];
        }

        internal int inverse(int a)
        {
            if (a == 0)
            {
                throw new ArithmeticException();
            }
            return expTable[size - logTable[a] - 1];
        }


        internal int multiply(int a, int b)
        {
            if (a == 0 || b == 0)
            {
                return 0;
            }
            return expTable[(logTable[a] + logTable[b]) % (size - 1)];
        }


        public int Size
        {
            get { return size; }
        }


        public int GeneratorBase
        {
            get { return generatorBase; }
        }


        public override String ToString()
        {
            return "GF(0x" + primitive.ToString("X") + ',' + size + ')';
        }
    }
}
