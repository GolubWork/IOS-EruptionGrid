using System;

namespace Code.Gameplay.Zones.Configs
{
    [Flags]
    public enum ZoneCameraTypeId: byte
    {
        Unknown = 1 << 0,
        OverCameraView = 1 << 1,
        InCameraView = 1 << 2,
    }
}