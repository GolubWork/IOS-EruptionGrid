using UnityEngine;

namespace Code.Windows.StaticWindows
{
  public interface IStaticWindowFactory
  {
    public void SetUIRoot(RectTransform uiRoot);
    public StaticWindow CreateWindow(StaticWindowId staticWindowId);
  }
}