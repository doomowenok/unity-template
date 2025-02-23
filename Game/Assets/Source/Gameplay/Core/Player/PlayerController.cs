using System;
using Gameplay.Core.Input;
using Infrastructure.Reactive;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Gameplay.Core
{
    // TODO::Move to something like ITickable for read input every frame instead of just single event.
    public class PlayerController : ITickable
    {
        private PlayerControls _playerControls;
        private BaseCharacter _character;

        private bool _enabled;

        public void Initialize(BaseCharacter controlledCharacter)
        {
            _playerControls = new PlayerControls();
            _character = controlledCharacter;
        }

        public void Enable()
        {
            _playerControls.Enable();
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
            _playerControls.Disable();
        }

        public void Dispose()
        {
            
        }

        void ITickable.Tick()
        {
            if (!_enabled)
            {
                return;
            }
            
            MovePlayer();    
        }

        private void MovePlayer()
        {
            Vector2 input = _playerControls.Player.Move.ReadValue<Vector2>();
            Vector3 direction = new Vector3(input.x, 0, input.y);
            _character.Move(direction);
        }
    }
}