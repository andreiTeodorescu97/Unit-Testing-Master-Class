using NUnit.Framework;
using System.Linq;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        //SetUp - called before each test, it is used for initialization
        //TearDown - called after each test, used in integration tests to clean up the database
        private Math _math;

        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            //act
            var result = _math.Add(1, 2);

            //assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(2, 2, 2)]
        public void Max_WhenCalled_ReturnsTheGreaterValue(int a, int b, int expectedResult)
        {
            //act
            var result = _math.Max(a, b);

            //assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }


        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            //act
            var result = _math.GetOddNumbers(5);

            //assert
            //Assert.That(result, Is.Not.Empty);

            ////Too Specific
            //Assert.That(result.Count(), Is.EqualTo(3));
            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(3));
            //Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new [] { 1, 3, 5 }));

            // Not needed here
            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);
        }
    }
}
