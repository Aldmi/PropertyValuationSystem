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

        public string Name { get; private set; }
        public bool IsShared { get; }                 // Общий для всех материал стен (добавляется супер админом)

        #endregion



        #region ctor

        public WallMaterial(string name, bool isShared = false)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Навание материала задано не верно");

            Name = name;
            IsShared = isShared;
        }

        #endregion



        public void ChangeName(string newName)
        {
            Name = newName;
        }


    }
}