using VContainer;
using VContainer.Unity;

namespace Gameplay.Core.Input.Installers
{
    public sealed class InputInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<InputController>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<InputSystem>(Lifetime.Singleton).AsSelf();
        }
    }
}