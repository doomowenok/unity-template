using UnityEngine;
using UnityEngine.UI;

namespace Source.Gameplay.Core
{
    [CreateAssetMenu(fileName = nameof(WatchThroughObjectsConfig), menuName = "Configs/Common/Watch Through Objects")]
    public sealed class WatchThroughObjectsConfig : ScriptableObject
    {
        [SerializeField] private Material _watchThroughMaterial;
        public Material WatchThroughMaterial => _watchThroughMaterial;

        [SerializeField] private LayerMask _watchThroughLayerMask;

        public LayerMask WatchThroughLayerMask => _watchThroughLayerMask;

        [SerializeField] private float _raycastMaxDistance = 3000.0f;
        public float RaycastMaxDistance => _raycastMaxDistance;

    }
}