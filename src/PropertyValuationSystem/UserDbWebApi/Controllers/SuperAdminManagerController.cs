using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserDbWebApi.DTO.RolesDto;
using UserDbWebApi.DTO.UserDto;
using UserDbWebApi.Entities;
using UserDbWebApi.Services;

namespace UserDbWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "SuperAdmin")]
    //[Authorize(Policy = "SuperAdminOnly")]
    public class SuperAdminManagerController : ControllerBase
    {
        #region field

        private readonly UserManagerService _userManager;

        #endregion



        #region ctor

        public SuperAdminManagerController(UserManagerService userManager)
        {
            _userManager = userManager;
        }

        #endregion




        #region Api

        // GET api/SuperAdminManager
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usersDto = await _userManager.GetAll();
            return new JsonResult(usersDto);
        }


        // GET api/SuperAdminManager/GetAllCompany
        [HttpGet("GetAllCompany")]
        public async Task<IActionResult> GetAllCompany()
        {
            var companysDto = await _userManager.GetAllCompanys();
            return new JsonResult(companysDto);
        }


        // GET api/SuperAdminManager/GetAllUsersCompany/{companyName}
        [HttpGet("GetAllUsersCompany/{companyName}")]
        public async Task<IActionResult> GetAllUsersCompany([FromRoute]string companyName)
        {
            if (!await _userManager.CompanyExistsAsync(companyName))
            {
                return BadRequest($"Компания с таким именем НЕ существует {companyName}");
            }
            var users = await _userManager.GetAllUsersCompany(companyName);
            return new JsonResult(users);
        }


        // POST api/SuperAdminManager/AddNewCompany
        [HttpPost("AddNewCompany")]
        public async Task<IActionResult> AddNewCompany([FromBody]CompanyDto companyDto)
        {
            if (companyDto == null)
            {
                ModelState.AddModelError("RoleDto", "POST body is null");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _userManager.CompanyExistsAsync(companyDto.Name))
            {
                return BadRequest($"Компания с таким именем уже существует {companyDto.Name}");
            }

            // добавить РОЛЬ
            var res = await _userManager.AddNewCompany(companyDto);
            return Ok(res.ToString());
        }


        // POST api/SuperAdminManager/AddNewUser
        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddNewUser([FromBody]ApplicationUserDto userDto)
        {
            if (userDto == null)
            {
                ModelState.AddModelError("RoleDto", "POST body is null");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _userManager.UserExistsAsync(userDto.UserName, userDto.Company.Name))
            {
                return BadRequest($"Такой пользователь уже существует {userDto.UserName} в компании {userDto.Company.Name}");
            }

            var res= await _userManager.AddNewUser(userDto);
            return Ok(res.ToString());
        }


        //DELETE api/SuperAdminManager/{userName}/{companyName}
        [HttpDelete("{userName}/{companyName}")]
        public async Task<IActionResult> Delete([FromRoute]string userName, [FromRoute]string companyName)
        {
            companyName = "Скоринг";//DEBUG
            if (!await _userManager.UserExistsAsync(userName, companyName))
            {
                return BadRequest($"Такого пользователя НЕ существует {userName} в компании {companyName}");
            }

            var res = await _userManager.RemoveUser(userName, companyName);
            return Ok(res.ToString());
        }



        // PUT api/SuperAdminManager/ChangeUserName/{id}/{newUserName}
        [HttpPut("ChangeUserName/{userId}/{newUserName}")]
        public async Task<IActionResult> ChangeUserName([FromRoute]string userId, [FromRoute]string newUserName)
        {
            if (!await _userManager.UserExistsAsync(userId))
            {
                return BadRequest($"Такого пользователя НЕ существует {userId}");
            }
            var res = await _userManager.ChangeUserNameAsync(userId, newUserName);
            return Ok(res);
        }


        // PUT api/SuperAdminManager/ChangeUserClaims/{id}
        [HttpPut("ChangeUserClaims/{userId}")]
        public async Task<IActionResult> ChangeUserClaims([FromRoute]string userId, [FromBody] ApplicationUserDto userDto)
        {
            if (userId != userDto.Id)
            {
                throw new Exception("Id пользователя не совпадает");
            }
            if (!await _userManager.UserExistsAsync(userId))
            {
                return BadRequest($"Такого пользователя НЕ существует {userId}");
            }
            var res = await _userManager.ChangeUserClaims(userId, userDto.Claims);
            return Ok(res);
        }


        // PUT api/SuperAdminManager/ChangeUserClaims/{id}
        [HttpPut("ChangeUserClaims/{userId}")]
        public async Task<IActionResult> ChangeUserPassword([FromRoute]string userId, [FromBody] ApplicationUserDto userDto)
        {
            //TODO: Где передавать пароли? Сделать отдельное Dto?
            if (!await _userManager.UserExistsAsync(userId))
            {
                return BadRequest($"Такого пользователя НЕ существует {userId}");
            }
            var res = await _userManager.ChangeUserPassword(userId, "", "");
            return Ok(res);
        }


        #endregion

    }
}