using KoC.Tests.TestEntities;
using NUnit.Framework;

namespace KoC.Tests.ReflectionHelpersTests
{
    [TestFixture]
    public class Given_WeakSetter
    {
        [Test]
        public void Given_WeakPropertySetter_When_Setting_Property_Then_Suceeds()
        {
            // Arrange
            var propertyInfo = typeof(Shape).GetProperty("Name");
            var sut = ReflectionHelpers.CreateWeakPropertySetter(propertyInfo);
            const string expectedName = "Cube";
            var shape = new Shape();

            // Act
            sut(shape, expectedName);

            // Assert
            Assert.AreEqual(expectedName, shape.Name);
        }

        [Test]
        public void Given_WeakFieldSetter_When_Setting_Field_Then_Suceeds()
        {
            // Arrange
            var fieldInfo = typeof(Shape).GetField("Id");
            var sut = ReflectionHelpers.CreateWeakFieldSetter(fieldInfo);
            const int expectedId = 777;
            var shape = new Shape();

            // Act
            sut(shape, expectedId);

            // Assert
            Assert.AreEqual(expectedId, shape.Id);
        }

        [Test]
        public void Given_WeakMemberSetter_When_Setting_Property_Then_Suceeds()
        {
            // Arrange
            var propertyInfo = typeof(Shape).GetProperty("Name");
            var sut = ReflectionHelpers.CreateWeakMemberSetter(propertyInfo);
            const string expectedName = "Cube";
            var shape = new Shape();

            // Act
            sut(shape, expectedName);

            // Assert
            Assert.AreEqual(expectedName, shape.Name);
        }

        [Test]
        public void Given_WeakMemberSetter_When_Setting_Field_Then_Suceeds()
        {
            // Arrange
            var fieldInfo = typeof(Shape).GetField("Id");
            var sut = ReflectionHelpers.CreateWeakMemberSetter(fieldInfo);
            const int expectedId = 777;
            var shape = new Shape();

            // Act
            sut(shape, expectedId);

            // Assert
            Assert.AreEqual(expectedId, shape.Id);
        }
    }
}