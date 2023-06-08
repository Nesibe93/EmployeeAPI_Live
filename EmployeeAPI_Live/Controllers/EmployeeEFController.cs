using EmployeeAPI_Live.Controllers.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI_Live.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeEFController : ControllerBase
    {

        private readonly AppDbContext _context;

        public EmployeeEFController(AppDbContext context)
        {
            _context = context;
        }

        // Öncelikle manuel bir şekilde bir liste yapısı oluşrup CRUD işlemlerini bunun üzerinde gerçekleştirelim.

        //Http Request Types - Response Codes -> Apilerde kullanılan
        //CRUD - R
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            return Ok(await _context.Employees.ToListAsync()); // Bad Request,Not found : API'nin Geri dönüş kodları
        }

        //CRUD - R ById
        [HttpGet("(id)")]
        public async Task<ActionResult<List<Employee>>> GetEmployeeById(int id)
        {

            var employee = await _context.Employees.FindAsync(id); //Id ye göre getirilen data
            if (employee == null)
                return BadRequest("Çalışan bulunamadı");

            return Ok(await _context.Employees.ToListAsync()); // Bad Request,Not found : API'nin Geri dönüş kodları
        }

        //CRUD - C
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync()); // Bad Request,Not found : API'nin Geri dönüş kodları
        }

        //CRUD - U
        [HttpPut] //-->Update işlemlerimizde kullanıyoruz 
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee data)
        {
            var employee = await _context.Employees.FindAsync(data.Id); // Id ye göre getiren data
            if (employee == null)
                return BadRequest("Çalışan bulunamadı");

            employee.FName = data.FName;
            employee.LName = data.LName;
            employee.City = data.City;

            await _context.SaveChangesAsync();
            return Ok(await _context.Employees.ToListAsync()); // Bad Request,Not found : API'nin Geri dönüş kodları
        }

        // CRUD - D
        [HttpDelete("(id)")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id); // Id ye göre getirilen data
            if (employee == null)
                return BadRequest("Çalışan bulunamadı");

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync()); // Bad Request,Not found : API'nin Geri dönüş kodları
        }
    }
}
