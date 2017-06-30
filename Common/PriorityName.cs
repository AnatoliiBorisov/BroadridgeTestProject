namespace BroadridgeTestProject.Common
{
    //TODO: to generic
    public class PriorityName
    {
        public Priority Priority { get; }

        public string Name { get; }

        public PriorityName(Priority priority, string name)
        {
            Priority = priority;

            Name = name;
        }
    }
}