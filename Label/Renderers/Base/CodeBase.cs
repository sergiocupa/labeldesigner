

using System;
using System.Runtime.CompilerServices;


namespace Label.Renderers.Base
{
    internal class CodeBase
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BitMatrix RenderResult(bool[] code, int width, int height, int sidesMargin)
        {
            int inputWidth   = code.Length;
            int fullWidth    = inputWidth + sidesMargin;
            int outputWidth  = (int)((double)Math.Max(width, fullWidth));
            int outputHeight = Math.Max(1, height);
            int multiple     = outputWidth / fullWidth;
            int leftPadding  = (outputWidth - (inputWidth * multiple)) / 2;

            BitMatrix output = new BitMatrix(outputWidth, outputHeight);
            for (int inputX = 0, outputX = leftPadding; inputX < inputWidth; inputX++, outputX += multiple)
            {
                if (code[inputX])
                {
                    output.setRegion(outputX, 0, multiple, outputHeight);
                }
            }
            return output;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int AppendPattern(bool[] target, int pos, int[] pattern, bool startColor)
        {
            bool color = startColor;
            int numAdded = 0;
            foreach (int len in pattern)
            {
                for (int j = 0; j < len; j++)
                {
                    target[pos++] = color;
                }
                numAdded += len;
                color = !color; // flip color after each segment
            }
            return numAdded;
        }



        internal static int DEFAULT_MARGIN = 10;

        internal static String ALPHABET_STRING = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";
        internal static readonly int ASTERISK_ENCODING = 0x094;

        internal static int[] CHARACTER_ENCODINGS = 
        {
            0x034, 0x121, 0x061, 0x160, 0x031, 0x130, 0x070, 0x025, 0x124, 0x064, // 0-9
            0x109, 0x049, 0x148, 0x019, 0x118, 0x058, 0x00D, 0x10C, 0x04C, 0x01C, // A-J
            0x103, 0x043, 0x142, 0x013, 0x112, 0x052, 0x007, 0x106, 0x046, 0x016, // K-T
            0x181, 0x0C1, 0x1C0, 0x091, 0x190, 0x0D0, 0x085, 0x184, 0x0C4, 0x0A8, // U-$
            0x0A2, 0x08A, 0x02A // /-%
        };

        internal static int[][] CODE_PATTERNS = {
                                                new[] {2, 1, 2, 2, 2, 2}, // 0
                                                new[] {2, 2, 2, 1, 2, 2},
                                                new[] {2, 2, 2, 2, 2, 1},
                                                new[] {1, 2, 1, 2, 2, 3},
                                                new[] {1, 2, 1, 3, 2, 2},
                                                new[] {1, 3, 1, 2, 2, 2}, // 5
                                                new[] {1, 2, 2, 2, 1, 3},
                                                new[] {1, 2, 2, 3, 1, 2},
                                                new[] {1, 3, 2, 2, 1, 2},
                                                new[] {2, 2, 1, 2, 1, 3},
                                                new[] {2, 2, 1, 3, 1, 2}, // 10
                                                new[] {2, 3, 1, 2, 1, 2},
                                                new[] {1, 1, 2, 2, 3, 2},
                                                new[] {1, 2, 2, 1, 3, 2},
                                                new[] {1, 2, 2, 2, 3, 1},
                                                new[] {1, 1, 3, 2, 2, 2}, // 15
                                                new[] {1, 2, 3, 1, 2, 2},
                                                new[] {1, 2, 3, 2, 2, 1},
                                                new[] {2, 2, 3, 2, 1, 1},
                                                new[] {2, 2, 1, 1, 3, 2},
                                                new[] {2, 2, 1, 2, 3, 1}, // 20
                                                new[] {2, 1, 3, 2, 1, 2},
                                                new[] {2, 2, 3, 1, 1, 2},
                                                new[] {3, 1, 2, 1, 3, 1},
                                                new[] {3, 1, 1, 2, 2, 2},
                                                new[] {3, 2, 1, 1, 2, 2}, // 25
                                                new[] {3, 2, 1, 2, 2, 1},
                                                new[] {3, 1, 2, 2, 1, 2},
                                                new[] {3, 2, 2, 1, 1, 2},
                                                new[] {3, 2, 2, 2, 1, 1},
                                                new[] {2, 1, 2, 1, 2, 3}, // 30
                                                new[] {2, 1, 2, 3, 2, 1},
                                                new[] {2, 3, 2, 1, 2, 1},
                                                new[] {1, 1, 1, 3, 2, 3},
                                                new[] {1, 3, 1, 1, 2, 3},
                                                new[] {1, 3, 1, 3, 2, 1}, // 35
                                                new[] {1, 1, 2, 3, 1, 3},
                                                new[] {1, 3, 2, 1, 1, 3},
                                                new[] {1, 3, 2, 3, 1, 1},
                                                new[] {2, 1, 1, 3, 1, 3},
                                                new[] {2, 3, 1, 1, 1, 3}, // 40
                                                new[] {2, 3, 1, 3, 1, 1},
                                                new[] {1, 1, 2, 1, 3, 3},
                                                new[] {1, 1, 2, 3, 3, 1},
                                                new[] {1, 3, 2, 1, 3, 1},
                                                new[] {1, 1, 3, 1, 2, 3}, // 45
                                                new[] {1, 1, 3, 3, 2, 1},
                                                new[] {1, 3, 3, 1, 2, 1},
                                                new[] {3, 1, 3, 1, 2, 1},
                                                new[] {2, 1, 1, 3, 3, 1},
                                                new[] {2, 3, 1, 1, 3, 1}, // 50
                                                new[] {2, 1, 3, 1, 1, 3},
                                                new[] {2, 1, 3, 3, 1, 1},
                                                new[] {2, 1, 3, 1, 3, 1},
                                                new[] {3, 1, 1, 1, 2, 3},
                                                new[] {3, 1, 1, 3, 2, 1}, // 55
                                                new[] {3, 3, 1, 1, 2, 1},
                                                new[] {3, 1, 2, 1, 1, 3},
                                                new[] {3, 1, 2, 3, 1, 1},
                                                new[] {3, 3, 2, 1, 1, 1},
                                                new[] {3, 1, 4, 1, 1, 1}, // 60
                                                new[] {2, 2, 1, 4, 1, 1},
                                                new[] {4, 3, 1, 1, 1, 1},
                                                new[] {1, 1, 1, 2, 2, 4},
                                                new[] {1, 1, 1, 4, 2, 2},
                                                new[] {1, 2, 1, 1, 2, 4}, // 65
                                                new[] {1, 2, 1, 4, 2, 1},
                                                new[] {1, 4, 1, 1, 2, 2},
                                                new[] {1, 4, 1, 2, 2, 1},
                                                new[] {1, 1, 2, 2, 1, 4},
                                                new[] {1, 1, 2, 4, 1, 2}, // 70
                                                new[] {1, 2, 2, 1, 1, 4},
                                                new[] {1, 2, 2, 4, 1, 1},
                                                new[] {1, 4, 2, 1, 1, 2},
                                                new[] {1, 4, 2, 2, 1, 1},
                                                new[] {2, 4, 1, 2, 1, 1}, // 75
                                                new[] {2, 2, 1, 1, 1, 4},
                                                new[] {4, 1, 3, 1, 1, 1},
                                                new[] {2, 4, 1, 1, 1, 2},
                                                new[] {1, 3, 4, 1, 1, 1},
                                                new[] {1, 1, 1, 2, 4, 2}, // 80
                                                new[] {1, 2, 1, 1, 4, 2},
                                                new[] {1, 2, 1, 2, 4, 1},
                                                new[] {1, 1, 4, 2, 1, 2},
                                                new[] {1, 2, 4, 1, 1, 2},
                                                new[] {1, 2, 4, 2, 1, 1}, // 85
                                                new[] {4, 1, 1, 2, 1, 2},
                                                new[] {4, 2, 1, 1, 1, 2},
                                                new[] {4, 2, 1, 2, 1, 1},
                                                new[] {2, 1, 2, 1, 4, 1},
                                                new[] {2, 1, 4, 1, 2, 1}, // 90
                                                new[] {4, 1, 2, 1, 2, 1},
                                                new[] {1, 1, 1, 1, 4, 3},
                                                new[] {1, 1, 1, 3, 4, 1},
                                                new[] {1, 3, 1, 1, 4, 1},
                                                new[] {1, 1, 4, 1, 1, 3}, // 95
                                                new[] {1, 1, 4, 3, 1, 1},
                                                new[] {4, 1, 1, 1, 1, 3},
                                                new[] {4, 1, 1, 3, 1, 1},
                                                new[] {1, 1, 3, 1, 4, 1},
                                                new[] {1, 1, 4, 1, 3, 1}, // 100
                                                new[] {3, 1, 1, 1, 4, 1},
                                                new[] {4, 1, 1, 1, 3, 1},
                                                new[] {2, 1, 1, 4, 1, 2},
                                                new[] {2, 1, 1, 2, 1, 4},
                                                new[] {2, 1, 1, 2, 3, 2}, // 105
                                                new[] {2, 3, 3, 1, 1, 1, 2}
                                             };
    }
}
