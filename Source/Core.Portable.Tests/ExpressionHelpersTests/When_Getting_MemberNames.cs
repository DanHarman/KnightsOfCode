using KoC.Tests.TestEntities;
using NUnit.Framework;

namespace KoC.Tests.ExpressionHelpersTests
{
    [TestFixture]
    public class When_Getting_MemberNames
    {
        [Test]
        public void Given_PropertyChain_Then_Gets_Expected()
        {
            // Arrange
            var expected = new[] {"InnerProperty", "Property"};

            // Act
            var res = ExpressionHelpers.GetMemberNames<Outter, int>(x => x.InnerProperty.Property);

            // Assert
            Assert.AreEqual(expected, res);
        }

        [Test]
        public void Given_PropertyChain_With_Function_Then_Throws()
        {
            // Act
            var res = ExpressionHelpers.GetMemberNames<Outter, string>(x => x.InnerProperty.GetName());

            // Assert
            // Assert.IsTrue(res);
        }
    }
}