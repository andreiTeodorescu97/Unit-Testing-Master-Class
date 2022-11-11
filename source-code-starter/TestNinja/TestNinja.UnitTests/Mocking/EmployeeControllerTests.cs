using Moq;
using NUnit.Framework;
using TestNinja.Mocking;
using TestNinja.Mocking.Mocks;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        [Test]
        public void EmployeeController_DeleteEmployee()
        {
            var helper = new Mock<IEmployeeHelper>();
            var controller = new EmployeeController(helper.Object);
            controller.DeleteEmployee(1);
            helper.Verify(h => h.DeleteEmployee(1));
        }
    }
}
