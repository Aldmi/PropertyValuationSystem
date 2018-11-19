namespace DAL.EFCore.Entities
{
    public class EfCompany : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}