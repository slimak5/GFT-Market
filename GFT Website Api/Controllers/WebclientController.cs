using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GFT.Website.Api.Controllers
{
    public class WebClientController : ApiController
    {

        [HttpGet]
        [EnableCors("*", "*", "*")]
        public int GenerateWebClientId()
        {
            return new Random().Next(10000, 99999);
        }
    }
}
