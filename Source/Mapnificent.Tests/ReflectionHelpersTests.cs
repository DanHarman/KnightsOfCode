//using KoC.Mapnificent.Tests.TestEntities.Inheritance;
//using NUnit.Framework;

//namespace KoC.Mapnificent.Tests
//{
//    [TestFixture]
//    public class ReflectionHelpersTests
//    {
//        [Test]
//        public void Given_WeakPropertyGetter_When_Getting_Property_Then_Suceeds()
//        {
//            // Arrange
//            var propertyInfo = typeof(Shape).GetProperty("Name");
//            var sut = ReflectionHelpers.CreateWeakPropertyGetter(propertyInfo);
//            const string expectedName = "Cube";
//            var shape = new Shape() {Name = expectedName};

//            // Act
//            var res = sut(shape);

//            // Assert
//            Assert.AreEqual(expectedName, res);
//        }

//        [Test]
//        public void Given_WeakPropertySetter_When_Setting_Property_Then_Suceeds()
//        {
//            // Arrange
//            var propertyInfo = typeof(Shape).GetProperty("Name");
//            var sut = ReflectionHelpers.CreateWeakPropertySetter(propertyInfo);
//            const string expectedName = "Cube";
//            var shape = new Shape();

//            // Act
//            sut(shape, expectedName);

//            // Assert
//            Assert.AreEqual(expectedName, shape.Name);
//        }

//        [Test]
//        public void Given_WeakFieldGetter_When_Getting_Field_Then_Suceeds()
//        {
//            // Arrange
//            var fieldInfo = typeof(Shape).GetField("Id");
//            var sut = ReflectionHelpers.CreateWeakFieldGetter(fieldInfo);
//            const int expectedId = 777;
//            var shape = new Shape() {Id = expectedId};

//            // Act
//            var res = sut(shape);

//            // Assert
//            Assert.AreEqual(expectedId, res);
//        }

//        [Test]
//        public void Given_WeakFieldSetter_When_Setting_Field_Then_Suceeds()
//        {
//            // Arrange
//            var fieldInfo = typeof(Shape).GetField("Id");
//            var sut = ReflectionHelpers.CreateWeakFieldSetter(fieldInfo);
//            const int expectedId = 777;
//            var shape = new Shape();

//            // Act
//            sut(shape, expectedId);

//            // Assert
//            Assert.AreEqual(expectedId, shape.Id);
//        }
//    }
//}