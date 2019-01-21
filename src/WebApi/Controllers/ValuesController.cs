using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BL.Services.Mediators.DigestMediators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SerilogTimings.Extensions;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _loger;
        private readonly DigestBaseMediator _digestBaseMed;


        public ValuesController(ILogger loger, DigestBaseMediator digestBaseMed)
        {
            _loger = loger;
            _digestBaseMed = digestBaseMed;
        }


        // GET api/values
        [HttpGet]
        //[Authorize(Roles = "SuperAdmin")]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Policy = "SuperAdminOnly")]
        [Authorize(Policy = "Acceess2_Tab1_Policy")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var claims = from c in User.Claims select new {c.Type, c.Value};
            claims = claims.ToList();
            var name = User.Identity.Name;
            var companyNAnme = User.FindFirst("CompanyName").Value;
            var roles = claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

            return new JsonResult(from c in User.Claims select new {c.Type, c.Value});
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public async Task<IEnumerable<WallMaterial>> Get(int id)
        //{
        //    IEnumerable<WallMaterial> res;
        //    using (_loger.TimeOperation("GetWallMaterialAsync Methode"))
        //    {
        //        res = await _digestBaseMed.GetWallMaterialAsync();
        //    }

        //    return res;
        //}

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


        //void Measure(params Action[] callbacks)
        //{
        //    foreach (Action callback in callbacks)
        //    {
        //        Stopwatch stopwatch = Stopwatch.StartNew();
        //        callback();
        //        stopwatch.Stop();
        //        Console.WriteLine(callback.Method.Name + " : " + stopwatch.ElapsedMilliseconds);
        //    }
        //}


        public async Task<T> Measure<T>(Func<Task<T>> func)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var res = await func();
            stopwatch.Stop();
            _loger.Information(">>>>>>>>" + func.Method.Name + " : " + stopwatch.ElapsedMilliseconds);
            return res;
        }
    }
}