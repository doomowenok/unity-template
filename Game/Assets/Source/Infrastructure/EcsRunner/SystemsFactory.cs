using Scellecs.Morpeh;
using VContainer;

namespace Infrastructure.EcsRunner
{
    public sealed class SystemsFactory : ISystemsFactory
    {
        private readonly IObjectResolver _container;

        public SystemsFactory(IObjectResolver container)
        {
            _container = container;
        }

        public TSystem CreateSystem<TSystem>() where TSystem : ISystem
        {
            return _container.Resolve<TSystem>();
        }
    }
}