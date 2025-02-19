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
    public sealed class MovingSystem : ISystem
    {
        private Filter _moversFilter;
        private Stash<MoveComponent> _movers;
        private Stash<MoveDirectionComponent> _directions;

        public World World { get; set; }

        public void OnAwake()
        {
            _moversFilter = World.Filter
                                .With<MoveComponent>()
                                .With<MoveDirectionComponent>()
                                .Build();

            _movers = World.GetStash<MoveComponent>();
            _directions = World.GetStash<MoveDirectionComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var mover in _moversFilter)
            {
                ref MoveComponent moveComponent = ref _movers.Get(mover);
                MoveDirectionComponent directionComponent = _directions.Get(mover);

                // moveComponent.CharacterController.Move(directionComponent.Direction);
                moveComponent.GameObject.transform.position += directionComponent.Direction;
            }
        }

        public void Dispose()
        {
        }
    }
}


