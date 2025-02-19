using Gameplay.Core.Input;
using UnityEngine;

namespace Gameplay.Core
{
    public sealed class InputController : IInputController
    {
        private PlayerControls _controls;

        public void Initialize()
        {
            _controls = new PlayerControls();
        }

        public void Enable()
        {
            _controls.Enable();
        }

        public void Disable()
        {
            _controls.Disable();
        }

        public void Dispose()
        {
            _controls.Dispose();
            _controls = null;
        }

        public Vector2 GetAxisInput()
        {
            return _controls.Player.Move.ReadValue<Vector2>();
        }
    }
}