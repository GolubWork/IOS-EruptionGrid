using Code.Meta.Skins.Configs;

namespace Code.Meta.Skins.Factories
{
    public interface ISkinFactory
    {
        MetaEntity UnlockSkinRequest(SkinTypeId skinTypeId);
        MetaEntity InitSelectedSkin();
        MetaEntity RequestChangeSelectedSkin(SkinTypeId skinTypeId);
    }
}