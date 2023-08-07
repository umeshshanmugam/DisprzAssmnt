using DisprzAssmnt.Interfaces;
using DisprzAssmnt.Models;
using System.Net;
//using System.Web.Http;

namespace DisprzAssmnt.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> employees = new List<Employee>()
        {
             new Employee { EmployeeID = 1, FirstName = "Umesh", LastName = "Shanmugam" },
        new Employee { EmployeeID = 2, FirstName = "Abc", LastName = "xyz" }
        };
        
        public EmployeeRepository()
        {
           
        }
        public void Add(Employee emp)
        {

            if (employees.Any(e => e.EmployeeID == emp.EmployeeID))
                throw new InvalidOperationException("EmployeeID already exists");

            employees.Add(emp);
        }

        public Employee Get(int id)
        {

            return employees.FirstOrDefault(p => p.EmployeeID == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return employees;
        }

        public void Remove(int id)
        {
            Employee employee = Get(id);

            if (employee == null)
            {
                throw new InvalidOperationException("EmployeeID not exists");
            }

            employees.RemoveAll(p => p.EmployeeID == id);
        }

        public void Update(Employee emp)
        {
            Employee employee = Get(emp.EmployeeID);

            if (employee == null)
            {
                throw new InvalidOperationException("Employee not exists");
            }

            employees.Remove(employee);
            employees.Add(emp);
        }
    }
}
