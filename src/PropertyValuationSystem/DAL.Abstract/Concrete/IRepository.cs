using DAL.Abstract.Abstract;
using DAL.Abstract.Entities;


namespace DAL.Abstract.Concrete
{
    /// <summary>
    /// Доступ к Компаниям
    /// </summary>
    public interface ICompanyRepository : IGenericDataRepository<Company>
    {  
    }

}