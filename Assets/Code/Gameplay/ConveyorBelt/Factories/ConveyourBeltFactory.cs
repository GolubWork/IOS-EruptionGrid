using System.Collections;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Buildings.Configs;
using Code.Infrastructure;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.ConveyorBelt.Factories
{
    public class ConveyourBeltFactory : IConveyourBeltFactory
    {
        private readonly IIdentifierService _identifiers;
        private readonly ICoroutineRunner _coroutineRunner;


        public ConveyourBeltFactory(IIdentifierService identifiers, ICoroutineRunner coroutineRunner)
        {
            _identifiers = identifiers;
            _coroutineRunner = coroutineRunner;
        }
        
        private IEnumerator SetLineRenderer(GameEntity entity, Vector3 startPoint, Vector3 endPoint)
        {
            while (!entity.hasLineRenderer)
            {
                yield return null;
            }

            var lineRenderer = entity.LineRenderer;

            lineRenderer.alignment = LineAlignment.TransformZ; 
            lineRenderer.textureMode = LineTextureMode.Tile;
            lineRenderer.positionCount = 3;
            float yOffset = 0.5f;

            lineRenderer.SetPosition(0, new Vector3(startPoint.x, startPoint.y, startPoint.z + 2));
            lineRenderer.SetPosition(1, new Vector3(endPoint.x, startPoint.y, endPoint.z + 2));
            lineRenderer.SetPosition(2, new Vector3(endPoint.x, endPoint.y - yOffset, endPoint.z + 2));

            lineRenderer.numCornerVertices = 4;
            lineRenderer.numCapVertices = 4;


        }



        public GameEntity CreateBelt(Vector3 at, Vector3 startPoint, Vector3 endPoint, BuildingTypeId buildingTypeId)
        {
            GameEntity belt =  CreateGameEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddViewPath(PrefabsDirectoryConstants.ConveuourBelt)
                    .AddConveyorBeltSpeed(0.5f)
                    .AddItemSpacing(0.3f)
                    .AddConveyourItemsIds(new List<int>())
                    .AddBuildingTypeId(buildingTypeId)
                    .With(e => e.isConveyorBelt = true)
                ;
            _coroutineRunner.StartCoroutine(SetLineRenderer(belt, startPoint, endPoint));
            return belt;
        }
    }
}