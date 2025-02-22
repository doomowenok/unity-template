using System;
using Sirenix.OdinInspector;

namespace Gameplay.Core
{
    [Serializable]
    public struct CharacterData
    {
        private bool IsPlayer => CharacterType == CharacterType.Player;
        
        public CharacterType CharacterType;
        public string PrefabName;
        public float MoveSpeed;

        [ShowIf(nameof(IsPlayer))] 
        public float TimeForOrderAction;
    }
}