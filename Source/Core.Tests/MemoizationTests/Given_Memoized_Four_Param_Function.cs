using System;
using KoC;
using NUnit.Framework;

namespace Core.Tests.MemoizationTests
{
    [TestFixture]
    public class Given_Memoized_Four_Param_Function
    {
        [Test]
        public void When_Invoked_Twice_With_Same_Param_Then_Returns_Same_Result()
        {
            // Arrange
            var rand = new Random();
            Func<int, int, int, int, string> f = (a, b, c, d) => string.Format("a random result {0}", rand.Next());
            var memoized = f.Memoize();

            // Act
            var r1 = memoized(1, 1, 1, 1);
            var r2 = memoized(1, 1, 1, 1);

            // Assert
            Assert.AreEqual(r1, r2);
        }

        [Test]
        public void When_Invoked_Twice_With_Different_Param_Then_Returns_Different_Result()
        {
            // Arrange
            var rand = new Random();
            Func<int, int, int, int, string> f = (a, b, c, d) => string.Format("a random result {0}", rand.Next());
            var memoized = f.Memoize();

            // Act
            var r1 = memoized(1, 1, 1, 1);
            var r2 = memoized(1, 2, 1, 1);

            // Assert
            Assert.AreNotEqual(r1, r2);
        }
    }
}