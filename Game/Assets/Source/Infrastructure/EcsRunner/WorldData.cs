using Scellecs.Morpeh;

namespace Infrastructure.EcsRunner
{
    public sealed class WorldData
    {
        public readonly World World;
        public readonly SystemsGroup Systems;
        public readonly WorldType Type;

        public WorldData(WorldType type, World world, SystemsGroup systems)
        {
            Type = type;
            World = world;
            Systems = systems;
        }
    }
}