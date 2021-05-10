

namespace Label.Renderers.Datamatrix
{
    internal interface Encoder
    {
        int EncodingMode { get; }

        void encode(EncoderContext context);
    }
}
