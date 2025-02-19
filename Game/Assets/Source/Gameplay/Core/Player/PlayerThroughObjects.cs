using UnityEngine;

namespace Gameplay.Core
{
    public sealed class PlayerThroughObjects : MonoBehaviour
    {
        private static readonly int PositionId = Shader.PropertyToID("_PlayerPosition");
        private static readonly int SizeId = Shader.PropertyToID("_Size");

        public Material WatchThroughMaterial;
        public Camera Camera;
        public LayerMask Mask;

        private void Update()
        {
            Vector3 direction = Camera.transform.position - transform.position;
            Ray ray = new Ray(transform.position, direction.normalized);

            if(Physics.Raycast(ray, 3000, Mask))
            {
                WatchThroughMaterial.SetFloat(SizeId, 1.0f);
            }
            else
            {
                WatchThroughMaterial.SetFloat(SizeId, 0.0f);
            }

            Vector3 view = Camera.WorldToViewportPoint(transform.position);
            WatchThroughMaterial.SetVector(PositionId, view);
        }
    }
}