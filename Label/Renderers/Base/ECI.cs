

namespace Label.Renderers.Base
{
    public abstract class ECI
    {

        public virtual int Value { get; private set; }

        internal ECI(int val)
        {
            Value = val;
        }


        public static ECI getECIByValue(int val)
        {
            if (val < 0 || val > 999999)
            {
                throw new System.ArgumentException("Bad ECI value: " + val);
            }
            if (val < 900)
            {
                // Character set ECIs use 000000 - 000899
                return CharacterSetECI.getCharacterSetECIByValue(val);
            }
            return null;
        }
    }
}
