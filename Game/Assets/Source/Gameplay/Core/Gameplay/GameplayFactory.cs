using Cysharp.Threading.Tasks;
using Gameplay.Services.Physics;
using UnityEngine;

namespace Gameplay.Core
{
    public class GameplayFactory : IGameplayFactory
    {
        public GameplayFactory()
        {
        }
        
        public async UniTask CreateGameWorld()
        {
            
        }

        public UniTask DisposeGameWorld()
        {
            return UniTask.CompletedTask;
        }
    }
}