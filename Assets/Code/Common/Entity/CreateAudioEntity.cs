namespace Code.Common.Entity
{
    public static class CreateAudioEntity
    {
        public static AudioEntity Empty() =>
            Contexts.sharedInstance.audio.CreateEntity();
    }
}