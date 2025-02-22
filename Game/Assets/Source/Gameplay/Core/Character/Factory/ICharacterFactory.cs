using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Core
{
    public interface ICharacterFactory
    {
        UniTask<TCharacter> CreateCharacter<TCharacter>(
            CharacterType type,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null) where TCharacter : BaseCharacter;
    }
}