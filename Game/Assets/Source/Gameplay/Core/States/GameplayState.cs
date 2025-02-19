using System;
using Cysharp.Threading.Tasks;
using Infrastructure.EcsRunner;
using Infrastructure.SceneLoading;
using Infrastructure.StateMachine.States;

namespace Gameplay.Core
{
    public sealed class GameplayState : IPayloadState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IWorldBuilder _worldBuilder;

        private WorldData _world;

        public GameplayState(ISceneLoader sceneLoader, IWorldBuilder worldBuilder)
        {
            _sceneLoader = sceneLoader;
            _worldBuilder = worldBuilder;
        }
        
        public async UniTask Enter(string sceneName)
        {
             await _sceneLoader.LoadSceneAsync(sceneName, onComplete: () => CreateGameWorld());
        }

        public UniTask Exit()
        {
            _world.World.Dispose();
            return UniTask.CompletedTask;
        }
        
        private void CreateGameWorld()
        {
            _world = _worldBuilder
                        .CreateWorld(WorldType.Gameplay)
                        .WithSystem<PlayerCreationSystem>()
                        .WithSystem<InputSystem>()
                        .WithSystem<PlayerMoveDirectionSystem>()
                        .WithSystem<MovingSystem>()
                        .Build();
        }
    }
}