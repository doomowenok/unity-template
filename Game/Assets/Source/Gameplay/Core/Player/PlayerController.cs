using Gameplay.Core.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Core
{
    // TODO::Move to something like ITickable for read input every frame instead of just single event.
    public class PlayerController
    {
        private PlayerControls _playerControls;
        private BaseCharacter _character;

        public void Initialize(BaseCharacter controlledCharacter)
        {
            _playerControls = new PlayerControls();
            _character = controlledCharacter;
            
            // BUG::Not working for my purposes.
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
            Debug.Log($"[PLAYER CONTROLLER]::Move performed.");
            Vector2 input = context.ReadValue<Vector2>();
            Vector3 direction = new Vector3(input.x, 0, input.y);
            _character.Move(direction);
        }
    }
}