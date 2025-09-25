using System;

namespace Code.Gameplay.Zones.Configs
{
    [Flags]
    public enum ZoneSideTypeId: byte
    {
        Unknown = 1 << 0,
        Left = 1 << 1,
        Right = 1 << 2,
        Top = 1 << 3,
        Bottom = 1 << 4,
    }
}