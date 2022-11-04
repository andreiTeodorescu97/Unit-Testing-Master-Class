using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        [TestCase(15, "FizzBuzz")]
        [TestCase(45, "FizzBuzz")]
        [TestCase(-45, "FizzBuzz")]
        [TestCase(3, "Fizz")]
        [TestCase(6, "Fizz")]
        [TestCase(9, "Fizz")]
        [TestCase(-9, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(25, "Buzz")]
        [TestCase(50, "Buzz")]
        [TestCase(-50, "Buzz")]
        [TestCase(0, "FizzBuzz")]
        [TestCase(1, "1")]
        [TestCase(2, "2")]
        [TestCase(77, "77")]
        public void GetOutput_WhenCalledWithMultipleOf3And5_ReturnFizzBuzz(int input, string expectedResult)
        {
            var result = FizzBuzz.GetOutput(input);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
