namespace Gameplay.Core
{
    public interface IInventory
    {
        int AllOrdersAmount { get; }
        void AddOrder(OrderType order, int amount);
        void RemoveOrder(OrderType order, int amount);
        bool IsFull();
    }
}