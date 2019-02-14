using System.Threading.Tasks;
using Application.Dto._4Digests;
using Digests.Core.Model._4Company;
using Digests.Data.Abstract;

namespace Application.Digests.Worker
{
    public class Digests
    {
        private readonly IUnitOfWorkDigests _uowDigests;


        #region ctor

        public Digests(IUnitOfWorkDigests uowDigests)
        {
            _uowDigests = uowDigests;
        }

        #endregion



        #region Methods

        public async Task AddNewCompany(CompanyDto companyDto)
        {
           var companyDetailresResult = CompanyDetails.Create(companyDto.CompanyDetails.DetailInfo);
           if (companyDetailresResult.IsFailure)
           {
                //вернуть Result с ошибкой
           }

           var companyResult= Company.Create(companyDto.Name, companyDetailresResult.Value);
           if(companyResult.IsFailure)
           {
               //вернуть Result с ошибкой
           }

           await _uowDigests.CompanyRepository.AddAsync(companyResult.Value);
           await _uowDigests.SaveChangesAsync();
        }


        #endregion

    }
}