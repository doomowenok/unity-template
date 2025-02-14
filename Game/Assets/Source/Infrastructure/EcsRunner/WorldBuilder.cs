using Scellecs.Morpeh;

namespace Infrastructure.EcsRunner
{
    public sealed class WorldBuilder 
    {
        private readonly ISystemsFactory _systemsFactory;

        public WorldBuilder(ISystemsFactory systemsFactory)
        {
            _systemsFactory = systemsFactory;
        }

        private WorldData _currentWorldData;

        public WorldBuilder CreateWorld(WorldType type)
        {
            World world = World.Create();
            SystemsGroup group = world.CreateSystemsGroup();
            world.UpdateByUnity = true;
            _currentWorldData = new WorldData(type, world, group);
            return this;
        }

        public WorldBuilder WithSystem<TSystem>() where TSystem : class, ISystem, new()
        {
            _currentWorldData.Systems.AddSystem(_systemsFactory.CreateSystem<TSystem>());
            return this;
        }

        public WorldData Build()
        {
            return _currentWorldData;
        }
    }
}