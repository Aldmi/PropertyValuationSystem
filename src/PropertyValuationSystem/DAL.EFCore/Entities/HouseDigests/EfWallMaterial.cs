using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.EFCore.Entities.HouseDigests
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