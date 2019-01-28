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

        private readonly List<House> _houses = new List<House>();

        #endregion



        #region prop

        public string Name { get; } 
        public CompanyDetails CompanyDetails { get; }
        public IReadOnlyList<House> GetHouses => _houses;

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
        }

        #endregion



        #region Methode

        public void AddHouse(House house)
        {
            _houses.Add(house);
        }

        #endregion
    }
}