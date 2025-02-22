using VContainer;
using VContainer.Unity;

namespace Gameplay.Core
{
    public sealed class GameplayInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<GameplayState>(Lifetime.Singleton).AsSelf();
            builder.Register<GameplayFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}