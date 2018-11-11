using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserDbWebApi.Entities;
using UserDbWebApi.Services;

namespace UserDbWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        private readonly RoleManagerService _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public ValuesController(RoleManagerService roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        // GET api/values
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var claims = from c in User.Claims select new { c.Type, c.Value };
            claims = claims.ToList();
            var name = User.Identity.Name;
            var companyNAnme = User.FindFirst("CompanyName").Value;
            var roles = claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

           var users= _userManager.Users.ToList();


            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
