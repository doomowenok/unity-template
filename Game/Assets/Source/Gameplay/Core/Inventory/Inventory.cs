using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Core
{
    public class Inventory
    {
        private readonly Dictionary<OrderType, int> _orders = new Dictionary<OrderType, int>(16);
        
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

            Debug.Log($"[INVENTORY]::Add order {order} with amount {amount}. Current amount is {_orders[order]}.");
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

        private bool IsFull()
        {
            return false;
        }
    }
}