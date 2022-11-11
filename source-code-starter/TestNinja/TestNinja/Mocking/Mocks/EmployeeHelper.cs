namespace TestNinja.Mocking.Mocks
{
    public class EmployeeHelper : IEmployeeHelper
    {
        private EmployeeContext _employeeContext;

        public EmployeeHelper()
        {
            _employeeContext = new EmployeeContext();
        }
        public void DeleteEmployee(int id)
        {
            var employee = _employeeContext.Employees.Find(id);
            if (employee != null)
            {
                _employeeContext.Employees.Remove(employee);
                _employeeContext.SaveChanges();
            }
        }
    }
}
