using Scellecs.Morpeh;

namespace Infrastructure.EcsRunner
{
    public interface ISystemsFactory
    {
        TSystem CreateSystem<TSystem>() where TSystem : ISystem;
    }
}