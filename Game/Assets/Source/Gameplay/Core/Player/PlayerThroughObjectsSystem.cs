using System;
using Infrastructure.Config;
using Scellecs.Morpeh;
using Source.Gameplay.Core;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Gameplay.Core
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public sealed class PlayerThroughObjectsSystem : ISystem
    {
        private static readonly int PositionId = Shader.PropertyToID("_PlayerPosition");
        private static readonly int SizeId = Shader.PropertyToID("_Size");

        private readonly IConfigProvider _configProvider;
        private WatchThroughObjectsConfig _config;

        private Filter _playersFilter;
        private Stash<MoveComponent> _movers;
        private Stash<PlayerComponent> _players;

        private LayerMask _throughObjectMask;
        private float _raycastMaxDistance;
        private Material _throughObjectMaterial;

        public PlayerThroughObjectsSystem(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _config = _configProvider.GetConfig<WatchThroughObjectsConfig>("Configs/Common");

            _throughObjectMask = _config.WatchThroughLayerMask;
            _raycastMaxDistance = _config.RaycastMaxDistance;
            _throughObjectMaterial = _config.WatchThroughMaterial;

            _playersFilter = World.Filter.With<PlayerComponent>().With<MoveComponent>().Build();
            _movers = World.GetStash<MoveComponent>();
            _players = World.GetStash<PlayerComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity player in _playersFilter)
            {
                ref PlayerComponent playerComponent = ref _players.Get(player);
                ref MoveComponent moveComponent = ref _movers.Get(player);

                Camera camera = playerComponent.Camera;
                Transform playerTransform = moveComponent.GameObject.transform;

                Vector3 direction = camera.transform.position - playerTransform.position;
                Ray ray = new Ray(playerTransform.position, direction.normalized);

                if (Physics.Raycast(ray, _raycastMaxDistance, _throughObjectMask))
                {
                    _throughObjectMaterial.SetFloat(SizeId, 1.0f);
                }
                else
                {
                    _throughObjectMaterial.SetFloat(SizeId, 0.0f);
                }

                Vector3 view = camera.WorldToViewportPoint(playerTransform.position);
                _throughObjectMaterial.SetVector(PositionId, view);
            }
        }

        public void Dispose()
        {
        }
    }
}