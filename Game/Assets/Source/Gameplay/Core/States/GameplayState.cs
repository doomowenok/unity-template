using Cysharp.Threading.Tasks;
using Infrastructure.SceneLoading;
using Infrastructure.StateMachine.States;

namespace Gameplay.Core
{
    public sealed class GameplayState : IPayloadState<string>
    {
        private readonly ISceneLoader _sceneLoader;

        public GameplayState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public async UniTask Enter(string sceneName)
        {
             await _sceneLoader.LoadSceneAsync(sceneName, onComplete: () => CreateGameWorld());
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
        
        private void CreateGameWorld()
        {
            
        }
    }
}