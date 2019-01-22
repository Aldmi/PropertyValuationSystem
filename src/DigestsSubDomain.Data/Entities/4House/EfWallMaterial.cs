using System.Collections.Generic;
using Database.Abstract;

namespace Digests.Data.EfCore.Entities._4House
{
    /// <summary>
    /// Материал Стен
    /// </summary>
    public class EfWallMaterial : BaseEntity
    {
        public string Name { get; set; }

        #region NavigationProp
        public List<EfHouse> Houses { get; set; }
        #endregion
    }
}