﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI_Live.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Öncelikle manuel bir şekilde bir liste yapısı oluşrup CRUD işlemlerini bunun üzerinde gerçekleştirelim.

        private static List<Employee> employees = new List<Employee>
        {
                new Employee { Id = 1,FName="Ümit",LName="Karaçivi",City="İstanbul"},
                new Employee { Id = 2,FName="Nurgül",LName="Karaçivi",City="Bursa"},
                new Employee { Id = 3,FName="Doğa Bengi",LName="Karaçivi",City="İstanbul"}
        };
        //Http Request Types - Response Codes -> Apilerde kullanılan
        //CRUD - R
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            return Ok(employees); // Bad Request,Not found : API'nin Geri dönüş kodları
        }

        //CRUD - R ById
        [HttpGet("(id)")]
        public async Task<ActionResult<List<Employee>>> GetEmployeeById(int id)
        {

            var employee = employees.Find(e => e.Id == id); //Id ye göre getirilen data
            if (employee == null)
                return BadRequest("Çalışan bulunamadı");

            return Ok(employee); // Bad Request,Not found : API'nin Geri dönüş kodları
        }

        //CRUD - C
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            employees.Add(employee);
            return Ok(employees); // Bad Request,Not found : API'nin Geri dönüş kodları
        }

        //CRUD - U
        [HttpPut] //-->Update işlemlerimizde kullanıyoruz 
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee data)
        {
            var employee = employees.Find(e => e.Id == data.Id); // Id ye göre getiren data
            if (employee == null)
                return BadRequest("Çalışan bulunamadı");

            employee.FName = data.FName;
            employee.LName = data.LName;
            employee.City = data.City;

            return Ok(employees); // Bad Request,Not found : API'nin Geri dönüş kodları
        }

        // CRUD - D
        [HttpDelete("(id)")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {

            var employee = employees.Find(e => e.Id == id); // Id ye göre getirilen data
            if (employee == null)
                return BadRequest("Çalışan bulunamadı");

            employees.Remove(employee);

            return Ok(employees); // Bad Request,Not found : API'nin Geri dönüş kodları
        }
    }
}
