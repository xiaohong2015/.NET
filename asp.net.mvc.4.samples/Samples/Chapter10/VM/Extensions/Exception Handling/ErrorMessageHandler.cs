using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Artech.Mvc.Extensions
{
    [ConfigurationElementType(typeof(ErrorMessageHandlerData))]
    public class ErrorMessageHandler : IExceptionHandler
    {
        public string ErrorMessage { get; private set; }
        public ErrorMessageHandler(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            if (null != HttpContext.Current)
            {
                HttpContext.Current.SetErrorMessage(this.ErrorMessage);
            }
            return exception;
        }
    }

    public class ErrorMessageHandlerData : ExceptionHandlerData
    {
        [ConfigurationProperty("errorMessage", IsRequired = true)]
        public string ErrorMessage
        {
            get { return (string)this["errorMessage"]; }
            set { this["errorMessage"] = value; }
        }
        public override IEnumerable<TypeRegistration> GetRegistrations(string namePrefix)
        {
            yield return new TypeRegistration<IExceptionHandler>(() => new ErrorMessageHandler(this.ErrorMessage))
            {
                Name = this.BuildName(namePrefix),
                Lifetime = TypeRegistrationLifetime.Transient
            };
        }
    }
}