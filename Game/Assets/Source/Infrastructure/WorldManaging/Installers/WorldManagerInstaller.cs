using VContainer;
using VContainer.Unity;

namespace Infrastructure.WorldManaging.Installers
{
    public sealed class WorldManagerInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<WorldManager>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}