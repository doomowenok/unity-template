using UnityEngine;
using VContainer;

namespace Gameplay.Core
{
    public sealed class PlayerCharacter : BaseCharacter
    {
        private float _timeForOderAction;
        private float _timeAtPoint;

        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }

        public override void Initialize(in CharacterData data)
        {
            base.Initialize(data);
            
            ResetTimeAtPoint();
            
            _timeForOderAction = data.TimeForOrderAction;
        }

        private void OnTriggerStay(Collider other)
        {
            if (ObjectCollisionController.Is<OrderPoint>(other.gameObject.GetInstanceID()))
            {
                _timeAtPoint += Time.DeltaTime;

                if (_timeAtPoint >= _timeForOderAction)
                {
                    OrderPoint orderPoint = ObjectCollisionController.Get<OrderPoint>(other.gameObject.GetInstanceID());
                    Debug.Log($"[PLAYER]::Perform action with order.");
                    ResetTimeAtPoint();
                    _inventory.AddOrder(orderPoint.Order, 1);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (ObjectCollisionController.Is<OrderPoint>(other.gameObject.GetInstanceID()))
            {
                Debug.Log($"[PLAYER]::Exit order point.");
                ResetTimeAtPoint();
            }
        }

        private void ResetTimeAtPoint()
        {
            _timeAtPoint = 0.0f;
        }
    }
}