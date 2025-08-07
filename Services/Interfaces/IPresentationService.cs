using itransition_task6_server.Models;

namespace itransition_task6_server.Services.Interfaces
{
    public interface IPresentationService
    {
        IEnumerable<Presentation> GetAll();
        Presentation? GetById(string id);
        Slide? GetSlide(string presentationId, string slideId);
        string CreatePresentation(string title, string username);
        void AddSlide(string presentationId, Slide slide);
        void AddElement(string presentationId, string slideId, SlideElement element);
        void RemoveElement(string presentationId, string slideId, string elementId);
        void UpdateElement(string presentationId, string slideId, SlideElement element);
    }
}
