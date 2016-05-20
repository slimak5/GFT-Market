using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Messaging;
using System.Xml.Serialization;

namespace GFT.Website.Api.Controllers
{
    public class FeedsController : ApiController
    {

        static MessageQueue bakToMiddleTierQueue = new MessageQueue(@".\private$\bak.to.mt.queue");
        static List<Models.Feed> feedList = new List<Models.Feed>();
        [HttpGet]
        [EnableCors("*", "*", "*")]
        public List<Models.Feed> getFeeds()
        {
            bakToMiddleTierQueue.MessageReadPropertyFilter.AppSpecific = true;
            Message[] messages = bakToMiddleTierQueue.GetAllMessages();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Models.Feed>));
            foreach (Message message in messages)
            {

                if (message.AppSpecific == 2)
                {
                    bakToMiddleTierQueue.ReceiveById(message.Id);
                    feedList.AddRange((List<Models.Feed>)xmlSerializer.Deserialize(message.BodyStream));
                }
            }
            bakToMiddleTierQueue.Dispose();
            return feedList;
        }

    }
}
