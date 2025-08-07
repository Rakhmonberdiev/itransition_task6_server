using itransition_task6_server.Data;
using itransition_task6_server.Models;
using itransition_task6_server.Services.Interfaces;

namespace itransition_task6_server.Services
{
    public class PresentationService(IPresentationStorage  storage) : IPresentationService
    {
        public void AddElement(string presentationId, string slideId, SlideElement element)
        {
            var slide = GetSlide(presentationId, slideId);
            if (slide is null) return;

            slide.Elements.Add(element);
            var pres = storage.GetById(presentationId);
            storage.Update(pres!);
        }

        public void AddSlide(string presentationId, Slide slide)
        {
            var pres = storage.GetById(presentationId);
            if (pres is null) return;
            pres.Slides.Add(slide);
            storage.Update(pres);
        }

        public string CreatePresentation(string title, string username)
        {
            var model = new Presentation
            {
                Title = title,
                CreatorName = username,
                Slides = new List<Slide>()
            };
            storage.Add(model);
            return model.Id;

        }

        public IEnumerable<Presentation> GetAll() => storage.GetAll();

        public Presentation? GetById(string id) => storage.GetById(id);

        public Slide? GetSlide(string presentationId, string slideId)
        {
            var pres = storage.GetById(presentationId);
            return pres?.Slides.FirstOrDefault(s => s.Id == slideId);
        }

        public void RemoveElement(string presentationId, string slideId, string elementId)
        {
            var slide = GetSlide(presentationId, slideId);
            if (slide is null) return;

            slide.Elements.RemoveAll(e => e.Id == elementId);
            var pres = storage.GetById(presentationId);
            storage.Update(pres!);
        }

        public void UpdateElement(string presentationId, string slideId, SlideElement element)
        {
            var slide = GetSlide(presentationId, slideId);
            if (slide is null) return;

            var index = slide.Elements.FindIndex(e => e.Id == element.Id);
            if (index != -1)
            {
                slide.Elements[index] = element;
                var pres = storage.GetById(presentationId);
                storage.Update(pres!);
            }
        }
    }
}
