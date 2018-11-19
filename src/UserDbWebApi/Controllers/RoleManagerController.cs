using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserDbWebApi.DTO.RolesDto;
using UserDbWebApi.Services;

namespace UserDbWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "SuperAdmin")]
    public class RoleManagerController : ControllerBase
    {
        #region field

        private readonly RoleManagerService _roleManager;

        #endregion




        #region ctor

        public RoleManagerController(RoleManagerService roleManager)
        {
            _roleManager = roleManager;
        }

        #endregion




        #region Api

        // GET api/RoleManager
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rolesDto = await _roleManager.GetAll();
            return new JsonResult(rolesDto);
        }


        // POST api/RoleManager
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RoleDto data)
        {
            if (data == null)
            {
                ModelState.AddModelError("RoleDto", "POST body is null");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _roleManager.RoleExistsAsync(data.Name))
            {
                return BadRequest($"Роль с таким именем уже существует {data.Name}");
            }

            // добавить РОЛЬ
            var res=  await _roleManager.AddRoleAsync(data);   
            return Ok(res);
        }


        // PUT api/RoleManager/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditRole([FromRoute]string id, [FromBody]RoleDto data)
        {
            if (data == null)
            {
                ModelState.AddModelError("RoleDto", "POST body is null");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id == data.Id)
            {
                var res = await _roleManager.EditRoleAsync(data);
                return Ok(res);
            }
            return NotFound("Id не совпадают");
        }


        //DELETE api/RoleManager/Admin
        [HttpDelete("{roleName}")]
        public async Task<IActionResult> Delete([FromRoute]string roleName)
        {
            if (!(await _roleManager.RoleExistsAsync(roleName)))
            {
                return NotFound(roleName);
            }
            var res = await _roleManager.RemoveRoleAsync(roleName);
            return Ok(res);
        }

        #endregion
    }
}