using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Digests.Core.Model._4House;
using FluentValidation;
using Shared.Kernel.ForDomain;
using Shared.Kernel.Validators;

namespace Digests.Core.Model._4Company
{
    /// <summary>
    /// Компания.
    /// AgregateRoot
    /// </summary>
    public class Company : DomainAggregateRoot
    {
        #region fields

        private List<House> _houses;

        #endregion



        #region prop

        public string Name { get; } 
        public CompanyDetails CompanyDetails { get; private set; }

        public IReadOnlyList<House> GetHouses
        {
            get => _houses;
            private set => _houses = value as List<House>; //для маппинга
        }
        #endregion



        #region ctor

        private Company(string name, CompanyDetails companyDetails)
        {
            Name = name;
            CompanyDetails = companyDetails;
            _houses = new List<House>();
        }

        #endregion



        #region Factory

        public static Result<Company, string> Create(string name, CompanyDetails companyDetails)
        {
            Company company = new Company(name, companyDetails);
            CompanyValidator validator = new CompanyValidator();
            var valRes = validator.Validate(company);
            if (valRes.IsValid)
            {
                return Result.Ok<Company, string>(company);
            }
            var errors = valRes.ToString("~");
            return Result.Fail<Company, string>(errors);
        }

        private class CompanyValidator : AbstractValidator<Company>
        {
            public CompanyValidator()
            {
                RuleFor(x => x.Name).SetValidator(new StringNotNullNotEmptyValidator());
                RuleFor(x => x.CompanyDetails).NotNull();
            }
        }

        #endregion



        #region Methode

        public void AddHouse(House house)
        {
            _houses.Add(house);
        }


        public void RemoveHouse(House house)
        {
            _houses.Remove(house);
        }


        public void UpdateCompanyDetails(CompanyDetails newCompanyDetails)
        {
            CompanyDetails = newCompanyDetails;
        }

        #endregion



    }





}