namespace itransition_task6_server.Models
{
    public class Slide
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Order { get; set; } = 0;
        public List<SlideElement> Elements { get; set; } = new List<SlideElement>();
    }
}
