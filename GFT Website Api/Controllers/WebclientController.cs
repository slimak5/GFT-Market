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
        public static int WebClientIdPool = 1000;
        [HttpGet]
        [EnableCors("*", "*", "*")]
        public int GenerateWebClientId()
        {
            WebClientIdPool++;
            return WebClientIdPool;
        }
    }
}
