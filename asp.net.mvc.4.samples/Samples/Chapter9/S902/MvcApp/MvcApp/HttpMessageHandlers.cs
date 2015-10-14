using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MvcApp
{
    public class FooMessageHandler : DelegatingHandler
    { }

    public class BarMessageHandler : DelegatingHandler
    { }

    public class BazMessageHandler : DelegatingHandler
    { }
}