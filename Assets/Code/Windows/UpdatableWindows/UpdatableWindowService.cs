using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Windows.UpdatableWindows
{
    public class UpdatableWindowService : IUpdatableWindowService
    {
        private readonly IUpdatableWindowFactory _factory;
        private readonly List<UpdatableWindow> _opened = new();
        private readonly HashSet<UpdatableWindowId> _openingNow = new(); // анти-рекурсия

        public UpdatableWindowService(IUpdatableWindowFactory factory) => _factory = factory;

        public void Open(UpdatableWindowId id)
        {
            if (_openingNow.Contains(id)) return;
            PurgeDead();
            if (_opened.Any(w => w && w.Id == id)) return;

            try
            {
                _openingNow.Add(id);
                var w = _factory.CreateWindow(id);
                _opened.Add(w);
            }
            finally { _openingNow.Remove(id); }
        }

        public void Close(UpdatableWindowId id)
        {
            PurgeDead();
            var w = _opened.FirstOrDefault(x => x && x.Id == id);
            if (!w) return;
            _opened.Remove(w);
            if (w.gameObject) Object.Destroy(w.gameObject);
        }

        public void CloseAll()
        {
            foreach (var w in _opened.Where(x => x && x.gameObject).ToList())
                Object.Destroy(w.gameObject);
            _opened.Clear();
        }

        private void PurgeDead() => _opened.RemoveAll(x => !x || !x.gameObject);
    }
}