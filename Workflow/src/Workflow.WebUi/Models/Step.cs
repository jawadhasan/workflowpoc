namespace Workflow.WebUi.Models
{
    public class Step
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public Step(long id, string name, string title)
        {
            Id = id;
            Name = name;
            Title = title;
        }
    }
}