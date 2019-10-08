using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImporterEmployeesController : ControllerBase
    {
        [HttpPost]
        public bool ImporterLesDonnes([FromBody] string pathFile)
        {
            bool isImported = false;



            return isImported;
        }
    }
}