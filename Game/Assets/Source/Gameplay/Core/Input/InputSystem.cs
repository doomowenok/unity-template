using System;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Gameplay.Core
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public sealed class InputSystem : ISystem
    {
        private readonly IInputController _inputController;

        private Filter _inputsFilter;

        private Stash<InputComponent> _inputs;

        public InputSystem(IInputController inputController)
        {
            _inputController = inputController;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _inputController.Initialize();
            _inputController.Enable();

            _inputsFilter = World.Filter.With<InputComponent>().Build();
            _inputs = World.GetStash<InputComponent>();

            var inputEntity = World.CreateEntity();
            _inputs.Set(inputEntity, new InputComponent());
            Debug.Log($"Input created.");
        }

        public void OnUpdate(float deltaTime)
        {
            Vector2 axisInput = _inputController.GetAxisInput();
            
            foreach (var input in _inputsFilter)
            {
                ref InputComponent inputComponent = ref _inputs.Get(input);
                inputComponent.X = axisInput.x;
                inputComponent.Z = axisInput.y;
            }
        }

        public void Dispose()
        {
            _inputController.Disable();
            _inputController.Dispose();
        }
    }
}