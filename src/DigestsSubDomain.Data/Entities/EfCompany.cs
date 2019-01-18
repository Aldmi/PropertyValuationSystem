using Database.Abstract;

namespace DigestsSubDomain.Data.EfCore.Entities
{
    public class EfCompany : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}