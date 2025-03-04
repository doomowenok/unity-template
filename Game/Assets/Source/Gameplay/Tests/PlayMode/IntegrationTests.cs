using System.Collections;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;

namespace Gameplay.Tests.PlayMode
{
    public class IntegrationTests
    {
        [UnityTest]
        public IEnumerator When1FramePassed_ThenDeltaTimeChanged()
        {
            // Arrange

            // Act
            yield return null;
            
            // Assert
            Time.deltaTime.Should().BePositive();
        }
    }
}
