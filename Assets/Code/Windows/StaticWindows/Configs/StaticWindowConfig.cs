using System;
using UnityEngine;

namespace Code.Windows.StaticWindows.Configs
{
  [Serializable]
  public class StaticWindowConfig
  {
    public StaticWindowId Id;
    public GameObject Prefab;
  }
}