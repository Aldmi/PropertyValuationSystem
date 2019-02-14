using System;
using CSharpFunctionalExtensions;
using FluentValidation;
using Shared.Kernel.ForDomain;
using Shared.Kernel.Validators;

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

        private WallMaterial(string name, bool isShared = false)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Навание материала задано не верно");

            Name = name;
            IsShared = isShared;
        }

        #endregion



        #region Factory

        public static Result<WallMaterial, string> Create(string name, bool isShared = false)
        {
            var wallMaterial = new WallMaterial(name, isShared);
            var wallMaterialValidator = new WallMaterialValidator();
            var valRes = wallMaterialValidator.Validate(wallMaterial);
            if (valRes.IsValid)
            {
                return Result.Ok<WallMaterial, string>(wallMaterial);
            }
            var errors = valRes.ToString("~");
            return Result.Fail<WallMaterial, string>(errors);
        }


        private class WallMaterialValidator : AbstractValidator<WallMaterial>
        {
            public WallMaterialValidator()
            {
                RuleFor(x => x.Name).SetValidator(new StringNotNullNotEmptyValidator());
            }
        }

        #endregion




        public void ChangeName(string newName)
        {
            Name = newName;
        }


    }
}