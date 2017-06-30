namespace BroadridgeTestProject.Common
{
    //TODO: to generic
    public class ColorName
    {
        public Color Color { get; }

        public string Name { get; }

        public ColorName(Color color, string name)
        {
            Color = color;

            Name = name;
        }
    }
}