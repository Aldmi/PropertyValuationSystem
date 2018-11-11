using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using UserDbWebApi.Entities;

namespace UserDbWebApi.DTO.UserDto
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Компания не установлена")]
        public string UserName { get; set; }

        public string Password { get; set; }
        public CompanyDto Company { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }

    //DEBUG
    public class ApplicationUser2
    {



        public Company Company { get; set; }

        public virtual DateTimeOffset? LockoutEnd { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if two factor authentication is enabled for this
        //     user.
        [PersonalData]
        public virtual bool TwoFactorEnabled { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if a user has confirmed their telephone address.
        [PersonalData]
        public virtual bool PhoneNumberConfirmed { get; set; }
        //
        // Summary:
        //     Gets or sets a telephone number for the user.
        [ProtectedPersonalData]
        public virtual string PhoneNumber { get; set; }
        //
        // Summary:
        //     A random value that must change whenever a user is persisted to the store
        public virtual string ConcurrencyStamp { get; set; }
        //
        // Summary:
        //     A random value that must change whenever a users credentials change (password
        //     changed, login removed)
        public virtual string SecurityStamp { get; set; }
        //
        // Summary:
        //     Gets or sets a salted and hashed representation of the password for this user.
        public virtual string PasswordHash { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if a user has confirmed their email address.
        [PersonalData]
        public virtual bool EmailConfirmed { get; set; }
        //
        // Summary:
        //     Gets or sets the normalized email address for this user.
        public virtual string NormalizedEmail { get; set; }
        //
        // Summary:
        //     Gets or sets the email address for this user.
        [ProtectedPersonalData]
        public virtual string Email { get; set; }
        //
        // Summary:
        //     Gets or sets the normalized user name for this user.
        public virtual string NormalizedUserName { get; set; }
        //
        // Summary:
        //     Gets or sets the user name for this user.
        [ProtectedPersonalData]
        public virtual string UserName { get; set; }
        //
        // Summary:
        //     Gets or sets the primary key for this user.
        [PersonalData]
        public virtual string Id { get; set; }
        //

        public virtual bool LockoutEnabled { get; set; }
        //
        // Summary:
        //     Gets or sets the number of failed login attempts for the current user.
        public virtual int AccessFailedCount { get; set; }

    }
}