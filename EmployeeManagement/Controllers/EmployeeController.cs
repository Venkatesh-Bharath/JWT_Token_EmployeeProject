using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {   
      
        private readonly EmployeeDbContext dbContext;
        public EmployeeController(EmployeeDbContext _dbContext) { 
         dbContext= _dbContext;
        }
        [HttpPost("/post")]
        public IActionResult postData(Employee emp)
        {
            dbContext.Employees.AddAsync(emp);
            dbContext.SaveChanges(); 
            return Ok(emp);

        }
     
        [HttpGet("/get")]
        public ICollection<Employee> GetAllData()
        {
          return dbContext.Employees.ToList();
        }
    }


}
