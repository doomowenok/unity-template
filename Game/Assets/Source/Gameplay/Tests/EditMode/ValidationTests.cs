using NUnit.Framework;
using Tools;

namespace Gameplay.Tests.EditMode
{
    public class ValidationTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ValidationTestsSimplePasses()
        {
            // Use the Assert class to test conditions
            Validator.FindMissingComponents();
        }
    }
}
