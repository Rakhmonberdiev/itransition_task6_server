using itransition_task6_server.Models;

namespace itransition_task6_server.Data
{
    public interface IPresentationStorage
    {
        IEnumerable<Presentation> GetAll();
        Presentation? GetById(string id);
        void Add(Presentation presentation);
        void Update(Presentation presentation);
        void Remove(string id);
    }
}
