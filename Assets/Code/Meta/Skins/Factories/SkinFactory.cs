using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData.SkinStaticData;
using Code.Infrastructure.Identifiers;
using Code.Meta.Skins.Configs;

namespace Code.Meta.Skins.Factories
{
    public class SkinFactory : ISkinFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly ISkinDataProvider _skinDataProvider;

        public SkinFactory(IIdentifierService identifierService, ISkinDataProvider skinDataProvider)
        {
            _identifierService = identifierService;
            _skinDataProvider = skinDataProvider;
        }


        public MetaEntity UnlockSkinRequest(SkinTypeId skinTypeId)
        {
            return CreateMetaEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddRequestSkinTypeId(skinTypeId)
                ;
        }
        
        public MetaEntity InitSelectedSkin()
        {
            return CreateMetaEntity.Empty()
                    .AddSelectedSkinStorage(_skinDataProvider.GetSkinConfig().SkinDatas.Find(skin => skin.skinStatusId == SkinStatusId.Selected).skinTypeId)
                    .With(e => e.isStorage = true)
                ;
        }
        
        public MetaEntity RequestChangeSelectedSkin(SkinTypeId skinTypeId)
        {
            return CreateMetaEntity.Empty()
                    .AddRequestSkinTypeId(skinTypeId)
                    .With(e => e.isChangeSkinRequest = true)
                ;
        }
    }
}