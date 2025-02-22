using UnityEngine;

namespace Gameplay.Core
{
    [RequireComponent(typeof(Collider))]
    public class OrderPoint : MonoBehaviour 
    {
        public OrderActionType Action { get; private set; }
        public OrderType Order { get; private set; }

        public void Initialize(OrderActionType action, OrderType order)
        {
            Action = action;
            Order = order;
        }
    }
}