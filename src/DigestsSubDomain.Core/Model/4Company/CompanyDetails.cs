using Shared.Kernel.ForDomain;

namespace Digests.Core.Model._4Company
{
    /// <summary>
    /// Реквизиты фирмы.
    /// ValueObject
    /// </summary>
    public class CompanyDetails : DomainValueObject<CompanyDetails>
    {




        protected override bool EqualsCore(CompanyDetails other)
        {
            throw new System.NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new System.NotImplementedException();
        }
    }
}