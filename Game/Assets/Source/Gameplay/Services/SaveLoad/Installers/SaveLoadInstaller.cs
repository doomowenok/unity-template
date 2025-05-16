﻿using VContainer;
using VContainer.Unity;

namespace Gameplay.Services.SaveLoad
{
    public sealed class SaveLoadInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<SaveLoadService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerPrefsSavingService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}