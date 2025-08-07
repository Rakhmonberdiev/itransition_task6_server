using System.Text.Json.Serialization;

namespace itransition_task6_server.Models
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(TextBlock), "text")]
    [JsonDerivedType(typeof(ImageBlock), "image")]
    public class SlideElement
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
