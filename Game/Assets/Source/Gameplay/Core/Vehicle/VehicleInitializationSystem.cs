using Cysharp.Threading.Tasks;
using Infrastructure.Resource;
using Scellecs.Morpeh;

namespace Gameplay.Core
{
    public sealed class VehicleInitializationSystem : IInitializer
    {
        private readonly IResourceProvider _resourceProvider;
        
        private Stash<Vehicle> _vehiclesStash;
        private Stash<Wheels> _wheelsStash;
        private Stash<Engine> _enginesStash;
        private Stash<GasPedal> _gasPedalsStash;
        private Stash<BrakePedal> _brakePedalsStash;
        
        public World World { get; set; }

        public VehicleInitializationSystem(IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider;
        }

        public void OnAwake()
        {
            _vehiclesStash = World.GetStash<Vehicle>();
            _wheelsStash = World.GetStash<Wheels>();
            _enginesStash = World.GetStash<Engine>();
            _gasPedalsStash = World.GetStash<GasPedal>();
            _brakePedalsStash = World.GetStash<BrakePedal>();
            
            _resourceProvider
                .Get<VehicleAuthoring>("")
                .ContinueWith(InitializeVehicle);
        }

        public void Dispose()
        {
            
        }

        private void InitializeVehicle(VehicleAuthoring vehicleAuthoring)
        {
            Entity entity = World.CreateEntity();
            int id = 155;

            _vehiclesStash.Add(entity, new Vehicle()
            {
                Id = id
            });
            _wheelsStash.Add(entity, new Wheels()
            {
                Id = id,
                WheelCollider = vehicleAuthoring.Wheels
            });
            _enginesStash.Add(entity, new Engine()
            {
                Id = id,
                AccelerationCurve = vehicleAuthoring.Configuration.AccelerationCurve,
                CurrentPower = 0.0f,
                MaxPower = vehicleAuthoring.Configuration.MaxPower,
            });
            _gasPedalsStash.Add(entity, new GasPedal()
            {
                IsActive = false,
            });
            _brakePedalsStash.Add(entity, new BrakePedal()
            {
                IsActive = false,
            });
        }
    }
}