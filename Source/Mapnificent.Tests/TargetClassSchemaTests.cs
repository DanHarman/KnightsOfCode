using KoC.Mapnificent.Tests.TestEntities.Inheritance;
using NUnit.Framework;

namespace KoC.Mapnificent.Tests
{
    [TestFixture]
    public class TargetClassSchemaTests
    {
        [Test]
        public void When_Creating_Schema_Then_Schema_Is_Correct()
        {
            // Arrange
            var sut = new TargetClassSchema(typeof(Circle));

            // Assert
            Assert.IsTrue(sut.Members.ContainsKey("Name"));
            Assert.IsTrue(sut.Members.ContainsKey("Radius"));
        }
    }
}
