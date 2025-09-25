using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Windows.StaticWindows;

namespace Code.Meta.UI.HUD.AchivmentsWindow
{
    public class AchivmentWindowModel
    {
        private readonly IStaticWindowService _staticWindowService;
        private readonly IAudioFactory _audioFactory;

        public AchivmentWindowModel(IStaticWindowService staticWindowService, IAudioFactory audioFactory)
        {
            _staticWindowService = staticWindowService;
            _audioFactory = audioFactory;
        }

        public void ReturnHome()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(StaticWindowId.AchivmentsWindow);
            _staticWindowService.Open(StaticWindowId.HomeWindow);
        }
    }
}