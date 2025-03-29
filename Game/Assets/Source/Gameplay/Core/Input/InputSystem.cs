using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Core
{
    public sealed class InputSystem : ISystem
    {
        private readonly PlayerControls _input;
        
        private Filter _inputFilter;
        private Stash<InputComponent> _inputs;
        
        public World World { get; set; }

        public InputSystem(PlayerControls input)
        {
            _input = input;
        }

        public void OnAwake()
        {
            Entity entity = World.CreateEntity();
            _inputs.Add(entity, new InputComponent());
        }

        public void OnUpdate(float deltaTime)
        {
            Vector2 moveInput = _input.PlayerCore.Move.ReadValue<Vector2>();
            bool performing = _input.PlayerCore.Brake.phase == InputActionPhase.Performed;

            foreach (Entity entity in _inputFilter)
            {
                ref InputComponent input = ref _inputs.Get(entity);

                input.Horizontal = moveInput.x;
                input.Vertical = moveInput.y;
                input.Brake = performing;
            }
        }

        public void Dispose() { }
    }
}