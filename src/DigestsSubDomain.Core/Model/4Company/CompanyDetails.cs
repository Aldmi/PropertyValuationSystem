using CSharpFunctionalExtensions;
using FluentValidation;
using Newtonsoft.Json;
using Shared.Kernel.ForDomain;

namespace Digests.Core.Model._4Company
{
    /// <summary>
    /// Реквизиты фирмы.
    /// ValueObject
    /// </summary>
    public class CompanyDetails : DomainValueObject<CompanyDetails>
    {
        #region prop

        public string DetailInfo { get;}

        #endregion



        #region ctor

        [JsonConstructor]
        private CompanyDetails(string detailInfo)
        {
            DetailInfo = detailInfo;
        }

        #endregion



        #region Factory

        public static Result<CompanyDetails, string> Create(string detailInfo)
        {
            CompanyDetails companyDetails = new CompanyDetails(detailInfo);
            CompanyDetailsValidator detailsValidator = new CompanyDetailsValidator();
            var valRes = detailsValidator.Validate(companyDetails);
            if (valRes.IsValid)
            {
                return Result.Ok<CompanyDetails, string>(companyDetails);
            }
            var errors = valRes.ToString("~");
            return Result.Fail<CompanyDetails, string>(errors);
        }

        private class CompanyDetailsValidator : AbstractValidator<CompanyDetails>
        {
            public CompanyDetailsValidator()
            {
                RuleFor(x => x.DetailInfo).NotEmpty();
            }
        }

        #endregion



        #region OvverideMembers
        protected override bool EqualsCore(CompanyDetails other)
        {
            throw new System.NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}