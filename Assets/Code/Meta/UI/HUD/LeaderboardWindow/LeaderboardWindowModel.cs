using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Windows.StaticWindows;
using TMPro;

namespace Code.Meta.UI.HUD.LeaderboardWindow
{
    public class LeaderboardWindowModel
    {
        private readonly IStaticWindowService _staticWindowService;
        private readonly IAudioFactory _audioFactory;

        public LeaderboardWindowModel(IStaticWindowService staticWindowService, IAudioFactory audioFactory)
        {
            _staticWindowService = staticWindowService;
            _audioFactory = audioFactory;
        }
        public void ReturnHome()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(StaticWindowId.LeaderBoardWindow);
            _staticWindowService.Open(StaticWindowId.HomeWindow);
        }
    }
}