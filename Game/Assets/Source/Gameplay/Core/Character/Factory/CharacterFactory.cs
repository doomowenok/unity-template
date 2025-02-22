using Cysharp.Threading.Tasks;
using Infrastructure.Config;
using Infrastructure.Pool;
using Infrastructure.Resource;
using UnityEngine;

namespace Gameplay.Core
{
    public sealed class CharacterFactory : ICharacterFactory
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
        
        public async UniTask<TCharacter> CreateCharacter<TCharacter>(
            CharacterType type,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null) where TCharacter : BaseCharacter
        {
            CharacterData data = _configProvider.GetConfig<CharactersConfig>().Characters[type];

            TCharacter character = null;
            
            if (!_objectPool.TryRent<TCharacter>(out character))
            {
                TCharacter prefab = await _resourceProvider.Get<TCharacter>(data.PrefabName);
                character = Object.Instantiate(prefab);
            }
            
            character.Initialize(data);
                
            character.transform.position = position;
            character.transform.rotation = rotation;
            character.transform.parent = parent;
                
            character.gameObject.SetActive(true);

            return null;
        } 
    }
}