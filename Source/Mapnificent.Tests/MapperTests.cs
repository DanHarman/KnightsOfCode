using System;
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
            var from = new CircleDto {Name = "Circle", Radius = 15.0};

            // When Mapping
            var result = sut.Map<Circle>(from);

            // Assert
        }
    }
}
