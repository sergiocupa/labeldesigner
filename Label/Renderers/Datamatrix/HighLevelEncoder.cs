

using Label.Renderers.Base;
using System;
using System.Text;


namespace Label.Renderers.Datamatrix
{
    internal class HighLevelEncoder
    {
        public const char PAD = (char)129;
        public const char LATCH_TO_C40 = (char)230;
        public const char LATCH_TO_BASE256 = (char)231;
        public const char FNC1 = (char)232;
        public const char STRUCTURED_APPEND = (char)233;
        public const char READER_PROGRAMMING = (char)234;
        public const char UPPER_SHIFT = (char)235;
        public const char MACRO_05 = (char)236;
        public const char MACRO_06 = (char)237;
        public const char LATCH_TO_ANSIX12 = (char)238;
        public const char LATCH_TO_TEXT = (char)239;
        public const char LATCH_TO_EDIFACT = (char)240;
        public const char ECI = (char)241;
        public const char C40_UNLATCH = (char)254;
        public const char X12_UNLATCH = (char)254;
        public const String MACRO_05_HEADER = "[)>\u001E05\u001D";
        public const String MACRO_06_HEADER = "[)>\u001E06\u001D";
        public const String MACRO_TRAILER = "\u001E\u0004";


        private static char randomize253State(char ch, int codewordPosition)
        {
            int pseudoRandom = ((149 * codewordPosition) % 253) + 1;
            int tempVariable = ch + pseudoRandom;
            return (char)(tempVariable <= 254 ? tempVariable : tempVariable - 254);
        }

        public static String encodeHighLevel(String msg)
        {
            return encodeHighLevel(msg, ESymbolShapeHint.FORCE_NONE, null, null, Encodation.ASCII);
        }


        public static String encodeHighLevel(String msg,
                                             ESymbolShapeHint shape,
                                             Dimension minSize,
                                             Dimension maxSize,
                                             int defaultEncodation)
        {
            //the codewords 0..255 are encoded as Unicode characters
            Encoder[] encoders =
               {
               new ASCIIEncoder(), new C40Encoder(), new TextEncoder(),
               new X12Encoder(), new EdifactEncoder(), new Base256Encoder()
            };

            var context = new EncoderContext(msg);
            context.setSymbolShape(shape);
            context.setSizeConstraints(minSize, maxSize);

            if (msg.StartsWith(MACRO_05_HEADER) && msg.EndsWith(MACRO_TRAILER))
            {
                context.writeCodeword(MACRO_05);
                context.setSkipAtEnd(2);
                context.Pos += MACRO_05_HEADER.Length;
            }
            else if (msg.StartsWith(MACRO_06_HEADER) && msg.EndsWith(MACRO_TRAILER))
            {
                context.writeCodeword(MACRO_06);
                context.setSkipAtEnd(2);
                context.Pos += MACRO_06_HEADER.Length;
            }

            int encodingMode = defaultEncodation; //Default mode
            switch (encodingMode)
            {
                case Encodation.BASE256:
                    context.writeCodeword(HighLevelEncoder.LATCH_TO_BASE256);
                    break;
                case Encodation.C40:
                    context.writeCodeword(HighLevelEncoder.LATCH_TO_C40);
                    break;
                case Encodation.X12:
                    context.writeCodeword(HighLevelEncoder.LATCH_TO_ANSIX12);
                    break;
                case Encodation.TEXT:
                    context.writeCodeword(HighLevelEncoder.LATCH_TO_TEXT);
                    break;
                case Encodation.EDIFACT:
                    context.writeCodeword(HighLevelEncoder.LATCH_TO_EDIFACT);
                    break;
                case Encodation.ASCII:
                    break;
                default:
                    throw new InvalidOperationException("Illegal mode: " + encodingMode);
            }
            while (context.HasMoreCharacters)
            {
                encoders[encodingMode].encode(context);
                if (context.NewEncoding >= 0)
                {
                    encodingMode = context.NewEncoding;
                    context.resetEncoderSignal();
                }
            }
            int len = context.Codewords.Length;
            context.updateSymbolInfo();
            int capacity = context.SymbolInfo.dataCapacity;
            if (len < capacity &&
                encodingMode != Encodation.ASCII &&
                encodingMode != Encodation.BASE256 &&
                encodingMode != Encodation.EDIFACT)
            {
                context.writeCodeword('\u00fe'); //Unlatch (254)
            }
            //Padding
            StringBuilder codewords = context.Codewords;
            if (codewords.Length < capacity)
            {
                codewords.Append(PAD);
            }
            while (codewords.Length < capacity)
            {
                codewords.Append(randomize253State(PAD, codewords.Length + 1));
            }

            return context.Codewords.ToString();
        }

        internal static int lookAheadTest(String msg, int startpos, int currentMode)
        {
            if (startpos >= msg.Length)
            {
                return currentMode;
            }
            float[] charCounts;
            //step J
            if (currentMode == Encodation.ASCII)
            {
                charCounts = new[] { 0, 1, 1, 1, 1, 1.25f };
            }
            else
            {
                charCounts = new[] { 1, 2, 2, 2, 2, 2.25f };
                charCounts[currentMode] = 0;
            }

            int charsProcessed = 0;
            while (true)
            {
                //step K
                if ((startpos + charsProcessed) == msg.Length)
                {
                    var min = Int32.MaxValue;
                    var mins = new byte[6];
                    var intCharCounts = new int[6];
                    min = findMinimums(charCounts, intCharCounts, min, mins);
                    var minCount = getMinimumCount(mins);

                    if (intCharCounts[Encodation.ASCII] == min)
                    {
                        return Encodation.ASCII;
                    }
                    if (minCount == 1 && mins[Encodation.BASE256] > 0)
                    {
                        return Encodation.BASE256;
                    }
                    if (minCount == 1 && mins[Encodation.EDIFACT] > 0)
                    {
                        return Encodation.EDIFACT;
                    }
                    if (minCount == 1 && mins[Encodation.TEXT] > 0)
                    {
                        return Encodation.TEXT;
                    }
                    if (minCount == 1 && mins[Encodation.X12] > 0)
                    {
                        return Encodation.X12;
                    }
                    return Encodation.C40;
                }

                char c = msg[startpos + charsProcessed];
                charsProcessed++;

                //step L
                if (isDigit(c))
                {
                    charCounts[Encodation.ASCII] += 0.5f;
                }
                else if (isExtendedASCII(c))
                {
                    charCounts[Encodation.ASCII] = (float)Math.Ceiling(charCounts[Encodation.ASCII]);
                    charCounts[Encodation.ASCII] += 2.0f;
                }
                else
                {
                    charCounts[Encodation.ASCII] = (float)Math.Ceiling(charCounts[Encodation.ASCII]);
                    charCounts[Encodation.ASCII]++;
                }

                //step M
                if (isNativeC40(c))
                {
                    charCounts[Encodation.C40] += 2.0f / 3.0f;
                }
                else if (isExtendedASCII(c))
                {
                    charCounts[Encodation.C40] += 8.0f / 3.0f;
                }
                else
                {
                    charCounts[Encodation.C40] += 4.0f / 3.0f;
                }

                //step N
                if (isNativeText(c))
                {
                    charCounts[Encodation.TEXT] += 2.0f / 3.0f;
                }
                else if (isExtendedASCII(c))
                {
                    charCounts[Encodation.TEXT] += 8.0f / 3.0f;
                }
                else
                {
                    charCounts[Encodation.TEXT] += 4.0f / 3.0f;
                }

                //step O
                if (isNativeX12(c))
                {
                    charCounts[Encodation.X12] += 2.0f / 3.0f;
                }
                else if (isExtendedASCII(c))
                {
                    charCounts[Encodation.X12] += 13.0f / 3.0f;
                }
                else
                {
                    charCounts[Encodation.X12] += 10.0f / 3.0f;
                }

                //step P
                if (isNativeEDIFACT(c))
                {
                    charCounts[Encodation.EDIFACT] += 3.0f / 4.0f;
                }
                else if (isExtendedASCII(c))
                {
                    charCounts[Encodation.EDIFACT] += 17.0f / 4.0f;
                }
                else
                {
                    charCounts[Encodation.EDIFACT] += 13.0f / 4.0f;
                }

                // step Q
                if (isSpecialB256(c))
                {
                    charCounts[Encodation.BASE256] += 4.0f;
                }
                else
                {
                    charCounts[Encodation.BASE256]++;
                }

                //step R
                if (charsProcessed >= 4)
                {
                    var intCharCounts = new int[6];
                    var mins = new byte[6];
                    findMinimums(charCounts, intCharCounts, Int32.MaxValue, mins);
                    int minCount = getMinimumCount(mins);

                    if (intCharCounts[Encodation.ASCII] < intCharCounts[Encodation.BASE256]
                        && intCharCounts[Encodation.ASCII] < intCharCounts[Encodation.C40]
                        && intCharCounts[Encodation.ASCII] < intCharCounts[Encodation.TEXT]
                        && intCharCounts[Encodation.ASCII] < intCharCounts[Encodation.X12]
                        && intCharCounts[Encodation.ASCII] < intCharCounts[Encodation.EDIFACT])
                    {
                        return Encodation.ASCII;
                    }
                    if (intCharCounts[Encodation.BASE256] < intCharCounts[Encodation.ASCII]
                        || (mins[Encodation.C40] + mins[Encodation.TEXT] + mins[Encodation.X12] + mins[Encodation.EDIFACT]) == 0)
                    {
                        return Encodation.BASE256;
                    }
                    if (minCount == 1 && mins[Encodation.EDIFACT] > 0)
                    {
                        return Encodation.EDIFACT;
                    }
                    if (minCount == 1 && mins[Encodation.TEXT] > 0)
                    {
                        return Encodation.TEXT;
                    }
                    if (minCount == 1 && mins[Encodation.X12] > 0)
                    {
                        return Encodation.X12;
                    }
                    if (intCharCounts[Encodation.C40] + 1 < intCharCounts[Encodation.ASCII]
                        && intCharCounts[Encodation.C40] + 1 < intCharCounts[Encodation.BASE256]
                        && intCharCounts[Encodation.C40] + 1 < intCharCounts[Encodation.EDIFACT]
                        && intCharCounts[Encodation.C40] + 1 < intCharCounts[Encodation.TEXT])
                    {
                        if (intCharCounts[Encodation.C40] < intCharCounts[Encodation.X12])
                        {
                            return Encodation.C40;
                        }
                        if (intCharCounts[Encodation.C40] == intCharCounts[Encodation.X12])
                        {
                            int p = startpos + charsProcessed + 1;
                            while (p < msg.Length)
                            {
                                char tc = msg[p];
                                if (isX12TermSep(tc))
                                {
                                    return Encodation.X12;
                                }
                                if (!isNativeX12(tc))
                                {
                                    break;
                                }
                                p++;
                            }
                            return Encodation.C40;
                        }
                    }
                }
            }
        }

        private static int findMinimums(float[] charCounts, int[] intCharCounts, int min, byte[] mins)
        {
            SupportClass.Fill(mins, (byte)0);
            for (int i = 0; i < 6; i++)
            {
                intCharCounts[i] = (int)Math.Ceiling(charCounts[i]);
                int current = intCharCounts[i];
                if (min > current)
                {
                    min = current;
                    SupportClass.Fill(mins, (byte)0);
                }
                if (min == current)
                {
                    mins[i]++;

                }
            }
            return min;
        }

        private static int getMinimumCount(byte[] mins)
        {
            int minCount = 0;
            for (int i = 0; i < 6; i++)
            {
                minCount += mins[i];
            }
            return minCount;
        }

        internal static bool isDigit(char ch)
        {
            return ch >= '0' && ch <= '9';
        }

        internal static bool isExtendedASCII(char ch)
        {
            return ch >= 128 && ch <= 255;
        }

        internal static bool isNativeC40(char ch)
        {
            return (ch == ' ') || (ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z');
        }

        internal static bool isNativeText(char ch)
        {
            return (ch == ' ') || (ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'z') || ch == 0x001d;
        }

        internal static bool isNativeX12(char ch)
        {
            return isX12TermSep(ch) || (ch == ' ') || (ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z');
        }

        internal static bool isX12TermSep(char ch)
        {
            return (ch == '\r') //CR
                || (ch == '*')
                || (ch == '>');
        }

        internal static bool isNativeEDIFACT(char ch)
        {
            return ch >= ' ' && ch <= '^';
        }

        internal static bool isSpecialB256(char ch)
        {
            return false; //TODO NOT IMPLEMENTED YET!!!
        }

        /// <summary>
        /// Determines the number of consecutive characters that are encodable using numeric compaction.
        /// </summary>
        /// <param name="msg">the message</param>
        /// <param name="startpos">the start position within the message</param>
        /// <returns>the requested character count</returns>
        public static int determineConsecutiveDigitCount(String msg, int startpos)
        {
            int count = 0;
            int len = msg.Length;
            int idx = startpos;
            if (idx < len)
            {
                char ch = msg[idx];
                while (isDigit(ch) && idx < len)
                {
                    count++;
                    idx++;
                    if (idx < len)
                    {
                        ch = msg[idx];
                    }
                }
            }
            return count;
        }

        internal static void illegalCharacter(char c)
        {
            throw new ArgumentException(String.Format("Illegal character: {0} (0x{1:X})", c, (int)c));
        }
    }
}
