using System.Linq;
using KoC.Tests.TestEntities;
using NUnit.Framework;

namespace KoC.Tests.ReflectionHelpersTests
{
    [TestFixture]
    public class Given_SafeWeakGetter
    {
        [Test]
        public void Given_One_Node_Property_Chain_Then_Gets_Member()
        {
            // Arrange
            var memberInfos = ExpressionHelpers.GetMemberInfos<Inner, int>(x => x.Property);
            var sut = ReflectionHelpers.CreateSafeWeakMemberChainGetter(memberInfos);
            var instance = new Inner() { Property = 1234 };

            // Act
            var res = sut(instance);

            // Assert
            Assert.IsTrue(res.HasResult);
            Assert.AreEqual(instance.Property, res.Result);
        }

        [Test]
        public void Given_One_Node_Field_Chain_Then_Gets_MemberInfo()
        {
            // Arrange
            var memberInfos = ExpressionHelpers.GetMemberInfos<Inner, int>(x => x.Field);
            var sut = ReflectionHelpers.CreateSafeWeakMemberChainGetter(memberInfos);
            var instance = new Inner() { Field = 1234 };

            // Act
            var res = sut(instance);

            // Assert
            Assert.IsTrue(res.HasResult);
            Assert.AreEqual(instance.Field, res.Result);
        }

        [Test]
        public void Given_Two_Node_Property_Chain_Then_Gets_MemberInfos()
        {
            // Arrange
            var memberInfos = ExpressionHelpers.GetMemberInfos<Outter, int>(x => x.InnerProperty.Property);
            var sut = ReflectionHelpers.CreateSafeWeakMemberChainGetter(memberInfos);
            var instance = new Outter { InnerProperty = new Inner { Property = 1234 }};

            // Act
            var res = sut(instance);

            // Assert
            Assert.IsTrue(res.HasResult);
            Assert.AreEqual(instance.InnerProperty.Property, res.Result);
        }

        [Test]
        public void Given_Two_Node_Field_Chain_Then_Gets_MemberInfos()
        {
            // Arrange
            var memberInfos = ExpressionHelpers.GetMemberInfos<Outter, int>(x => x.InnerField.Field);
            var sut = ReflectionHelpers.CreateSafeWeakMemberChainGetter(memberInfos);
            var instance = new Outter { InnerField = new Inner { Field = 1234 } };

            // Act
            var res = sut(instance);

            // Assert
            Assert.IsTrue(res.HasResult);
            Assert.AreEqual(instance.InnerField.Field, res.Result);
        }

        [Test]
        public void Given_Two_Node_Property_Chain_With_Null_Link_Then_HasResult_False()
        {
            // Arrange
            var memberInfos = ExpressionHelpers.GetMemberInfos<Outter, int>(x => x.InnerProperty.Property);
            var sut = ReflectionHelpers.CreateSafeWeakMemberChainGetter(memberInfos);
            var instance = new Outter { InnerProperty = null };

            // Act
            var res = sut(instance);

            // Assert
            Assert.IsFalse(res.HasResult);
        }

        [Test]
        public void Given_Two_Node_Field_Chain_With_Null_Link_Then_HasResult_False()
        {
            // Arrange
            var memberInfos = ExpressionHelpers.GetMemberInfos<Outter, int>(x => x.InnerField.Field);
            var sut = ReflectionHelpers.CreateSafeWeakMemberChainGetter(memberInfos);
            var instance = new Outter { InnerField = null };

            // Act
            var res = sut(instance);

            // Assert
            Assert.IsFalse(res.HasResult);
        }
    }
}