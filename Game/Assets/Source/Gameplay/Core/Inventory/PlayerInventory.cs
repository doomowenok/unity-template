using System.Collections.Generic;

namespace Gameplay.Core
{
    public class PlayerInventory : IInventory
    {
        private readonly Dictionary<OrderType, int> _orders = new Dictionary<OrderType, int>(16);
        public int AllOrdersAmount { get; private set; }

        public void AddOrder(OrderType order, int amount)
        {
            if (IsFull())
            {
                return;
            }


            if (_orders.ContainsKey(order))
            {
                _orders[order] += amount;
            }
            else
            {
                _orders.Add(order, amount);
            }
            
            AllOrdersAmount += amount;
        }

        public void RemoveOrder(OrderType order, int amount)
        {
            if (_orders.ContainsKey(order))
            {
                _orders[order] -= amount;

                if (_orders[order] <= 0)
                {
                    _orders.Remove(order);
                }
            }
        }

        public bool IsFull()
        {
            return false;
        }
    }
}