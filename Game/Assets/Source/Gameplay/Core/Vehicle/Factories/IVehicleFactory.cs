using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Core
{
    public interface IVehicleFactory
    {
        UniTask<VehicleAuthoring> CreateVehicle(string name, Vector3 at, Quaternion rotateTo, Transform parent = null);
    }
}