namespace Codebase.Gifts
{
    public class Gift
    {
        public Box Box;
        public Bow Bow;
        public Design Design;

        public Gift (Box box, Bow bow, Design design)
        {
            Box = box;
            Bow = bow;
            Design = design;
        }
        
        public Gift ()
        {
            Box = null;
            Bow = null;
            Design = null;
        }

        public Gift Clone()
        {
            return new Gift(Box, Bow, Design);
        }

        public void Copy(Gift from)
        {
            Box = from.Box;
            Bow = from.Bow;
            Design = from.Design;
        }
        
        public void ApplyGiftPart(GiftPart part)
        {
            switch (part)
            {
                case Box box:
                    Box = box;
                    break;
                case Bow bow:
                    Bow = bow;
                    break;
                case Design design:
                    Design = design;
                    break;
            }
        }

        public bool Compare(Gift gift)
        {
            return gift.Box == Box && gift.Bow == Bow && gift.Design == Design;
        }
    }
}