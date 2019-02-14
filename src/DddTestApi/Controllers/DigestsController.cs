using System.Threading.Tasks;
using Application.Dto._4Digests;
using Application.Services;
using Digests.Core.Model._4House;
using Digests.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DddTestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DigestsController : ControllerBase
    {
        private readonly IUnitOfWorkDigests _unitOfWorkDigests;
        private readonly DigestsService _digestsService;


        public DigestsController(IUnitOfWorkDigests unitOfWorkDigests, DigestsService digestsService)
        {
            _unitOfWorkDigests = unitOfWorkDigests;
            _digestsService = digestsService;
        }



        // GET
        [HttpGet]
        public async Task<IActionResult> GetCompanys()
        {
            var res = await _digestsService.GetCompanys();


            return new OkResult();
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyDto companyDto)
        {
           var res= await _digestsService.AddNewCompany(companyDto);

           return new OkResult();
        }
    }
}