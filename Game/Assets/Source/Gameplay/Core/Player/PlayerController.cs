using Gameplay.Core.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Core
{
    public class PlayerController
    {
        private PlayerControls _playerControls;
        private BaseCharacter _character;

        public void Initialize(BaseCharacter controlledCharacter)
        {
            _playerControls = new PlayerControls();
            _character = controlledCharacter;
            
            _playerControls.Player.Move.performed += MovePlayer;
        }

        public void Enable()
        {
            _playerControls.Enable();
        }

        public void Disable()
        {
            _playerControls.Disable();
        }

        public void Dispose()
        {
            
        }

        private void MovePlayer(InputAction.CallbackContext context)
        {
            Vector3 direction = context.ReadValue<Vector2>();
            _character.Move(direction);
        }
    }
}