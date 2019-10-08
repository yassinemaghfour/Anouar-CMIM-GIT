using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMIM.Models;

namespace Cmim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly CmimDBContext _context;

        public EmployeesController(CmimDBContext context)
        {
            _context = context;
			
		}

		// GET: api/Employees
        
		[HttpGet]
        public IEnumerable<Employee> Getemployees()
        {
            return _context.employees;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.employees.Include("Place").Where(emp => emp.matricule == id).ToArrayAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee[0]);
        }

        //Get Filtre Employee
        [HttpGet("{societe}/{activity}")]
        public async Task<IActionResult> GetEmployee([FromRoute] string societe, [FromRoute] int activity, [FromRoute] int Buactivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Employee> employees = _context.employees ;

            if(societe == "VEM" || societe == "VEAS" || societe == "SVL")
                employees = employees.Where(d => d.company.Equals(societe));

            if(activity != -1)
                employees =  employees.Where(d => d.PlaceplacdeId == activity);

            //if (Buactivity != -1 )
            //    employees = employees.Where(d => d.BubuId == Buactivity);
            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] string id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.matricule)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.matricule }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        private bool EmployeeExists(string id)
        {
            return _context.employees.Any(e => e.matricule == id);
        }
    }
}