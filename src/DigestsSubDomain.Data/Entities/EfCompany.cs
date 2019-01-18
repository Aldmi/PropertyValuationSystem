using Database.Abstract;

namespace Digests.Data.EfCore.Entities
{
    public class EfCompany : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}