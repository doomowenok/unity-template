using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.Core
{
    public struct Vehicle : IComponent
    {
        public int Id;
    }

    public class VehicleAuthoring : MonoBehaviour
    {
        public CarConfiguration Configuration;
        public List<WheelCollider> Wheels;
    }

    public struct Wheels : IComponent
    {
        public int Id;
        public List<WheelCollider> WheelCollider;
    }

    public struct Engine : IComponent
    {
        public int Id;
        public AnimationCurve AccelerationCurve;
        public float MaxPower;
        public float CurrentPower;
    }

    public struct GasPedal : IComponent
    {
        public bool IsActive;
    }

    public struct BrakePedal : IComponent
    {
        public bool IsActive;
    }
}