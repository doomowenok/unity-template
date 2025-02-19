using VContainer;
using VContainer.Unity;

namespace Gameplay.Core
{
    public sealed class MoveInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<MovingSystem>(Lifetime.Singleton).AsSelf();
        }
    }
}