using System.Collections.Generic;
using System.Linq;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Common;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using Code.Infrastructure.EntityViews.Registrars;
using Code.Infrastructure.Identifiers;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Objects.Factories
{
    public abstract class AObjectPool : IObjectPool
    {
        protected readonly IIdentifierService _identifierService;
        protected readonly HashSet<GameEntity> _objects = new HashSet<GameEntity>();
        protected readonly Vector3 Far = new Vector3(-999, -999, -999);


        protected virtual int ObjectsCount => 10;


        protected AObjectPool(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public virtual void Initialize()
        {
            for (int i = 0; i < ObjectsCount; i++)
            {
                GameEntity obj = BuildDefaultObject(CreateGameEntity.Empty());
                _objects.Add(obj);
            }
        }

        public void CleanUp()
        {
            _objects.Clear();
        }

        public virtual GameEntity ReserveDefaultObject()
        {
            GameEntity obj = null;
            if (_objects.Count > 0)
            {
                int attempts = 0;
                const int maxAttempts = 2;
                while (attempts++ < maxAttempts)
                {
                    obj = _objects.PickRandom();
                    if (!obj.isReserved)
                    {
                        obj.isReserved = true;
                        _objects.Remove(obj);
                        if (_objects.Count <= 5)
                        {
                            GameEntity newObject = BuildDefaultObject(CreateGameEntity.Empty());
                            _objects.Add(newObject);
                        }
                        return obj;
                    }
                }
            }

            obj = BuildDefaultObject(CreateGameEntity.Empty());
            obj.isReserved = true;
            return obj;
        }

        public virtual void ReturnDefaultObject(GameEntity obj)
        {
            obj.ReplaceWorldPosition(Far);
            RemoveNotDefaultComponents(obj);
            _objects.Add(obj);
        }

        protected virtual void RemoveNotDefaultComponents(GameEntity entity)
        {
            int[] nonDefaultComponentIndices = entity.GetComponentIndices()
                .Where(index => !IsDefaultComponent(index))
                .ToArray();
            foreach (var index in nonDefaultComponentIndices)
            {
                entity.RemoveComponent(index);
            }
        }

        protected virtual GameEntity BuildDefaultObject(GameEntity obj)
        {
            obj
                .AddId(_identifierService.Next())
                .AddViewPath(PrefabsDirectoryConstants.DefaultObjectPrefabPath)
                .AddWorldPosition(Far)
                .With(e => e.isObjectFromPool = true)
                ;
            return obj;
        }

        protected virtual bool IsDefaultComponent(int index)
        {
            return index == GameComponentsLookup.Transform
                   || index == GameComponentsLookup.View
                   || index == GameComponentsLookup.Id
                   || index == GameComponentsLookup.WorldPosition
                   || index == GameComponentsLookup.SpriteRenderer
                ;
        }
    }
}