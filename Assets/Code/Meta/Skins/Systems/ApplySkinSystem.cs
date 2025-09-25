using System.Collections.Generic;
using Code.Gameplay.StaticData.SkinStaticData;
using Entitas;

namespace Code.Meta.Skins.Systems
{
    public class ApplySkinSystem: IExecuteSystem
    {
        private readonly ISkinDataProvider _skinDataProvider;
        private readonly IGroup<GameEntity> _skinApplyRequired;
        private readonly IGroup<MetaEntity> _skinDatas;
        private List<GameEntity> _buffer = new (6);

        public ApplySkinSystem(GameContext game, MetaContext meta, ISkinDataProvider skinDataProvider)
        {
            _skinDataProvider = skinDataProvider;
            _skinApplyRequired = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.RequireSkinApplication,
                GameMatcher.SpriteRenderer
            ));

            _skinDatas = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.SelectedSkinStorage
            ));
        }

        public void Execute()
        {
            foreach (GameEntity skinRequire in _skinApplyRequired.GetEntities(_buffer))
            foreach (MetaEntity skinData in _skinDatas)
            {
                skinRequire.SpriteRenderer.sprite = _skinDataProvider.GetSkinDataById(skinData.SelectedSkinStorage).Sprite;
                skinRequire.isRequireSkinApplication = false;
            }
        }
    }
}