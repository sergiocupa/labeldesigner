

using System;
using System.Text;


namespace Label.Renderers.Datamatrix
{
    internal sealed class Base256Encoder : Encoder
    {
        public int EncodingMode
        {
            get { return Encodation.BASE256; }
        }

        public void encode(EncoderContext context)
        {
            var buffer = new StringBuilder();
            buffer.Append('\u0000'); //Initialize length field
            while (context.HasMoreCharacters)
            {
                char c = context.CurrentChar;
                buffer.Append(c);

                context.Pos++;

                int newMode = HighLevelEncoder.lookAheadTest(context.Message, context.Pos, EncodingMode);
                if (newMode != EncodingMode)
                {
                    // Return to ASCII encodation, which will actually handle latch to new mode
                    context.signalEncoderChange(Encodation.ASCII);
                    break;
                }
            }
            int dataCount = buffer.Length - 1;
            const int lengthFieldSize = 1;
            int currentSize = context.CodewordCount + dataCount + lengthFieldSize;
            context.updateSymbolInfo(currentSize);
            bool mustPad = (context.SymbolInfo.dataCapacity - currentSize) > 0;
            if (context.HasMoreCharacters || mustPad)
            {
                if (dataCount <= 249)
                {
                    buffer[0] = (char)dataCount;
                }
                else if (dataCount <= 1555)
                {
                    buffer[0] = (char)((dataCount / 250) + 249);
                    buffer.Insert(1, new[] { (char)(dataCount % 250) });
                }
                else
                {
                    throw new InvalidOperationException(
                        "Message length not in valid ranges: " + dataCount);
                }
            }
            {
                var c = buffer.Length;
                for (int i = 0; i < c; i++)
                {
                    context.writeCodeword(randomize255State(
                       buffer[i], context.CodewordCount + 1));
                }
            }
        }

        private static char randomize255State(char ch, int codewordPosition)
        {
            int pseudoRandom = ((149 * codewordPosition) % 255) + 1;
            int tempVariable = ch + pseudoRandom;
            if (tempVariable <= 255)
            {
                return (char)tempVariable;
            }
            return (char)(tempVariable - 256);
        }
    }
}
