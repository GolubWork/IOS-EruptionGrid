using Code.Audios.Audio.Configs;
using Code.Meta.UI.HUD.SettingsWindow;
using Code.Progress;
using Entitas;
using UnityEngine;

namespace Code.Audios.Audio
{
    [Audio] public class AudioSettingsComponent : ISavedComponent { public AudioSettingsData Value; }
    
    [Audio] public class MusicSource : IComponent { public AudioSource Value; }
    [Audio] public class SoundSource : IComponent { public AudioSource Value; }
    [Audio] public class MusicComponent : IComponent { public MusicTypeId Value; }
    [Audio] public class SoundComponent : IComponent { public SoundTypeId Value; }
    [Audio] public class Processed : IComponent { }
    
    [Audio] public class SettingsControllerComponent : IComponent { public ISettingsController Value; }
    [Audio] public class MusicVolumeChanger : IComponent { }
   
    [Audio] public class SoundVolumeChanger : IComponent { }
    [Audio] public class Volume : IComponent { public float Value; }
    [Audio] public class SelfDestructTimer : IComponent { public float Value; }

}