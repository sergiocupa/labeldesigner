﻿

using Label.Renderers.Base;
using System;
using System.Text;


namespace Label.Renderers.Datamatrix
{
    internal sealed class EncoderContext
    {
        private readonly String msg;
        private ESymbolShapeHint shape;
        private Dimension minSize;
        private Dimension maxSize;
        private readonly StringBuilder codewords;
        private int pos;
        private int newEncoding;
        private SymbolInfo symbolInfo;
        private int skipAtEnd;
        private static readonly Encoding encoding;

        static EncoderContext()
        {
#if !(WindowsCE || SILVERLIGHT4 || SILVERLIGHT5 || NETFX_CORE || PORTABLE)
            encoding = Encoding.GetEncoding("ISO-8859-1");
#elif WindowsCE
         try
         {
            encoding = Encoding.GetEncoding("ISO-8859-1");
         }
         catch (PlatformNotSupportedException)
         {
            encoding = Encoding.GetEncoding(1252);
         }
#else
         // not fully correct but what else
         encoding = Encoding.GetEncoding("UTF-8");
#endif
        }

        public EncoderContext(String msg)
        {
            //From this point on Strings are not Unicode anymore!
            var msgBinary = encoding.GetBytes(msg);
            var sb = new StringBuilder(msgBinary.Length);
            var c = msgBinary.Length;
            for (int i = 0; i < c; i++)
            {
                // TODO: does it works in .Net the same way?
                var ch = (char)(msgBinary[i] & 0xff);
                if (ch == '?' && msg[i] != '?')
                {
                    throw new ArgumentException("Message contains characters outside " + encoding.WebName + " encoding.");
                }
                sb.Append(ch);
            }
            this.msg = sb.ToString(); //Not Unicode here!
            shape = ESymbolShapeHint.FORCE_NONE;
            this.codewords = new StringBuilder(msg.Length);
            newEncoding = -1;
        }

        public void setSymbolShape(ESymbolShapeHint shape)
        {
            this.shape = shape;
        }

        public void setSizeConstraints(Dimension minSize, Dimension maxSize)
        {
            this.minSize = minSize;
            this.maxSize = maxSize;
        }

        public void setSkipAtEnd(int count)
        {
            this.skipAtEnd = count;
        }

        public char CurrentChar
        {
            get { return msg[pos]; }
        }

        public char Current
        {
            get { return msg[pos]; }
        }

        public void writeCodewords(String codewords)
        {
            this.codewords.Append(codewords);
        }

        public void writeCodeword(char codeword)
        {
            this.codewords.Append(codeword);
        }

        public int CodewordCount
        {
            get { return this.codewords.Length; }
        }

        public void signalEncoderChange(int encoding)
        {
            this.newEncoding = encoding;
        }

        public void resetEncoderSignal()
        {
            this.newEncoding = -1;
        }

        public bool HasMoreCharacters
        {
            get { return pos < TotalMessageCharCount; }
        }

        private int TotalMessageCharCount
        {
            get { return msg.Length - skipAtEnd; }
        }

        public int RemainingCharacters
        {
            get { return TotalMessageCharCount - pos; }
        }

        public void updateSymbolInfo()
        {
            updateSymbolInfo(CodewordCount);
        }

        public void updateSymbolInfo(int len)
        {
            if (this.symbolInfo == null || len > this.symbolInfo.dataCapacity)
            {
                this.symbolInfo = SymbolInfo.lookup(len, shape, minSize, maxSize, true);
            }
        }

        public void resetSymbolInfo()
        {
            this.symbolInfo = null;
        }

        public int Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public StringBuilder Codewords
        {
            get { return codewords; }
        }

        public SymbolInfo SymbolInfo
        {
            get { return symbolInfo; }
        }

        public int NewEncoding
        {
            get { return newEncoding; }
        }

        public String Message
        {
            get { return msg; }
        }

        public bool Fnc1CodewordIsWritten { get; set; }
    }
}
