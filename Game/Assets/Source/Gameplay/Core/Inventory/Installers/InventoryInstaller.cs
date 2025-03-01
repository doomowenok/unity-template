using VContainer;
using VContainer.Unity;

namespace Gameplay.Core
{
    public sealed class InventoryInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<PlayerInventory>(Lifetime.Transient).AsSelf();
        }
    }
}