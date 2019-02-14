using System;
using System.Collections.Generic;
using Shared.Kernel.ForDomain;

namespace Application.Dto._4Digests
{
    public class CompanyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CompanyDetailsDto CompanyDetails { get; set; }
        public List<HouseDto> Houses { get; set; }
    }
}