using VContainer;
using VContainer.Unity;

namespace Gameplay.Core
{
    public sealed class PlayerInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<PlayerCreationSystem>(Lifetime.Singleton).AsSelf();
            builder.Register<PlayerMoveDirectionSystem>(Lifetime.Singleton).AsSelf();
            builder.Register<PlayerThroughObjectsSystem>(Lifetime.Singleton).AsSelf();
        }
    }
}