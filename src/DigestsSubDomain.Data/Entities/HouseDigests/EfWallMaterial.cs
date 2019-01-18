using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.Abstract;

namespace DigestsSubDomain.Data.Entities.HouseDigests
{
    /// <summary>
    /// Материал Стен
    /// </summary>
    public class EfWallMaterial : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        #region NavigationProp
        public List<EfHouse> Houses { get; set; }
        #endregion
    }
}