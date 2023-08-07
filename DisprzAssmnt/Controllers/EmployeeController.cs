using DisprzAssmnt.Interfaces;
using DisprzAssmnt.Models;

using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ModelBinding;


namespace DisprzAssmnt.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository empRepo;
        private IEmailService emailService;
        public EmployeeController(IEmployeeRepository _empRepo, IEmailService _emailService)
        {
            empRepo = _empRepo;
            emailService = _emailService;
        }

        [HttpGet(Name ="GetEmp")]
        public Employee Details(int id)
        {
            return empRepo.Get(id);
        }

        // POST: EmployeeController/Create
        [HttpPost(Name ="CreateEmp")]
        public ObjectResult Create(Employee emp)
        {

            Employee _emp = empRepo.Get(emp.EmployeeID);

            if (_emp != null)
            {
                var modelState = new ModelStateDictionary();
                modelState.AddModelError("EmployeeID", "Already Exists");

                return BadRequest(modelState);
            }

            empRepo.Add(emp);
            return Ok(1);
        }

        [HttpPost(Name ="UpdateEmp")]
        public ObjectResult Update(Employee emp)
        {
            Employee _emp = empRepo.Get(emp.EmployeeID);

            if (_emp == null)
            {
                return NotFound(new { Id = emp.EmployeeID, error = string.Format("Employee ID {0} not found", emp.EmployeeID) });
            }

            empRepo.Update(emp);
            return Ok(1);
        }

        [HttpGet(Name = "DeleteEmp")]
        public ObjectResult Remove(int empID)
        {
            Employee _emp = empRepo.Get(empID);

            if (_emp == null)
            {
                return NotFound(new { Id = empID, error = string.Format("Employee ID {0} not found", empID) });
            }

            empRepo.Remove(empID);
            return Ok(1);
        }

        [HttpGet(Name = "SendWelcomeMail")]
        public ObjectResult SendMail(string emailAddress)
        {
            emailService.Send(emailAddress);
            return Ok(1);
        }

    }
}
