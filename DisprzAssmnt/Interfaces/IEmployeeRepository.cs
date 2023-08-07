using DisprzAssmnt.Models;

namespace DisprzAssmnt.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee Get(int id);
        void Add(Employee emp);
        void Remove(int id);
        void Update(Employee emp);
    }
}
