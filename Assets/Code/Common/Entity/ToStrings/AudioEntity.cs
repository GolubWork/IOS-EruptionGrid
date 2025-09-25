
    

    using System;
    using System.Linq;
    using System.Text;
    using Code.Common.Extensions;
    using Entitas;
    using UnityEngine;
    // ReSharper disable once CheckNamespace
    using Code.Common.Entity.ToStrings;

    public  sealed partial class AudioEntity : INamedEntity
    {
        private EntityPrinter _printer;

        public override string ToString()
        {
            if (_printer == null)
                _printer = new EntityPrinter(this);

            _printer.InvalidateCache();

            return _printer.BuildToString();
        }

        public string EntityName(IComponent[] components)
        {
            try
            {
                if (components.Length == 1)
                    return components[0].GetType().Name;

                foreach (IComponent component in components)
                {
                    switch (component.GetType().Name)
                    {
                        case nameof(MusicSource):
                            return PrintMusicSource();

                        case nameof(SoundSource):
                            return PrintSoundSource();
                        
                        case nameof(AudioListener):
                            return PrintListener();
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }

            return components.First().GetType().Name;
        }

        private string PrintListener() =>
            new StringBuilder($"Audio Listener ")
                .With(s => s.Append($"Id:{Id}"), when: hasId)
                .ToString();

        private string PrintMusicSource() =>
            new StringBuilder($"Music Source ")
                .With(s => s.Append($"Id:{Id}"), when: hasId)
                .ToString();

        private string PrintSoundSource() =>
            new StringBuilder($"Sound Source ")
                .With(s => s.Append($"Id:{Id}"), when: hasId)
                .ToString();

        public string BaseToString() => base.ToString();
    }
