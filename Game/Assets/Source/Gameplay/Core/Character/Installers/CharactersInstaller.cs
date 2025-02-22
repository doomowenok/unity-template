using VContainer;
using VContainer.Unity;

namespace Gameplay.Core
{
    public sealed class CharactersInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<CharacterFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}