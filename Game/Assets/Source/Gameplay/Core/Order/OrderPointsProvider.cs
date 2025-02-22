using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Core
{
    public sealed class OrderPointsProvider : MonoBehaviour
    {
        [SerializeField] private List<OrderPoint> _orderPoints;
        
        public IReadOnlyList<OrderPoint> OrderPoints => _orderPoints;
        
        #if UNITY_EDITOR
        [Button]
        private void CollectAllOrderPoints()
        {
            _orderPoints = new List<OrderPoint>(FindObjectsByType<OrderPoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None));
        }
        #endif
    }
}