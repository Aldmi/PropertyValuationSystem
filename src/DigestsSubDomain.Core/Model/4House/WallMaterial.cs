using System;
using Shared.Kernel.ForDomain;

namespace Digests.Core.Model._4House
{
    /// <summary>
    /// Материал Стен.
    /// ValueObject.
    /// </summary>
    public class WallMaterial : DomainValueObject<WallMaterial>
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




        #region OvverideMembers

        protected override bool EqualsCore(WallMaterial other)
        {
            var equality = Name.Equals(other.Name);
            return equality;
        }

        protected override int GetHashCodeCore()
        {
            return Name.GetHashCode();
        }

        #endregion
    }
}