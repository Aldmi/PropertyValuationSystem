using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Digests.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DddTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWorkDigests _unitOfWorkDigests;
        private readonly DigestsService _digestsService;

        public ValuesController(IUnitOfWorkDigests unitOfWorkDigests, DigestsService digestsService)
        {
            _unitOfWorkDigests = unitOfWorkDigests;
            _digestsService = digestsService;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get([FromServices]IUnitOfWorkDigests unitOfWorkDigests)
        {

           // _digestsService.AddNewCompany()

            return new string[] { "value1", "value2" };
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
