using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserDbWebApi.Data;
using UserDbWebApi.DTO.UserDto;
using UserDbWebApi.Entities;

namespace UserDbWebApi.Services
{
    public class UserManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;           //для работы с таблицей Company напрямую



        #region ctor

        public UserManagerService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        #endregion




        #region Api

        /// <summary>
        /// Вернуть всех пользователей
        /// </summary>
        public async Task<List<ApplicationUserDto>> GetAll()
        {
            var usersDto = new List<ApplicationUserDto>();
            var users = await _userManager.Users.Where(user => user.UserName != "SuperAdmin").Include(user => user.Company).ToListAsync();
            foreach (var user in users)
            {
                var userDto = new ApplicationUserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Company = new CompanyDto { Id = user.Company.Id, Name = user.Company.Name },
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };
                //РОЛЬ
                var roleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                userDto.RoleName = roleName;
                //КЛЕЙМЫ
                var userClaims = await _userManager.GetClaimsAsync(user);
                if (userClaims.Any())
                {
                    userDto.Claims = userClaims.ToDictionary(claim => claim.Type, claim => claim.Value);
                }
                usersDto.Add(userDto);
            }

            return usersDto.ToList();
        }


        public async Task<List<ApplicationUserDto>> GetAllUsersCompany(string companyName)
        {
            var usersDto = new List<ApplicationUserDto>();
            var users = await _userManager.Users.Where(user => user.UserName != "SuperAdmin" && user.Company.Name == companyName).Include(user => user.Company).ToListAsync();
            foreach (var user in users)
            {
                var userDto = new ApplicationUserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Company = new CompanyDto { Id = user.Company.Id, Name = user.Company.Name },
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };
                //РОЛЬ
                var roleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                userDto.RoleName = roleName;
                //КЛЕЙМЫ
                var userClaims = await _userManager.GetClaimsAsync(user);
                if (userClaims.Any())
                {
                    userDto.Claims = userClaims.ToDictionary(claim => claim.Type, claim => claim.Value);
                }
                usersDto.Add(userDto);
            }
            return usersDto.ToList();

        }


        /// <summary>
        /// Проверяет есть ли компания по имени.
        /// </summary>
        public async Task<bool> CompanyExistsAsync(string companyName) =>
             await _context.Companys.FirstOrDefaultAsync(company => company.Name == companyName) != null;


        /// <summary>
        /// Получить все компании
        /// </summary>
        public async Task<List<CompanyDto>> GetAllCompanys() =>
            await _context.Companys.Where(company => company.Name != "Супер Компания").Select(company => new CompanyDto { Name = company.Name }).ToListAsync();


        /// <summary>
        /// Добавить новую компанию
        /// </summary>
        public async Task<EntityState> AddNewCompany(CompanyDto companyDto)
        {
            var res = await _context.Companys.AddAsync(new Company { Name = companyDto.Name });
            await _context.SaveChangesAsync();
            return res.State;
        }


        /// <summary>
        /// Проверяет есть сотрудник в компании.
        /// </summary>
        public async Task<bool> UserExistsAsync(string userName, string companyName) =>
            await _userManager.Users.Where(u => u.Company.Name == companyName).FirstOrDefaultAsync(u => u.UserName == userName) != null;


        /// <summary>
        /// Проверяет есть сотрудник в по Id.
        /// </summary>
        public async Task<bool> UserExistsAsync(string userId) => 
            await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId) != null;



        /// <summary>
        /// Добавить Нового пользователя
        /// ConcurrencyStamp - (random value) изменяется всегда когда пользователь сохраняется в БД
        /// SecurityStamp - (random value) изменяется всегда когда меняются данные учетной записи пользователя.
        /// </summary>
        public async Task<bool> AddNewUser(ApplicationUserDto userDto)
        {
            var company = await _context.Companys.FirstOrDefaultAsync(c => c.Name == userDto.Company.Name);
            if (company == null)
                throw new Exception($"Компания не найденна {userDto.Company.Name}");

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == userDto.RoleName);
            if (role == null)
                throw new Exception($"Роль не найденна {userDto.RoleName}");

            var newUser = new ApplicationUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                //PasswordHash = userDto.Password,
                Company = company,
                NormalizedUserName = userDto.UserName.ToUpper(),
                NormalizedEmail = userDto.Email?.ToUpper(),
            };
            //ДОБАВИТЬ ПОЛЬЗОВАТЕЛЯ
            var result = await _userManager.CreateAsync(newUser, userDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            //ДОБАВИТЬ РОЛЬ
            result = _userManager.AddToRoleAsync(newUser, role.Name).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            //ДОБАВИТЬ КЛЕЙМЫ ПОЛЬЗОВАТЕЛЯ
            var claims = userDto.Claims.Select(c => new Claim(c.Key, c.Value)).ToList();
            result = await _userManager.AddClaimsAsync(newUser, claims);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return true;
        }


        /// <summary>
        /// Удалить сотрудника из компании.
        /// Удаляет вместе с клеймами.
        /// </summary>
        public async Task<bool> RemoveUser(string userName, string companyName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => (u.UserName == userName) && (u.Company.Name == companyName));
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return true;
        }


        public async Task<bool> ChangeUserNameAsync(string userId, string newUserName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.UserName = newUserName;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            await _userManager.UpdateNormalizedUserNameAsync(user);
            return true;
        }


        public async Task<bool> ChangeUserClaims(string userId, Dictionary<string, string> newUserClaims)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var claimsNew = newUserClaims.Select(c => new Claim(c.Key, c.Value)).ToList();
            var claimsCurrent=await _userManager.GetClaimsAsync(user);
            foreach (var currentClaim in claimsCurrent)
            {
                var cliamNew = claimsNew.FirstOrDefault(c => c.Type == currentClaim.Type);
                if (cliamNew != null)
                {
                    var result = await _userManager.ReplaceClaimAsync(user, currentClaim, cliamNew);
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
            }
       
            return true;
        }


        public async Task<bool> ChangeUserPassword(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            return true;
        }

        #endregion




        #region Methods

        private async Task<IdentityRole> GetRoleByUserIdAsync(string userId)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId);
            if (userRole == null)
                throw new Exception($"Роль не найденна для этого пользователя {userId}");

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);
            return role;
        }


        private async Task<List<IdentityUserClaim<string>>> GetUserClaimsAsync(string userId)
        {
            var userClaims = await _context.UserClaims.Where(uc => uc.UserId == userId).ToListAsync();
            return userClaims;
        }

        #endregion
    }
}