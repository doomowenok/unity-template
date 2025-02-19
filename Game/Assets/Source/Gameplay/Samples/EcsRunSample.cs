using Gameplay.Samples;
using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay
{
    public sealed class EcsRunSample : MonoBehaviour
    {
        private void Awake()
        {
            var world = World.Create();   
            world.UpdateByUnity = true;
            var systems = world.CreateSystemsGroup();
            systems.AddSystem(new PlayerSystem());
            world.AddSystemsGroup(0, systems);
        }
    }
}