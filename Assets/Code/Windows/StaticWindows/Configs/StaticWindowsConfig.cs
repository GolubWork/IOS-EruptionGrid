using System.Collections.Generic;
using UnityEngine;

namespace Code.Windows.StaticWindows.Configs
{
  [CreateAssetMenu(fileName = "StaticWindowConfig", menuName = "Custom/UI/Static Window Config")]
  public class StaticWindowsConfig : ScriptableObject
  {
    public List<StaticWindowConfig> WindowConfigs;
  }
}