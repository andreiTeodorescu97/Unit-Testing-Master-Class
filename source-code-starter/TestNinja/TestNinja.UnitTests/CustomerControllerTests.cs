using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(0);

            //The result is exactly a NotFound object
            Assert.That(result, Is.TypeOf<NotFound>());

            //Not found or derivates
            Assert.That(result, Is.InstanceOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IsIsNotZero_ReturnOk()
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(1);

            //The result is exactly a Ok object
            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
