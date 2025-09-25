using System.Collections.Generic;
using UnityEngine;

namespace Code.Windows.StaticWindows
{
  public class StaticWindowService : IStaticWindowService
  {
    private readonly IStaticWindowFactory _staticWindowFactory;

    private readonly List<StaticWindow> _openedWindows = new();

    public StaticWindowService(IStaticWindowFactory staticWindowFactory) =>
      _staticWindowFactory = staticWindowFactory;

    public void Open(StaticWindowId staticWindowId) => 
      _openedWindows.Add(_staticWindowFactory.CreateWindow(staticWindowId));

    public void Close(StaticWindowId staticWindowId)
    {
      StaticWindow window = _openedWindows.Find(x => x.Id == staticWindowId);
      
      _openedWindows.Remove(window);
      
      if (window != null && window.gameObject != null)
      {
        GameObject.Destroy(window.gameObject);
      }
    }

    public void CloseAll()
    {
      List<StaticWindow> windowsCopy = new List<StaticWindow>(_openedWindows);
     
      foreach (StaticWindow window in windowsCopy)
      {
        if (window != null && window.gameObject != null)
        {
          GameObject.Destroy(window.gameObject);
        }
      }
      windowsCopy.Clear();
      _openedWindows.Clear();
    }
    
    public void CloseAll(StaticWindowId staticWindowId)
    {
      List<StaticWindow> windowsCopy = new List<StaticWindow>(_openedWindows);
     
      foreach (StaticWindow window in windowsCopy)
      {
        if (window != null && window.gameObject != null)
        {
            if(window.Id == staticWindowId) continue;
            GameObject.Destroy(window.gameObject);
        }
      }
      windowsCopy.Clear();
      _openedWindows.Clear();
    }
  }
}