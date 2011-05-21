using System;
using StructureMap;
using FluentValidation;

namespace DotNetFlow.Core.Infrastructure
{
    public class StructureMapValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return ObjectFactory.TryGetInstance(validatorType) as IValidator;
        }
    }
}