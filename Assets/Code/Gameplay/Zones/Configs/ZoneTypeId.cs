namespace Code.Gameplay.Zones.Configs
{
    public enum ZoneTypeId
    {
        OverCameraViewLeft = ZoneCameraTypeId.OverCameraView & ZoneSideTypeId.Left,
        OverCameraViewRight = ZoneCameraTypeId.OverCameraView & ZoneSideTypeId.Right,
        OverCameraViewTop = ZoneCameraTypeId.OverCameraView & ZoneSideTypeId.Top,
        OverCameraViewBottom = ZoneCameraTypeId.OverCameraView & ZoneSideTypeId.Bottom,

        InCameraViewLeft = ZoneCameraTypeId.InCameraView & ZoneSideTypeId.Left,
        InCameraViewRight = ZoneCameraTypeId.InCameraView & ZoneSideTypeId.Right,
        InCameraViewTop = ZoneCameraTypeId.InCameraView & ZoneSideTypeId.Top,
        InCameraViewBottom = ZoneCameraTypeId.InCameraView & ZoneSideTypeId.Bottom,
    }
}