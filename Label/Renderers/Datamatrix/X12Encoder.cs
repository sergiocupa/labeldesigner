

using System.Text;


namespace Label.Renderers.Datamatrix
{
    internal sealed class X12Encoder : C40Encoder
    {
        public override int EncodingMode
        {
            get { return Encodation.X12; }
        }

        public override void encode(EncoderContext context)
        {
            //step C
            var buffer = new StringBuilder();
            int currentMode = EncodingMode;
            while (context.HasMoreCharacters)
            {
                char c = context.CurrentChar;
                context.Pos++;

                encodeChar(c, buffer);

                int count = buffer.Length;
                if ((count % 3) == 0)
                {
                    writeNextTriplet(context, buffer);

                    int newMode = HighLevelEncoder.lookAheadTest(context.Message, context.Pos, currentMode);
                    if (newMode != currentMode)
                    {
                        // Return to ASCII encodation, which will actually handle latch to new mode
                        context.signalEncoderChange(Encodation.ASCII);
                        break;
                    }
                }
            }
            handleEOD(context, buffer);
        }

        protected override int encodeChar(char c, StringBuilder sb)
        {
            switch (c)
            {
                case '\r':
                    sb.Append('\u0000');
                    break;
                case '*':
                    sb.Append('\u0001');
                    break;
                case '>':
                    sb.Append('\u0002');
                    break;
                case ' ':
                    sb.Append('\u0003');
                    break;
                default:
                    if (c >= '0' && c <= '9')
                    {
                        sb.Append((char)(c - 48 + 4));
                    }
                    else if (c >= 'A' && c <= 'Z')
                    {
                        sb.Append((char)(c - 65 + 14));
                    }
                    else
                    {
                        HighLevelEncoder.illegalCharacter(c);
                    }
                    break;
            }
            return 1;
        }

        protected override void handleEOD(EncoderContext context, StringBuilder buffer)
        {
            context.updateSymbolInfo();
            int available = context.SymbolInfo.dataCapacity - context.CodewordCount;
            int count = buffer.Length;
            context.Pos -= count;
            if (context.RemainingCharacters > 1 || available > 1 ||
                context.RemainingCharacters != available)
                context.writeCodeword(HighLevelEncoder.X12_UNLATCH);
            if (context.NewEncoding < 0)
                context.signalEncoderChange(Encodation.ASCII);
        }
    }
}
