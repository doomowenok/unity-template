using Cysharp.Threading.Tasks;
using Gameplay.Services.Physics;
using UnityEngine;

namespace Gameplay.Core
{
    public class GameplayFactory : IGameplayFactory
    {
        private readonly ICharacterFactory _characterFactory;
        private readonly PlayerController _playerController;
        private readonly IObjectCollisionController _objectCollisionController;

        public GameplayFactory(
            ICharacterFactory characterFactory,
            PlayerController playerController,
            IObjectCollisionController objectCollisionController)
        {
            _characterFactory = characterFactory;
            _playerController = playerController;
            _objectCollisionController = objectCollisionController;
        }
        
        public async UniTask CreateGameWorld()
        {
            InitializeOrderPoints();
            await CreatePlayer();
        }

        public UniTask DisposeGameWorld()
        {
            return UniTask.CompletedTask;
        }

        private void InitializeOrderPoints()
        {
            OrderPointsProvider orderPointsProvider = UnityEngine.Object.FindFirstObjectByType<OrderPointsProvider>();

            foreach (OrderPoint orderPoint in orderPointsProvider.OrderPoints)
            {
                _objectCollisionController.Register(orderPoint);
            }
        }

        private async UniTask CreatePlayer()
        {
            PlayerCharacter player = await _characterFactory.CreateCharacter<PlayerCharacter>(
                CharacterType.Player,
                Vector3.zero,
                Quaternion.identity);
            _objectCollisionController.Register(player);
            _playerController.Initialize(player);
            _playerController.Enable();
        }
    }
}