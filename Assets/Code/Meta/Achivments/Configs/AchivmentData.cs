 using System;
using UnityEngine.AddressableAssets;

namespace Code.Meta.Achivments.Configs
{
    [Serializable]
    public class AchivmentData
    {
        public AchivmentTypeId AchivmentTypeId;
        public AchivmentStatusId achivmentStatusId;
        public string title;
        public string description;
        public string iconPath;

    }
}