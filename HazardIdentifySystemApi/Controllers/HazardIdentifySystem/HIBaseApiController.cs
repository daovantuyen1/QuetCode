using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HazardIdentifySystemApi.Controllers.HazardIdentifySystem
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    [HICustomAuthenApi]
    public class HIBaseApiController : ApiController
    {

    }
}
