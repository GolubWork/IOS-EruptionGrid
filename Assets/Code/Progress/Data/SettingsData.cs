using Newtonsoft.Json;

namespace Code.Progress.Data
{
    public class SettingsData
    {
        [JsonProperty("musicVolume")] public float MusicVolume;
        [JsonProperty("soundVolume")] public float SoundVolume;
    }
}