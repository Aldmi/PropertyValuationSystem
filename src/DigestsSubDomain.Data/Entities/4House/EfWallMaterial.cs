using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Abstract;

namespace Digests.Data.EfCore.Entities._4House
{
    /// <summary>
    /// Материал Стен
    /// </summary>
    [Table("WallMaterial")]
    public class EfWallMaterial : BaseEntity
    {
        public string Name { get; set; }
        public bool IsShared { get; set; }

        #region NavigationProp
        public List<EfHouse> Houses { get; set; } = new List<EfHouse>();
        #endregion
    }
}