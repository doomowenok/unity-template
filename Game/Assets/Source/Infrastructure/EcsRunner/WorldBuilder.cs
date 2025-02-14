using Scellecs.Morpeh;

namespace Infrastructure.EcsRunner
{
    // TODO::Use container for resolving systems.
    public sealed class WorldBuilder 
    {
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
            // TODO::Add resolved systems from container.
            _currentWorldData.Systems.AddSystem<TSystem>(new TSystem());
            return this;
        }

        public WorldData Build()
        {
            return _currentWorldData;
        }
    }
}