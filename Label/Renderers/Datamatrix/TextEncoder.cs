﻿

using System.Text;


namespace Label.Renderers.Datamatrix
{
    internal sealed class TextEncoder : C40Encoder
    {
        public override int EncodingMode
        {
            get { return Encodation.TEXT; }
        }

        protected override int encodeChar(char c, StringBuilder sb)
        {
            if (c == ' ')
            {
                sb.Append('\u0003');
                return 1;
            }
            if (c >= '0' && c <= '9')
            {
                sb.Append((char)(c - 48 + 4));
                return 1;
            }
            if (c >= 'a' && c <= 'z')
            {
                sb.Append((char)(c - 97 + 14));
                return 1;
            }
            if (c <= '\u001f')
            {
                sb.Append('\u0000'); //Shift 1 Set
                sb.Append(c);
                return 2;
            }
            if (c <= '/')
            {
                sb.Append('\u0001'); //Shift 2 Set
                sb.Append((char)(c - 33));
                return 2;
            }
            if (c <= '@')
            {
                sb.Append('\u0001'); //Shift 2 Set
                sb.Append((char)(c - 58 + 15));
                return 2;
            }
            if (c >= '[' && c <= '_')
            {
                sb.Append('\u0001'); //Shift 2 Set
                sb.Append((char)(c - 91 + 22));
                return 2;
            }
            if (c == '\u0060')
            {
                sb.Append('\u0002'); //Shift 3 Set
                sb.Append((char)(c - 96));
                return 2;
            }
            if (c <= 'Z')
            {
                sb.Append('\u0002'); //Shift 3 Set
                sb.Append((char)(c - 65 + 1));
                return 2;
            }
            if (c <= '\u007f')
            {
                sb.Append('\u0002'); //Shift 3 Set
                sb.Append((char)(c - 123 + 27));
                return 2;
            }
            sb.Append("\u0001\u001e"); //Shift 2, Upper Shift
            int len = 2;
            len += encodeChar((char)(c - 128), sb);
            return len;
        }
    }
}
