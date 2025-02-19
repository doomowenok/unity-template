using Scellecs.Morpeh;

namespace Infrastructure.EcsRunner
{
    public sealed class WorldBuilder : IWorldBuilder
    {
        private readonly ISystemsFactory _systemsFactory;

        public WorldBuilder(ISystemsFactory systemsFactory)
        {
            _systemsFactory = systemsFactory;
        }

        private WorldData _currentWorldData;

        public IWorldBuilder CreateWorld(WorldType type)
        {
            World world = World.Create();
            SystemsGroup group = world.CreateSystemsGroup();
            world.UpdateByUnity = true;
            _currentWorldData = new WorldData(type, world, group);
            return this;
        }

        public IWorldBuilder WithSystem<TSystem>() where TSystem : class, ISystem
        {
            _currentWorldData.Systems.AddSystem(_systemsFactory.CreateSystem<TSystem>());
            return this;
        }

        public WorldData Build()
        {
            _currentWorldData.World.AddSystemsGroup(0, _currentWorldData.Systems);
            return _currentWorldData;
        }
    }
}