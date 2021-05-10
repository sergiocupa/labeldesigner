

using System;
using System.Collections.Generic;


namespace Label.Renderers.Code39
{

    internal class BarCode39Pattern
    {


        internal static int GetMultiplier(char digit, int Narrow)
        {
            var multiplier = 0;

            if (digit == 'W' || digit == 'w')
            {
                multiplier = Narrow;
            }
            else if (digit == 'N' || digit == 'n')
            {
                multiplier = 1;
            }
            else throw new Exception("unrecognized encoder value: " + digit);

            return multiplier;
        }


        internal static Dictionary<char, string> Digits;


        static BarCode39Pattern()
        {
            object[][] chars = new object[][] 
            {
                new object[] {'*', "NwNnWnWnN"},
                new object[] {'-', "NwNnNnWnW"},
                new object[] {'$', "NwNwNwNnN"},
                new object[] {'%', "NnNwNwNwN"},
                new object[] {'_', "NwWnNnWnN"},
                new object[] {'.', "WwNnNnWnN"},
                new object[] {'/', "NwNwNnNwN"},
                new object[] {'+', "NwNnNwNwN"},
                new object[] {'0', "NnNwWnWnN"},
                new object[] {'1', "WnNwNnNnW"},
                new object[] {'2', "NnWwNnNnW"},
                new object[] {'3', "WnWwNnNnN"},
                new object[] {'4', "NnNwWnNnW"},
                new object[] {'5', "WnNwWnNnN"},
                new object[] {'6', "NnWwWnNnN"},
                new object[] {'7', "NnNwNnWnW"},
                new object[] {'8', "WnNwNnWnN"},
                new object[] {'9', "NnWwNnWnN"},
                new object[] {'A', "WnNnNwNnW"},
                new object[] {'B', "NnWnNwNnW"},
                new object[] {'C', "WnWnNwNnN"},
                new object[] {'D', "NnNnWwNnW"},
                new object[] {'E', "WnNnWwNnN"},
                new object[] {'F', "NnWnWwNnN"},
                new object[] {'G', "NnNnNwWnW"},
                new object[] {'H', "WnNnNwWnN"},
                new object[] {'I', "NnWnNwWnN"},
                new object[] {'J', "NnNnWwWnN"},
                new object[] {'K', "WnNnNnNwW"},
                new object[] {'L', "NnWnNnNwW"},
                new object[] {'M', "WnWnNnNwN"},
                new object[] {'N', "NnNnWnNwW"},
                new object[] {'O', "WnNnWnNwN"},
                new object[] {'P', "NnWnWnNwN"},
                new object[] {'Q', "NnNnNnWwW"},
                new object[] {'R', "WnNnNnWwN"},
                new object[] {'S', "NnWnNnWwN"},
                new object[] {'T', "NnNnWnWwN"},
                new object[] {'U', "WwNnNnNnW"},
                new object[] {'V', "NwWnNnNnW"},
                new object[] {'W', "WwWnNnNnN"},
                new object[] {'X', "NwNnWnNnW"},
                new object[] {'Y', "WwNnWnNnN"},
                new object[] {'Z', "NwWnWnNnN"}
            };

            Digits = new Dictionary<char, string>();
            foreach (object[] c in chars)
            {
                Digits.Add((char)c[0], (string)c[1]);
            }
        }

    }



}
