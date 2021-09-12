using _335as2.Data;
using _335as2.Dtos;
using _335as2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _335as2.Controllers {
    [Route("api")]
    [ApiController]
    public class ProjController : Controller {

        private readonly IAPIRepo _repository;
        public ProjController(IAPIRepo repository) {
            _repository = repository;
        }

        [HttpPost("Register")]
        public ActionResult Register(UserInputDto user) {
            if(user.UserName == "")
                return Ok("Invalid username");
            User u = new User { UserName = user.UserName, Password = user.Password, Address = user.Address };
            bool b = _repository.Register(u);
            if(b)
                return Ok("User successfully registered.");
            else
                return Ok("Username not available.");
        }

        [Authorize(AuthenticationSchemes = "APIAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("GetVersionA")]
        public ActionResult GetVersionA() {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("userName");
            string password = c.Value;
            User user = _repository.GetUserByPassword(password);
            return Ok("v1");
        }



        public IActionResult Index() {
            return View();
        }
    }
}
