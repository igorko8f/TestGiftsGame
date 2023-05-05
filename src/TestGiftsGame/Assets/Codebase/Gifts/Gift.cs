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
    }
}