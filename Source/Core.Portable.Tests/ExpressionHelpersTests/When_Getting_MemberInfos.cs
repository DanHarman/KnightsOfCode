using System.Linq;
using KoC.Tests.TestEntities;
using NUnit.Framework;

namespace KoC.Tests.ExpressionHelpersTests
{
    [TestFixture]
    public class When_Getting_MemberInfos
    {
        [Test]
        public void Given_One_Node_Property_Chain_Then_Gets_MemberInfo()
        {
            // Arrange
            var expected = typeof(Inner).GetProperty("Property");

            // Act
            var res = ExpressionHelpers.GetMemberInfos<Inner, int>(x => x.Property);

            // Assert
            Assert.AreEqual(expected, res.Single());
        }

        [Test]
        public void Given_One_Node_Field_Chain_Then_Gets_MemberInfo()
        {
            // Arrange
            var expected = typeof(Inner).GetField("Field");

            // Act
            var res = ExpressionHelpers.GetMemberInfos<Inner, int>(x => x.Field);

            // Assert
            Assert.AreEqual(expected, res.Single());
        }

        [Test]
        public void Given_Two_Node_Property_Chain_Then_Gets_MemberInfos()
        {
            // Arrange
            var expected = new[]
            {
                typeof(Outter).GetProperty("InnerProperty"),
                typeof(Inner).GetProperty("Property")
            };

            // Act
            var res = ExpressionHelpers.GetMemberInfos<Outter, int>(x => x.InnerProperty.Property);

            // Assert
            Assert.AreEqual(expected, res);
        }

        [Test]
        public void Given_Two_Node_Field_Chain_Then_Gets_MemberInfos()
        {
            // Arrange
            var expected = new[]
            {
                typeof(Outter).GetField("InnerField"),
                typeof(Inner).GetField("Field")
            };

            // Act
            var res = ExpressionHelpers.GetMemberInfos<Outter, int>(x => x.InnerField.Field);

            // Assert
            Assert.AreEqual(expected, res);
        }
    }
}