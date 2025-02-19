using VContainer;
using VContainer.Unity;

namespace Infrastructure.EcsRunner.Installers
{
    public sealed class EcsRunnerInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<WorldBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SystemsFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}