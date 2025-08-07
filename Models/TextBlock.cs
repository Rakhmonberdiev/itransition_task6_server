namespace itransition_task6_server.Models
{
    public class TextBlock : SlideElement
    {
        public string Text { get; set; } = string.Empty;
        public string FontFamily { get; set; } = "Segoe UI";
        public double FontSize { get; set; } = 24;
        public string Color { get; set; } = "#1a1a1a";
    }
}
