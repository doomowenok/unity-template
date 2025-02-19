using System;
using UnityEngine;

namespace Gameplay.Core
{
    public interface IInputController
    {
        void Initialize();
        Vector2 GetAxisInput();
        void Enable();
        void Disable();
        void Dispose();
    }
}