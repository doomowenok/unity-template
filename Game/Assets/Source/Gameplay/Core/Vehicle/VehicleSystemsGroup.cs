using Infrastructure.WorldManaging;
using Scellecs.Morpeh;
using VContainer;

namespace Gameplay.Core
{
    public sealed class VehicleSystemsGroup : ISystemsGroup
    {
        private readonly IObjectResolver _container;

        public VehicleSystemsGroup(IObjectResolver container)
        {
            _container = container;
        }
        
        public void Install(WorldData worldData)
        {
            SystemsGroup systemsGroup = worldData.World.CreateSystemsGroup();
            
            systemsGroup.AddInitializer(_container.Resolve<VehicleInitializationSystem>());

            worldData.World.AddSystemsGroup(worldData.SystemsGroupCount, systemsGroup);
        }
    }
}