﻿

using Label.Renderers.Base;
using System;


namespace Label.Renderers.Qrcode
{
    internal static class MaskUtil
    {
        // Penalty weights from section 6.8.2.1
        private const int N1 = 3;
        private const int N2 = 3;
        private const int N3 = 40;
        private const int N4 = 10;


        public static int applyMaskPenaltyRule1(ByteMatrix matrix)
        {
            return applyMaskPenaltyRule1Internal(matrix, true) + applyMaskPenaltyRule1Internal(matrix, false);
        }


        public static int applyMaskPenaltyRule2(ByteMatrix matrix)
        {
            int penalty = 0;
            var array = matrix.Array;
            int width = matrix.Width;
            int height = matrix.Height;
            for (int y = 0; y < height - 1; y++)
            {
                var arrayY = array[y];
                var arrayY1 = array[y + 1];
                for (int x = 0; x < width - 1; x++)
                {
                    int value = arrayY[x];
                    if (value == arrayY[x + 1] && value == arrayY1[x] && value == arrayY1[x + 1])
                    {
                        penalty++;
                    }
                }
            }
            return N2 * penalty;
        }


        public static int applyMaskPenaltyRule3(ByteMatrix matrix)
        {
            int numPenalties = 0;
            byte[][] array = matrix.Array;
            int width = matrix.Width;
            int height = matrix.Height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte[] arrayY = array[y];  // We can at least optimize this access
                    if (x + 6 < width &&
                        arrayY[x] == 1 &&
                        arrayY[x + 1] == 0 &&
                        arrayY[x + 2] == 1 &&
                        arrayY[x + 3] == 1 &&
                        arrayY[x + 4] == 1 &&
                        arrayY[x + 5] == 0 &&
                        arrayY[x + 6] == 1 &&
                        (isWhiteHorizontal(arrayY, x - 4, x) || isWhiteHorizontal(arrayY, x + 7, x + 11)))
                    {
                        numPenalties++;
                    }
                    if (y + 6 < height &&
                        array[y][x] == 1 &&
                        array[y + 1][x] == 0 &&
                        array[y + 2][x] == 1 &&
                        array[y + 3][x] == 1 &&
                        array[y + 4][x] == 1 &&
                        array[y + 5][x] == 0 &&
                        array[y + 6][x] == 1 &&
                        (isWhiteVertical(array, x, y - 4, y) || isWhiteVertical(array, x, y + 7, y + 11)))
                    {
                        numPenalties++;
                    }
                }
            }
            return numPenalties * N3;
        }

        private static bool isWhiteHorizontal(byte[] rowArray, int from, int to)
        {
            from = Math.Max(from, 0);
            to = Math.Min(to, rowArray.Length);
            for (int i = from; i < to; i++)
            {
                if (rowArray[i] == 1)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool isWhiteVertical(byte[][] array, int col, int from, int to)
        {
            from = Math.Max(from, 0);
            to = Math.Min(to, array.Length);
            for (int i = from; i < to; i++)
            {
                if (array[i][col] == 1)
                {
                    return false;
                }
            }
            return true;
        }



        public static int applyMaskPenaltyRule4(ByteMatrix matrix)
        {
            int numDarkCells = 0;
            var array = matrix.Array;
            int width = matrix.Width;
            int height = matrix.Height;
            for (int y = 0; y < height; y++)
            {
                var arrayY = array[y];
                for (int x = 0; x < width; x++)
                {
                    if (arrayY[x] == 1)
                    {
                        numDarkCells++;
                    }
                }
            }
            var numTotalCells = matrix.Height * matrix.Width;
            var darkRatio = (double)numDarkCells / numTotalCells;
            var fivePercentVariances = (int)(Math.Abs(darkRatio - 0.5) * 20.0); // * 100.0 / 5.0
            return fivePercentVariances * N4;
        }



        public static bool getDataMaskBit(int maskPattern, int x, int y)
        {
            int intermediate, temp;
            switch (maskPattern)
            {

                case 0:
                    intermediate = (y + x) & 0x1;
                    break;

                case 1:
                    intermediate = y & 0x1;
                    break;

                case 2:
                    intermediate = x % 3;
                    break;

                case 3:
                    intermediate = (y + x) % 3;
                    break;

                case 4:
                    intermediate = (((int)((uint)y >> 1)) + (x / 3)) & 0x1;
                    break;

                case 5:
                    temp = y * x;
                    intermediate = (temp & 0x1) + (temp % 3);
                    break;

                case 6:
                    temp = y * x;
                    intermediate = (((temp & 0x1) + (temp % 3)) & 0x1);
                    break;

                case 7:
                    temp = y * x;
                    intermediate = (((temp % 3) + ((y + x) & 0x1)) & 0x1);
                    break;

                default:
                    throw new ArgumentException("Invalid mask pattern: " + maskPattern);

            }
            return intermediate == 0;
        }



        private static int applyMaskPenaltyRule1Internal(ByteMatrix matrix, bool isHorizontal)
        {
            int penalty = 0;
            int iLimit = isHorizontal ? matrix.Height : matrix.Width;
            int jLimit = isHorizontal ? matrix.Width : matrix.Height;
            var array = matrix.Array;
            for (int i = 0; i < iLimit; i++)
            {
                int numSameBitCells = 0;
                int prevBit = -1;
                for (int j = 0; j < jLimit; j++)
                {
                    int bit = isHorizontal ? array[i][j] : array[j][i];
                    if (bit == prevBit)
                    {
                        numSameBitCells++;
                    }
                    else
                    {
                        if (numSameBitCells >= 5)
                        {
                            penalty += N1 + (numSameBitCells - 5);
                        }
                        numSameBitCells = 1;  // Include the cell itself.
                        prevBit = bit;
                    }
                }
                if (numSameBitCells >= 5)
                {
                    penalty += N1 + (numSameBitCells - 5);
                }
            }
            return penalty;
        }
    }
}
