using VContainer;
using VContainer.Unity;

namespace Gameplay.Core.Installers
{
    public sealed class VehicleInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<VehicleFactory>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<VehicleInitializationSystem>(Lifetime.Transient).AsSelf();
            
            builder.Register<VehicleSystemsGroup>(Lifetime.Singleton).AsSelf();
        }
    }
}