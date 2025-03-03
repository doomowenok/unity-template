using Scellecs.Morpeh;

namespace Infrastructure.WorldManaging
{
    public interface IWorldManager
    {
        IWorldManager CreateWorld(WorldType type, bool updateByUnity);
        IWorldManager AddSystem<TSystem>() where TSystem : class, ISystem, new();
        void Build();
    }
}