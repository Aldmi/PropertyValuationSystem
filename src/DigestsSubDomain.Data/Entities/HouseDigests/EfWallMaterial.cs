using System.Collections.Generic;
using Database.Abstract;

namespace DigestsSubDomain.Data.EfCore.Entities.HouseDigests
{
    /// <summary>
    /// Материал Стен
    /// </summary>
    public class EfWallMaterial : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        #region NavigationProp
        public List<EfHouse> Houses { get; set; }
        #endregion
    }
}