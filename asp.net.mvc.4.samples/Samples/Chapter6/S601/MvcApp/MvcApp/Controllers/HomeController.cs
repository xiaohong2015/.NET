using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ModelValidatorProvider validatorProvider = new DataErrorInfoModelValidatorProvider();
            return View(GetValidators(typeof(Contact), validatorProvider));
        }

        private IEnumerable<ModelValidator> GetValidators(Type dataType,ModelValidatorProvider validatorProvider)
        {
            ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(null, dataType);

            foreach (var validator in validatorProvider.GetValidators(metadata,ControllerContext))
            {
                yield return validator;
            }

            foreach (var propertyMetadata in metadata.Properties)
            {
                foreach (var validator in validatorProvider.GetValidators(propertyMetadata, ControllerContext))
                {
                    yield return validator;
                }
            }
        }
    }

}
