using System;
using System.Collections.Generic;
using Infrastructure.Time;
using Scellecs.Morpeh;
using VContainer.Unity;

namespace Infrastructure.WorldManaging
{
    public sealed class WorldManager : IWorldManager, ITickable
    {
        private readonly ITimeService _time;
        
        private readonly IDictionary<WorldType, World> _worlds;

        private World _worldInCreation;
        private SystemsGroup _groupInCreation;
        private WorldType _worldTypeInCreation;

        public WorldManager(ITimeService time)
        {
            _time = time;
            _worlds = new Dictionary<WorldType, World>(Enum.GetValues(typeof(WorldType)).Length);
        }
        
        public IWorldManager CreateWorld(WorldType type, bool updateByUnity)
        {
            _worldTypeInCreation = type;
            _worldInCreation = World.Create();
            _worldInCreation.UpdateByUnity = updateByUnity;
            
            _groupInCreation = _worldInCreation.CreateSystemsGroup();
            _worldInCreation.AddSystemsGroup( 0, _groupInCreation);
            
            return this;
        }
        
        public IWorldManager AddSystem<TSystem>() where TSystem : class, ISystem, new()
        {
            TSystem system = new TSystem();
            _groupInCreation.AddSystem(system);
            return this;
        }

        public void Build()
        {
            _worlds.Add(_worldTypeInCreation, _worldInCreation);
            
            _worldInCreation = null;
            _groupInCreation = null;
            _worldTypeInCreation = default;
        }

        public void SwitchTo(WorldType type)
        {
            // _worlds[type].CanUpdate = true;
        }

        void ITickable.Tick()
        {
            if (_worlds.Count == 0)
            {
                return;
            }
            
            foreach (var world in _worlds)
            {
                if (!world.Value.UpdateByUnity)
                {
                    world.Value.Update(_time.DeltaTime);
                }
            }
        }
    }
}