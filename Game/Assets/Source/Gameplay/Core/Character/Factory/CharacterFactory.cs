using Cysharp.Threading.Tasks;
using Infrastructure.Config;
using Infrastructure.Pool;
using Infrastructure.Resource;
using UnityEngine;

namespace Gameplay.Core.Factory
{
    public sealed class CharacterFactory 
    {
        private readonly IResourceProvider _resourceProvider;
        private readonly IConfigProvider _configProvider;
        private readonly IObjectPool _objectPool;

        public CharacterFactory(
            IResourceProvider resourceProvider,
            IConfigProvider configProvider,
            IObjectPool objectPool)
        {
            _resourceProvider = resourceProvider;
            _configProvider = configProvider;
            _objectPool = objectPool;
        }
        
        // TODO::WIP!
        public async UniTask<TCharacter> CreateCharacter<TCharacter>(
            CharacterType type,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null) where TCharacter : BaseCharacter
        {
            CharactersConfig config = _configProvider.GetConfig<CharactersConfig>();
            
            if (_objectPool.TryRent<TCharacter>(out TCharacter character))
            {
                character.Initialize(config.Characters[type]);
                
                character.transform.position = position;
                character.transform.rotation = rotation;
                character.transform.parent = parent;
                
                character.gameObject.SetActive(true);
                
                return character;
            }

            return null;
        } 
    }
}