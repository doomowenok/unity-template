using System;
using System.Collections.Generic;
using Infrastructure.Time;
using Scellecs.Morpeh;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.WorldManaging
{
    public sealed class WorldManager : IWorldManager, ITickable
    {
        private readonly ITimeService _time;
        private readonly IObjectResolver _container;

        private readonly IDictionary<WorldType, WorldData> _worlds;

        
        private WorldType _worldTypeInCreation;
        private WorldType _currentWorldType;

        public WorldManager(ITimeService time, IObjectResolver container)
        {
            _time = time;
            _container = container;
            _worlds = new Dictionary<WorldType, WorldData>(Enum.GetValues(typeof(WorldType)).Length);
        }
        
        public IWorldManager CreateWorld(WorldType type, bool updateByUnity)
        {
            _worldTypeInCreation = type;
            World world = World.Create();
            world.UpdateByUnity = updateByUnity;
            
            WorldData data = new WorldData
            {
                Enabled = false,
                World = world,
                SystemsGroupCount = 0,
            };

            _worlds.Add(_worldTypeInCreation, data);

            return this;
        }

        public IWorldManager AddSystemsGroup<TSystemsGroup>() where TSystemsGroup : class, ISystemsGroup
        {
            TSystemsGroup group = _container.Resolve<TSystemsGroup>();
            WorldData worldData = _worlds[_worldTypeInCreation];
            worldData.SystemsGroupCount++;
            group.Install(worldData);
            return this;
        }

        public void Build()
        {
            _worldTypeInCreation = WorldType.None;
        }

        public void SwitchTo(WorldType type)
        {
            if (_currentWorldType != WorldType.None)
            {
                SetCurrentWorldState(false);
            }
            
            _currentWorldType = type;
            SetCurrentWorldState(true);
        }

        void ITickable.Tick()
        {
            if (_worlds.Count == 0)
            {
                return;
            }
            
            foreach (var world in _worlds)
            {
                if (world.Value.Enabled && !world.Value.World.UpdateByUnity)
                {
                    world.Value.World.Update(_time.DeltaTime);
                }
            }
        }

        private void SetCurrentWorldState(bool enabled)
        {
            _worlds[_currentWorldType].Enabled = enabled;
        }
    }
}