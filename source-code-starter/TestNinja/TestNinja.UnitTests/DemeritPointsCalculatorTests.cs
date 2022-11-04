using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(-100)]
        [TestCase(301)]
        [TestCase(302)]
        [TestCase(9999999)]
        public void CalculateDemeritPoints_ThrowsArgumentOutOfRangeException(int input)
        {
            var calculator = new DemeritPointsCalculator();
            Assert.That(() => calculator.CalculateDemeritPoints(input), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());

        }

        [Test]
        [TestCase(60, 0)]
        [TestCase(61, 0)]
        [TestCase(62, 0)]
        [TestCase(63, 0)]
        [TestCase(64, 0)]
        [TestCase(65, 0)]
        [TestCase(66, 0)]
        [TestCase(67, 0)]
        [TestCase(68, 0)]
        [TestCase(70, 1)]
        [TestCase(71, 1)]
        [TestCase(72, 1)]
        [TestCase(73, 1)]
        [TestCase(74, 1)]
        [TestCase(75, 2)]
        [TestCase(76, 2)]
        [TestCase(77, 2)]
        [TestCase(78, 2)]
        [TestCase(79, 2)]
        [TestCase(80, 3)]
        public void CalculateDemeritPoints_ReturnsDemeritPoints(int input, int expectedResult)
        {
            var calculator = new DemeritPointsCalculator();
            var result = calculator.CalculateDemeritPoints(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}