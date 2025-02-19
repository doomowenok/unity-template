using System;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Gameplay.Core
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public sealed class PlayerCreationSystem : ISystem
    {
        private Stash<PlayerComponent> _players;
        private Stash<MoveComponent> _movers;
        private Stash<MoveDirectionComponent> _directions;

        public World World { get; set; }

        public void OnAwake()
        {
            _players = World.GetStash<PlayerComponent>();
            _movers = World.GetStash<MoveComponent>();
            _directions = World.GetStash<MoveDirectionComponent>();

            GameObject playerView = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Entity playerEntity = World.CreateEntity();

            _players.Set(playerEntity, new PlayerComponent());
            _movers.Set(playerEntity, new MoveComponent() { GameObject = playerView });
            _directions.Set(playerEntity, new MoveDirectionComponent());
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void Dispose()
        {
        }
    }
}