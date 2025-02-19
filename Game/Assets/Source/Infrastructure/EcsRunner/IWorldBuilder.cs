using Scellecs.Morpeh;

namespace Infrastructure.EcsRunner
{
    public interface IWorldBuilder
    {
        IWorldBuilder CreateWorld(WorldType type);
        IWorldBuilder WithSystem<TSystem>() where TSystem : class, ISystem;
        WorldData Build();
    }
}