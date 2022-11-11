using System.Data.Entity;
using TestNinja.Mocking.Mocks;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private readonly IEmployeeHelper _employeeHelper;

        public EmployeeController(IEmployeeHelper employeeHelper)
        {
            _employeeHelper = employeeHelper;
        }

        public ActionResult DeleteEmployee(int id)
        {
            _employeeHelper.DeleteEmployee(id);
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}