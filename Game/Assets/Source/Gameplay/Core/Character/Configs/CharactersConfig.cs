using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Gameplay.Core
{
    [CreateAssetMenu(fileName = nameof(CharactersConfig), menuName = "Configs/Characters Config")]
    public class CharactersConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<CharacterType, CharacterData> _characters;
        public IReadOnlyDictionary<CharacterType, CharacterData> Characters => _characters;
    }
}