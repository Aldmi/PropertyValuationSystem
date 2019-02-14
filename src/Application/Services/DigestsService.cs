using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto._4Digests;
using Application.Mapper;
using AutoMapper;
using CSharpFunctionalExtensions;
using Digests.Core.Model._4Company;
using Digests.Data.Abstract;

namespace Application.Services
{
    public class DigestsService
    {
        private readonly IUnitOfWorkDigests _uowDigests;
        private readonly IMapper _mapper;


        #region ctor

        public DigestsService(IUnitOfWorkDigests uowDigests)
        {
            _uowDigests = uowDigests;
            _mapper = ApplicationAutoMapperConfig.Mapper;
        }


        static DigestsService()
        {
            ApplicationAutoMapperConfig.Register();
            try
            {
                ApplicationAutoMapperConfig.AssertConfigurationIsValid();//Если настройки маппинга не валидны будет выбрашенно исключение
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion



        #region Methods

        public async Task<Result> AddNewCompany(CompanyDto companyDto)
        {
           var companyDetailresResult = CompanyDetails.Create(companyDto.CompanyDetails.DetailInfo);
           if (companyDetailresResult.IsFailure)
           {
               return Result.Fail(companyDetailresResult.Error);
           }
           var companyResult= Company.Create(companyDto.Name, companyDetailresResult.Value);
           if(companyResult.IsFailure)
           {
               return Result.Fail(companyDetailresResult.Error);
           }

           await _uowDigests.CompanyRepository.AddAsync(companyResult.Value);
           await _uowDigests.SaveChangesAsync();
           return Result.Ok();
        }



        public async Task<List<CompanyDto>> GetCompanys()
        {
            var companyes= (await _uowDigests.CompanyRepository.ListAsync()).ToList();

            try
            {
                var compDetail = CompanyDetails.Create("fdfd");
                var comp = Company.Create("wqwq", compDetail.Value).Value;
                var companyDto = _mapper.Map<CompanyDto>(comp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
   
            }
     
          return null;
        }



        #endregion

    }
}