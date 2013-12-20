using System;
using System.Reflection.Emit;
using KoC.Mapnificent.Tests.TestEntities.Inheritance;
using NUnit.Framework;

namespace KoC.Mapnificent.Tests
{
    [TestFixture]
    public class MapperTests
    {
        public void When_Mapping_Undefined_Class_Then_Throws()
        {
            var sut = new Mapper();

            Assert.Throws<MappingException>(() =>sut.Map<Circle>(new Object()));
        }

        [Test]
        public void Mapping()
        {
            // Given CircleDto
            var sut = new Mapper();
            sut.Define<CircleDto, Circle>()
                .For(to => to.Name, opt => opt.From(from => from.Name));
          //  sut.Define<CircleDto, Circle>(opt => opt.For(to => to.Name, opt2 => opt2.From(from => from.Name)));

            var dto = new CircleDto {Name = "Circle", Radius = 15.0};

            // When Mapping
            var result = sut.Map<Circle>(dto);

            // Assert
        }

        [Test]
        public void Given_Default_Mapping_When_Mapped_Then()
        {
            // Arrange
            var sut = new Mapper();
            sut.Define<ShapeDto, Shape>();
            sut.Define<CircleDto, Circle>();
              //  .Inherits<ShapeDto, Shape>();
            sut.Define<RectangleDto, Rectangle>();

//            var props = ReflectionHelpers.GetProperties(typeof(Rectangle));
    //       var props2 = ReflectionHelpers.GetProperties(typeof(Shape));

            // Act

            // Assert
        }
    }
}
