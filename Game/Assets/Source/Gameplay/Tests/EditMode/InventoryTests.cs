using FluentAssertions;
using Gameplay.Core;
using Moq;
using NSubstitute;
using NUnit.Framework;

namespace Gameplay.Tests.EditMode
{
    public class InventoryTests
    {
        [Test]
        public void WhenAddItemToInventory_ThenInventoryShouldHaveItem()
        {
            // Arrange
            PlayerInventory playerInventory = new PlayerInventory();
            int amount = 5;
            OrderType order = OrderType.None;
            
            // Act
            playerInventory.AddOrder(order, amount);
            
            // Assert
            playerInventory.AllOrdersAmount.Should().Be(amount);
        }
        
        [Test]
        public void WhenAddItemToInventory_ThenInventoryShouldHaveItem_WithMoq()
        {
            // Arrange
            int maxItemsInInventory = 3;
            int amount = 5;
            IInventory playerInventory = Mock.Of<IInventory>(x => x.IsFull() == maxItemsInInventory < amount);
            OrderType order = OrderType.None;
            
            // Act
            playerInventory.AddOrder(order, amount);
            
            // Assert
            playerInventory.IsFull().Should().Be(true);
        }
        
        [Test]
        public void WhenAddItemToInventory_ThenInventoryShouldHaveItem_WithNSubtitude()
        {
            // Arrange
            int maxItemsInInventory = 3;
            int amount = 5;
            OrderType order = OrderType.None;
            
            IInventory playerInventory = Substitute.For<IInventory>();
            playerInventory.IsFull().Returns(maxItemsInInventory < amount);
            
            // Act
            playerInventory.AddOrder(order, amount);
            
            // Assert
            playerInventory.IsFull().Should().Be(true);
        }
    }
}