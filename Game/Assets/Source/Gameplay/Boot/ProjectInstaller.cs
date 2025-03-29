using VContainer;
using VContainer.Unity;
using Infrastructure.Pool.Installers;
using Infrastructure.Resource.Installers;
using Infrastructure.SceneLoading.Installers;
using Infrastructure.StateMachine.Installers;
using Infrastructure.MVVM.Installers;
using Infrastructure.Config.Installers;
using Gameplay.Core;
using Gameplay.Services.Physics;
using Infrastructure.IdGenerator.Installers;
using Infrastructure.Time.Installers;

namespace Gameplay.Boot
{
    public sealed class ProjectInstaller : LifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            // States
            builder.Register<BootState>(Lifetime.Singleton).AsSelf();

            // Infrastructure
            InstallBindings<StateMachineInstaller>(builder);
            InstallBindings<SceneLoaderInstaller>(builder);
            InstallBindings<ResourcesInstaller>(builder);
            InstallBindings<ObjectPoolInstaller>(builder);
            InstallBindings<MvvmInstaller>(builder);
            InstallBindings<ConfigProviderInstaller>(builder);
            InstallBindings<TimeServiceInstaller>(builder);
            InstallBindings<IdGeneratorInstaller>(builder);

            // Services
            InstallBindings<PhysicsInstaller>(builder);
            
            // Core
            InstallBindings<GameplayInstaller>(builder);
        }

        private static void InstallBindings<TInstaller>(IContainerBuilder builder) where TInstaller : IInstaller,  new() 
        {
            TInstaller installer = new TInstaller();
            installer.Install(builder);
        }
    }
}