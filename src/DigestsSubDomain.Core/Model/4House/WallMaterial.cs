using System;
using Shared.Kernel.ForDomain;

namespace Digests.Core.Model._4House
{
    /// <summary>
    /// Материал Стен.
    /// </summary>
    public class WallMaterial : DomainAggregateRoot
    {
        #region prop

        public string Name { get; }

        #endregion



        #region ctor

        public WallMaterial(string name)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Навание материала задано не верно");

            Name = name;
        }

        #endregion


    }
}