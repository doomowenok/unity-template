using Cysharp.Threading.Tasks;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;

namespace Gameplay.Boot
{
    public sealed class BootState : IState
    {
        private readonly IApplicationStateMachine _stateMachine;

        public BootState(IApplicationStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}