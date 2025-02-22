using Cysharp.Threading.Tasks;

namespace Gameplay.Core
{
    public interface IGameplayFactory
    {
        UniTask CreateGameWorld();
        UniTask DisposeGameWorld();
    }
}