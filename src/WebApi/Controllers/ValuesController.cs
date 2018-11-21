using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Services.Mediators.DigestMediators;
using DAL.Abstract.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class ValuesController : ControllerBase
    {
        private readonly IHouseRepository _houseRepository;
        private readonly ILogger _loger;
        private readonly DigestBaseMediator _digestBaseMed;


        public ValuesController(IHouseRepository houseRepository, ILogger loger, DigestBaseMediator digestBaseMed)
        {
            _houseRepository = houseRepository;
            _loger = loger;
            _digestBaseMed = digestBaseMed;
        }



        // GET api/values
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Policy = "SuperAdminOnly")]
        //[Authorize(Policy = "ManagerOnly")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var claims = from c in User.Claims select new { c.Type, c.Value };
            claims = claims.ToList();
            var name = User.Identity.Name;
            var companyNAnme = User.FindFirst("CompanyName").Value;
            var roles = claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var res= await _digestBaseMed.GetWallMaterialAsync();          
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
