using VContainer;
using VContainer.Unity;

namespace Gameplay.Core
{
    public sealed class PlayerInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<PlayerController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}