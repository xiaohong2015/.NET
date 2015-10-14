using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace MvcApp.Controllers
{
    public class FooController : ApiController
    {
        public void Bar(
        [ModelBinder]
        [FromBody]
        string parameter1,
        [ModelBinder]
        string parameter2,
        [FromBody]
        string parameter3,
        string parameter4,
        CancellationToken parameter5,
        HttpRequestMessage parameter6,
        HttpContent parameter7,
        Baz parameter8
        )
        { }
    }

    public class Baz
    { }
}
