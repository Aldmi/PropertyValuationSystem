using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserDbWebApi.Data;
using UserDbWebApi.DTO.RolesDto;


namespace UserDbWebApi.Services
{
    public class RoleManagerService
    {
        private readonly RoleManager<IdentityRole> _roleManager;


        #region ctor

        public RoleManagerService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        #endregion



        #region Methode

        /// <summary>
        /// Вернуть все роли кроме SuperAdmin
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleDto>> GetAll()
        {
            var roles= await _roleManager.Roles.Where(r => r.Name != "SuperAdmin").ToListAsync();
            var rolesDto = new List<RoleDto>();
            foreach (var role in roles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                rolesDto.Add(new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Claims = roleClaims?.ToDictionary(claim => claim.Type, claim => claim.Value)
                });
            }

           

            return rolesDto;
        }


        /// <summary>
        /// Наличие роли по имени
        /// </summary>
        public async Task<bool> RoleExistsAsync(string name) => await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == name) != null;


        /// <summary>
        /// Получить роль по имени.
        /// </summary>
        public async Task<RoleDto> GetRoleAsync(string name)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == name);
            if (role == null)
                throw new Exception($"Роль не найденна по имени {name}");

            var roleClaims = await _roleManager.GetClaimsAsync(role);
            var roleDto= new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Claims = roleClaims?.ToDictionary(claim => claim.Type, claim => claim.Value)
            };
            return roleDto;
        }


        /// <summary>
        /// Добавить Роль.
        /// </summary>
        public async Task<bool> AddRoleAsync(RoleDto roleDto)
        {
            var role = new IdentityRole(roleDto.Name);
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            var claims = roleDto.Claims.Select(c => new Claim(c.Key, c.Value)).ToList();
            foreach (var cliam in claims)
            {
                result = await _roleManager.AddClaimAsync(role, cliam);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            return true;
        }


        /// <summary>
        /// Редактировать Роль.
        /// </summary>
        public async Task<bool> EditRoleAsync(RoleDto roleDto)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleDto.Id);
            if (role == null)
                return false;

            role.Name= roleDto.Name;
            var result= await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            //УДАЛИТЬ ВСЕ ТЕКУЩИЕ КЛЕЙМЫ У РОЛИ
            var currentClaims = await _roleManager.GetClaimsAsync(role);
            foreach (var currentClaim in currentClaims)
            {
                result = await _roleManager.RemoveClaimAsync(role, currentClaim);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            //ДОБАВИТЬ НОВЫЕ КЛЕЙМЫ ИЗ Dto
            var claims = roleDto.Claims.Select(c => new Claim(c.Key, c.Value)).ToList();
            foreach (var claim in claims)
            {
                result = await _roleManager.AddClaimAsync(role, claim);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            return true;
        }


        /// <summary>
        /// Удалить Роль.
        /// </summary>
        public async Task<bool> RemoveRoleAsync(string roleName)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
                return false;

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            return true;
        }

        #endregion
    }
}