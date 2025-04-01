using UnityEngine;

namespace Gameplay.Core
{
    [CreateAssetMenu(fileName = nameof(CarConfiguration), menuName = "Configs/Core/Car", order = 0)]
    public sealed class CarConfiguration : ScriptableObject
    {
        public string CarName;
        public CarDriveType DriveType;
        public AnimationCurve AccelerationCurve;
        public float MaxPower;
    }
}