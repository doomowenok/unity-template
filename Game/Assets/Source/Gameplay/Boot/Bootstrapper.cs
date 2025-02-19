using Cysharp.Threading.Tasks;
using Gameplay.Core;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.Factory;
using UnityEngine;
using VContainer;

namespace Gameplay.Boot
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        private IApplicationStateMachine _stateMachine;
        private IStateFactory _stateFactory;

        [Inject]
        private void Constuct(IApplicationStateMachine stateMachine, IStateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;
        }

        private void Start()
        {
            _stateMachine.AddState(_stateFactory.CreateState<BootState>());
            _stateMachine.AddState(_stateFactory.CreateState<GameplayState>());

            _stateMachine.Enter<BootState>().Forget();
        }
    }
}