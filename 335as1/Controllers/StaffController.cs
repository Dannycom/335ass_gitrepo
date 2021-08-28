using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _335as1.Controllers {
    [Route("api")]
    [ApiController]
    public class StaffController : Controller {






        public IActionResult Index() {
            return View();
        }
    } 
}
