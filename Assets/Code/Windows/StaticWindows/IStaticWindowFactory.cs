using UnityEngine;

namespace Code.Windows.StaticWindows
{
  public interface IStaticWindowFactory
  {
    public void SetUiRoot(GameObject uiRoot);
    StaticWindow CreateWindow(StaticWindowId id);
  }
}