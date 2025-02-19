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
    public sealed class PlayerMoveDirectionSystem : ISystem
    {
        private Filter _playersFilter;
        private Filter _inputsFilter;

        private Stash<MoveDirectionComponent> _players;
        private Stash<InputComponent> _inputs;

        public World World { get; set; }

        public void OnAwake()
        {
            _inputsFilter = World.Filter
                                    .With<InputComponent>()
                                    .Build();

            _playersFilter = World.Filter
                                    .With<PlayerComponent>()
                                    .With<MoveDirectionComponent>()
                                    .Build();

            _inputs = World.GetStash<InputComponent>();
            _players = World.GetStash<MoveDirectionComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var input in _inputsFilter)
            {
                foreach (var player in _playersFilter)
                {
                    InputComponent inputComponent = _inputs.Get(input);
                    ref MoveDirectionComponent moveDirectionComponent = ref _players.Get(player);

                    moveDirectionComponent.Direction = new Vector3(inputComponent.X, 0.0f, inputComponent.Z);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}