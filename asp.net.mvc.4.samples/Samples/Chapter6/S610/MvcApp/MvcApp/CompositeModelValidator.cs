using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp
{
    public class CompositeModelValidator : ModelValidator
    {
        public CompositeModelValidator(ModelMetadata metadata,ControllerContext controllerContext)
            : base(metadata, controllerContext)
        { }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            bool isPropertiesValid = true;
            foreach (ModelMetadata propertyMetadata in Metadata.Properties)
            {
                foreach (ModelValidator validator in propertyMetadata.GetValidators(this.ControllerContext))
                {
                    IEnumerable<ModelValidationResult> results = validator.Validate(propertyMetadata.Model);
                    if (results.Any())
                    {
                        isPropertiesValid = false;
                    }
                    foreach (ModelValidationResult result in results)
                    {
                        yield return new ModelValidationResult
                        {
                            MemberName = DefaultModelBinder.CreateSubPropertyName(propertyMetadata.PropertyName, result.MemberName),
                            Message = result.Message
                        };
                    }
                }
            }

            if (isPropertiesValid)
            {
                foreach (ModelValidator validator in Metadata.GetValidators(this.ControllerContext))
                {
                    IEnumerable<ModelValidationResult> results =validator.Validate(Metadata.Model);
                    foreach (ModelValidationResult result in results)
                    {
                        yield return result;
                    }
                }
            }
        }
    }

}