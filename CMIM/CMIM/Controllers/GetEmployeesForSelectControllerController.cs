using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMIM.Models;

namespace CMIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetEmployeesForSelectControllerController : ControllerBase
    {
        private readonly CmimDBContext _context;

        public GetEmployeesForSelectControllerController(CmimDBContext context)
        {
            _context = context;
        }

        // GET: api/GetEmployeesForSelectController
        [HttpGet]
        public IEnumerable<EmployeesVue> Getemployees()
        {
            return _context.employees.Select(E => new EmployeesVue {
                label = E.first_name + " " + E.last_name,
                value = E.matricule 
            }).ToList();
        }
    }

    public class EmployeesVue
    {
        public string value;
        public string label;
    }

}