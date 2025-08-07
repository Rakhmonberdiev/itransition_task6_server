namespace itransition_task6_server.Models
{
    public class Presentation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = "New Presentation";
        public string CreatorName { get; set; } = string.Empty;
        public List<Slide> Slides { get; set; } = new List<Slide>();
    }
}
