using System;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Infrastructure.Resource;
using Cysharp.Threading.Tasks;

namespace Gameplay.Core
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public sealed class PlayerCreationSystem : ISystem
    {
        private readonly IResourceProvider _resourceProvider;
        private Stash<PlayerComponent> _players;
        private Stash<MoveComponent> _movers;
        private Stash<MoveDirectionComponent> _directions;
        private PlayerProvider _player;

        public PlayerCreationSystem(IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _players = World.GetStash<PlayerComponent>();
            _movers = World.GetStash<MoveComponent>();
            _directions = World.GetStash<MoveDirectionComponent>();

            Debug.Log("Start creating player.");
            _resourceProvider.Get<PlayerProvider>("Player").ContinueWith(CreatePlayer).Forget();
        }

        private void CreatePlayer(PlayerProvider player)
        {
            Debug.Log("Player created.");
            _player = player;


            Entity playerEntity = World.CreateEntity();

            GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawnPoint");
            PlayerProvider playerInstance = UnityEngine.Object.Instantiate(_player, spawnPoint.transform.position, Quaternion.identity);

            PlayerComponent playerComponent = new PlayerComponent();
            MoveComponent moveComponent = new MoveComponent()
            {
                Rigidbody = playerInstance.Rigidbody,
                GameObject = playerInstance.gameObject
            };
            MoveDirectionComponent moveDirectionComponent = new MoveDirectionComponent();
            
            _players.Set(playerEntity, playerComponent);
            _movers.Set(playerEntity, moveComponent);
            _directions.Set(playerEntity, moveDirectionComponent);
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void Dispose()
        {
            _resourceProvider.Release<PlayerProvider>(_player);
        }
    }
}