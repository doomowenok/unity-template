using DG.Tweening;
using Infrastructure.MVVM;
using Infrastructure.Reactive;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace Gameplay.Core
{
    public class InputView : BaseView<InputViewModel>
    {
        [SerializeField] private Image _joystick;
        
        public override void Subscribe()
        {
            ViewModel.AnyInput.OnValueChanged += ControlInputVisibility;
        }

        public override void Unsubscribe()
        {
            ViewModel.AnyInput.OnValueChanged -= ControlInputVisibility;
        }

        private void ControlInputVisibility(bool anyInput)
        {
            float finalAlpha = anyInput ? 1 : 0;
            DOTween.ToAlpha(
                () => _joystick.color,
                (color) => _joystick.color = color,
                finalAlpha,
                0.2f);
        }
    }

    public class InputViewModel : BaseViewModel<JoystickVisibility>
    {
        public INotifyProperty<bool> AnyInput { get; } = new NotifyProperty<bool>();
        
        public override void Subscribe()
        {
            Model.AnyInput.OnValueChanged += NotifyAnyInput;
        }

        public override void Unsubscribe()
        {
            Model.AnyInput.OnValueChanged -= NotifyAnyInput;
        }

        private void NotifyAnyInput(bool anyInput)
        {
            AnyInput.Value = anyInput;
        }
    }

    // TODO::WIP hide joystick when innactive.
    public sealed class JoystickVisibility : ITickable
    {
        private readonly PlayerController _playerController;
        public INotifyProperty<bool> AnyInput { get; } = new NotifyProperty<bool>();

        public JoystickVisibility(PlayerController playerController)
        {
            _playerController = playerController;
        }

        void ITickable.Tick()
        {
            
        }
    }
}