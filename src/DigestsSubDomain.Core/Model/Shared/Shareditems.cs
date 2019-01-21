using System.Collections.Generic;
using Digests.Core.Model._4House;
using Shared.Kernel.ForDomain;

namespace Digests.Core.Model.Shared
{
    /// <summary>
    /// Общие элементы для всех компаний.
    /// Entity
    /// </summary>
    public class SharedItems : DomainEntity
    {
        public List<WallMaterial> WallMaterials { get; set; }
    }
}