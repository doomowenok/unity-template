using Cysharp.Threading.Tasks;
using Infrastructure.Resource;
using UnityEngine;

namespace Gameplay.Core
{
    public sealed class VehicleFactory : IVehicleFactory
    {
        private readonly IResourceProvider _resourceProvider;

        public VehicleFactory(IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider;
        }
        
        public async UniTask<VehicleAuthoring> CreateVehicle(string name, Vector3 at, Quaternion rotateTo, Transform parent = null)
        {
            VehicleAuthoring vehicleAuthoringPrefab = await _resourceProvider.Get<VehicleAuthoring>(name);
            VehicleAuthoring vehicleAuthoring = Object.Instantiate(vehicleAuthoringPrefab);
            return vehicleAuthoring;
        }
    }
}