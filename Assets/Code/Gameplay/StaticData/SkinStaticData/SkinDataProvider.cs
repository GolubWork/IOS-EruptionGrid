using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Meta.Skins.Configs;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.StaticData.SkinStaticData
{
    public class SkinDataProvider : ISkinDataProvider
    {
        private readonly IAssetProvider _assetProvider;
        private SkinConfig _skinConfig;
        private Dictionary<SkinTypeId, SkinData> _skins = new Dictionary<SkinTypeId, SkinData>();

        public SkinDataProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask LoadAll()
        {
            await LoadSkins();
        }

        public SkinData GetSkinDataById(SkinTypeId skinTypeId)
        {
            _skins.TryGetValue(skinTypeId, out SkinData skinData);
            return skinData;
        }
        public SkinConfig GetSkinConfig() => _skinConfig;
        
        private async UniTask LoadSkins()
        {
            _skinConfig = await _assetProvider.LoadScriptable<SkinConfig>(ConfigsDirectoryConstants.SkinConfigPath);
            foreach (SkinData data in _skinConfig.SkinDatas)
            {
                _skins.TryAdd(data.skinTypeId, data);
            }
        }
    }
}