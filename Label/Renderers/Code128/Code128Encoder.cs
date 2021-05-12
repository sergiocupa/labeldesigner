

using Label.Renderers.Base;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace Label.Renderers.Code128
{
    public class Code128Encoder : IEncoder
    {


       

        public EncoderResult Render(string contents)
        {
            var matrix = Prepare(contents);
            var result = RenderRectangles(matrix);

            EncoderResult r = new EncoderResult();
            r.Rectangles = result.Rectangles;
            r.Width = result.CodeWidth;
            r.Height = result.Height;
            return r;
        }


        private RendeResult RenderRectangles(bool[] bits)
        {
            if (Zoom <= 0) Zoom = 1;

            RendeResult saida = new RendeResult();
            int start = -1;
            int ix = 0;
            while (ix < bits.Length)
            {
                var val = bits[ix];

                if(val)
                {
                    if (start < 0)
                    {
                        start = ix;
                    }
                }
                else
                {
                    if ((start >= 0) && (ix > start))
                    {
                        var w = (ix - start) * (int)Mult;
                        var x = start * (int)Mult;

                        var w_z = w * Zoom;
                        var h_z = Height * Zoom;
                        var x_z = x * Zoom;

                        Rectangle r = new Rectangle((int)x_z, 0, (int)w_z, (int)h_z);
                        saida.Rectangles.Add(r);
                        start = -1;
                        saida.CodeWidth = (int)(x_z + w_z);
                        saida.Height = (int)h_z;
                    }
                }
                ix++;
            }
            return saida;
        }


        private bool[] Prepare(string contents)
        {
            int length = contents.Length;
            for (int i = 0; i < length; i++)
            {
                char c = contents[i];
                switch (c)
                {
                    case ESCAPE_FNC_1:
                    case ESCAPE_FNC_2:
                    case ESCAPE_FNC_3:
                    case ESCAPE_FNC_4:
                        break;
                    default:
                        if (c > 127)
                            // support for FNC4 isn't implemented, no full Latin-1 character set available at the moment
                            throw new ArgumentException("Bad character in input: " + c);
                        break;
                }
            }

            var patterns = new List<int[]>(); // temporary storage for patterns
            int checkSum = 0;
            int checkWeight = 1;
            int codeSet = 0; // selected code (CODE_CODE_B or CODE_CODE_C)
            int position = 0; // position in contents

            while (position < length)
            {
                //Select code to use
                int newCodeSet = ChooseCode(contents, position, codeSet);

                //Get the pattern index
                int patternIndex;
                if (newCodeSet == codeSet)
                {
                    // Encode the current character
                    // First handle escapes
                    switch (contents[position])
                    {
                        case ESCAPE_FNC_1:
                            patternIndex = CODE_FNC_1;
                            break;
                        case ESCAPE_FNC_2:
                            patternIndex = CODE_FNC_2;
                            break;
                        case ESCAPE_FNC_3:
                            patternIndex = CODE_FNC_3;
                            break;
                        case ESCAPE_FNC_4:
                            if (newCodeSet == CODE_CODE_A)
                                patternIndex = CODE_FNC_4_A;
                            else
                                patternIndex = CODE_FNC_4_B;
                            break;
                        default:
                            // Then handle normal characters otherwise
                            switch (codeSet)
                            {
                                case CODE_CODE_A:
                                    patternIndex = contents[position] - ' ';
                                    if (patternIndex < 0)
                                    {
                                        // everything below a space character comes behind the underscore in the code patterns table
                                        patternIndex += '`';
                                    }
                                    break;
                                case CODE_CODE_B:
                                    patternIndex = contents[position] - ' ';
                                    break;
                                default:
                                    // CODE_CODE_C
                                    patternIndex = Int32.Parse(contents.Substring(position, 2));
                                    position++; // Also incremented below
                                    break;
                            }
                            break;
                    }
                    position++;
                }
                else
                {
                    // Should we change the current code?
                    // Do we have a code set?
                    if (codeSet == 0)
                    {
                        // No, we don't have a code set
                        switch (newCodeSet)
                        {
                            case CODE_CODE_A:
                                patternIndex = CODE_START_A;
                                break;
                            case CODE_CODE_B:
                                patternIndex = CODE_START_B;
                                break;
                            default:
                                patternIndex = CODE_START_C;
                                break;
                        }
                    }
                    else
                    {
                        // Yes, we have a code set
                        patternIndex = newCodeSet;
                    }
                    codeSet = newCodeSet;
                }

                // Get the pattern
                patterns.Add(CodeBase.CODE_PATTERNS[patternIndex]);

                // Compute checksum
                checkSum += patternIndex * checkWeight;
                if (position != 0)
                {
                    checkWeight++;
                }
            }

            // Compute and append checksum
            checkSum %= 103;
            patterns.Add(CodeBase.CODE_PATTERNS[checkSum]);

            // Append stop code
            patterns.Add(CodeBase.CODE_PATTERNS[CODE_STOP]);

            // Compute code width
            int codeWidth = 0;
            foreach (int[] pattern in patterns)
            {
                foreach (int _width in pattern)
                {
                    codeWidth += _width;
                }
            }

            // Compute result
            var result = new bool[codeWidth];
            int pos = 0;
            foreach (int[] pattern in patterns)
            {
                pos += CodeBase.AppendPattern(result, pos, pattern, true);
            }

            return result;
        }



        private int ChooseCode(String value, int start, int oldCode)
        {
            CType lookahead = FindCType(value, start);
            if (lookahead == CType.ONE_DIGIT)
            {
                if (oldCode == CODE_CODE_A)
                {
                    return CODE_CODE_A;
                }
                return CODE_CODE_B;
            }
            if (lookahead == CType.UNCODABLE)
            {
                if (start < value.Length)
                {
                    var c = value[start];
                    if (c < ' ' || (oldCode == CODE_CODE_A && (c < '`' || (c >= ESCAPE_FNC_1 && c <= ESCAPE_FNC_4))))
                    {
                        // can continue in code A, encodes ASCII 0 to 95 or FNC1 to FNC4
                        return CODE_CODE_A;
                    }
                }
                return CODE_CODE_B; // no choice
            }
            if (oldCode == CODE_CODE_A && lookahead == CType.FNC_1)
            {
                return CODE_CODE_A;
            }
            if (oldCode == CODE_CODE_C)
            {
                // can continue in code C
                return CODE_CODE_C;
            }
            if (oldCode == CODE_CODE_B)
            {
                if (lookahead == CType.FNC_1)
                {
                    return CODE_CODE_B; // can continue in code B
                }
                // Seen two consecutive digits, see what follows
                lookahead = FindCType(value, start + 2);
                if (lookahead == CType.UNCODABLE || lookahead == CType.ONE_DIGIT)
                {
                    return CODE_CODE_B; // not worth switching now
                }
                if (lookahead == CType.FNC_1)
                {
                    // two digits, then FNC_1...
                    lookahead = FindCType(value, start + 3);
                    if (lookahead == CType.TWO_DIGITS)
                    {
                        // then two more digits, switch
                        return forceCodesetB ? CODE_CODE_B : CODE_CODE_C;
                    }
                    else
                    {
                        return CODE_CODE_B; // otherwise not worth switching
                    }
                }
                // At this point, there are at least 4 consecutive digits.
                // Look ahead to choose whether to switch now or on the next round.
                int index = start + 4;
                while ((lookahead = FindCType(value, index)) == CType.TWO_DIGITS)
                {
                    index += 2;
                }
                if (lookahead == CType.ONE_DIGIT)
                {
                    // odd number of digits, switch later
                    return CODE_CODE_B;
                }
                return forceCodesetB ? CODE_CODE_B : CODE_CODE_C; // even number of digits, switch now
            }
            // Here oldCode == 0, which means we are choosing the initial code
            if (lookahead == CType.FNC_1)
            {
                // ignore FNC_1
                lookahead = FindCType(value, start + 1);
            }
            if (lookahead == CType.TWO_DIGITS)
            {
                // at least two digits, start in code C
                return forceCodesetB ? CODE_CODE_B : CODE_CODE_C;
            }
            return CODE_CODE_B;
        }

        private static CType FindCType(String value, int start)
        {
            int last = value.Length;
            if (start >= last)
            {
                return CType.UNCODABLE;
            }
            char c = value[start];
            if (c == ESCAPE_FNC_1)
            {
                return CType.FNC_1;
            }
            if (c < '0' || c > '9')
            {
                return CType.UNCODABLE;
            }
            if (start + 1 >= last)
            {
                return CType.ONE_DIGIT;
            }
            c = value[start + 1];
            if (c < '0' || c > '9')
            {
                return CType.ONE_DIGIT;
            }
            return CType.TWO_DIGITS;
        }



        internal class RendeResult
        {
            public List<Rectangle> Rectangles { get; set; }
            public int CodeWidth { get; set; }
            public int Height { get; set; }

            internal RendeResult()
            {
                Rectangles = new List<Rectangle>();
            }
        }


        private enum CType
        {
            UNCODABLE,
            ONE_DIGIT,
            TWO_DIGITS,
            FNC_1
        }

        private bool forceCodesetB;


        private const int CODE_START_A = 103;
        private const int CODE_START_B = 104;
        private const int CODE_START_C = 105;
        private const int CODE_CODE_A = 101;
        private const int CODE_CODE_B = 100;
        private const int CODE_CODE_C = 99;
        private const int CODE_STOP = 106;

        private const char ESCAPE_FNC_1 = '\u00f1';
        private const char ESCAPE_FNC_2 = '\u00f2';
        private const char ESCAPE_FNC_3 = '\u00f3';
        private const char ESCAPE_FNC_4 = '\u00f4';

        private const int CODE_FNC_1 = 102; // Code A, Code B, Code C
        private const int CODE_FNC_2 = 97; // Code A, Code B
        private const int CODE_FNC_3 = 96; // Code A, Code B
        private const int CODE_FNC_4_A = 101; // Code A
        private const int CODE_FNC_4_B = 100; // Code B



        public int Height { get; set; }
        public bool RenderContent { get; set; }
        public bool RenderContentAboveBarcode { get; set; }
        public Font RenderContentFont { get; set; }
        public int GapBetweenTextAndCode { get; set; }
        public double Mult { get; set; }
        public double Zoom { get; set; }



        public Code128Encoder
        (
            double mult,
            int height, 
            Font renderContentFont = null, 
            int gapBetweenTextAndCode = 1, 
            bool renderContent = true, 
            bool renderContentAboveBarcode = false,
            double zoom = 1
        )
        {
            Zoom = zoom;
            Mult = mult;
            Height = height;
            RenderContent = renderContent;
            RenderContentAboveBarcode = renderContentAboveBarcode;
            RenderContentFont = renderContentFont;
            GapBetweenTextAndCode = gapBetweenTextAndCode;

            if (Mult < 1) Mult = 1;

            if (renderContent && renderContentFont == null)
            {
                RenderContentFont = new Font(FontFamily.GenericSansSerif, 10);
            }
        }
    }
}
