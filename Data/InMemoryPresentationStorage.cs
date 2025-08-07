using itransition_task6_server.Models;
using System.Collections.Concurrent;

namespace itransition_task6_server.Data
{
    public class InMemoryPresentationStorage : IPresentationStorage
    {
        private readonly ConcurrentDictionary<string, Presentation> _presentations = new ConcurrentDictionary<string, Presentation>();

        public InMemoryPresentationStorage(IEnumerable<Presentation> initialPresentations)
        {
            foreach (var pres in initialPresentations)
            {
                _presentations[pres.Id] = pres;
            }
        }
        public void Add(Presentation presentation)
        {
            _presentations[presentation.Id] = presentation;
        }

        public IEnumerable<Presentation> GetAll() => _presentations.Values;

        public Presentation? GetById(string id) =>
            _presentations.TryGetValue(id, out var pres) ? pres : null;

        public void Remove(string id)
        {
            _presentations.TryRemove(id, out _);
        }

        public void Update(Presentation presentation)
        {
            _presentations[presentation.Id] = presentation;
        }
    }
}
