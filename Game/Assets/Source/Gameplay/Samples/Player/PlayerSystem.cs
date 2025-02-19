using System;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Gameplay.Samples
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public sealed class PlayerSystem : ISystem
    {
        private Filter _filter;
        private Stash<PlayerComponent> _playerStash;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<PlayerComponent>().Build();
            _playerStash = World.GetStash<PlayerComponent>();

            GameObject playerObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Entity playerEntity = World.CreateEntity();
            _playerStash.Set(playerEntity, new PlayerComponent() { Transform = playerObject });
        }

        public void OnUpdate(float deltaTime)
        {
            foreach(var player in _filter)
            {
                ref PlayerComponent playerComp = ref _playerStash.Get(player);
                playerComp.Transform.transform.position += Vector3.one * deltaTime;
            }
        }

        public void Dispose()
        {
        }
    }
}