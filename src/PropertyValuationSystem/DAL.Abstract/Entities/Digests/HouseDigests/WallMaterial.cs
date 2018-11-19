using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Abstract.Entities.Digests.HouseDigests
{
    /// <summary>
    /// Материал Стен
    /// </summary>
    public class WallMaterial : EntityBase
    {
        public string Name { get; set; }
    }
}