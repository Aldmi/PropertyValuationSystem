using System;
using System.Collections.Generic;
using Digests.Core.Model._4House;
using Shared.Kernel.ForDomain;

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

        public Company(string name, CompanyDetails companyDetails)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) //TODO: вынести валидацию разных типов в Shared
                throw new InvalidOperationException("Навание фирмы задано не верно");

            if(companyDetails == null)
                throw new ArgumentNullException(nameof(companyDetails));

            Name = name;
            CompanyDetails = companyDetails;
            _houses = new List<House>();
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