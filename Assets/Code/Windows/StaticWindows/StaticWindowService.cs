using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Windows.StaticWindows
{
    public class StaticWindowService : IStaticWindowService
    {
        private readonly IStaticWindowFactory _staticWindowFactory;
        private readonly List<StaticWindow> _opened = new();
        private readonly HashSet<StaticWindowId> _openingNow = new(); // защита от рекурсии

        public StaticWindowService(IStaticWindowFactory factory) => _staticWindowFactory = factory;

        public void Open(StaticWindowId id)
        {
            if (_openingNow.Contains(id)) return;          // уже в процессе открытия
            PurgeDead();
            if (_opened.Any(w => w && w.Id == id)) return; // уже открыто

            try
            {
                _openingNow.Add(id);
                var win = _staticWindowFactory.CreateWindow(id);
                _opened.Add(win);
            }
            finally { _openingNow.Remove(id); }
        }

        public void Close(StaticWindowId id)
        {
            PurgeDead();
            var win = _opened.FirstOrDefault(w => w && w.Id == id);
            if (!win) return;
            _opened.Remove(win);
            if (win.gameObject) Object.Destroy(win.gameObject);
        }

        public void CloseAll()
        {
            foreach (var w in _opened.Where(w => w && w.gameObject).ToList())
                Object.Destroy(w.gameObject);
            _opened.Clear();
        }

        public void CloseAll(StaticWindowId keepId)
        {
            PurgeDead();
            foreach (var w in _opened.Where(w => w && w.Id != keepId).ToList())
            {
                _opened.Remove(w);
                if (w.gameObject) Object.Destroy(w.gameObject);
            }
        }

        private void PurgeDead() => _opened.RemoveAll(w => !w || !w.gameObject);
    }
}