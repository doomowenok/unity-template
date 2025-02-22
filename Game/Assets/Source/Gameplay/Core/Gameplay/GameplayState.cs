using Cysharp.Threading.Tasks;
using Infrastructure.SceneLoading;
using Infrastructure.StateMachine.States;
using Infrastructure.Time;

namespace Gameplay.Core
{
    public sealed class GameplayState : IPayloadState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameplayFactory _gameplayFactory;
        private readonly ITimeService _timeService;

        public GameplayState(
            ISceneLoader sceneLoader,
            IGameplayFactory gameplayFactory,
            ITimeService timeService)
        {
            _sceneLoader = sceneLoader;
            _gameplayFactory = gameplayFactory;
            _timeService = timeService;
        }
        
        public async UniTask Enter(string sceneName)
        {
            _timeService.SetTimeScale(1.0f);
            await _sceneLoader.LoadSceneAsync(sceneName);
            await CreateGameWorld();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
        
        private async UniTask CreateGameWorld()
        {
            await _gameplayFactory.CreateGameWorld();
        }
    }
}