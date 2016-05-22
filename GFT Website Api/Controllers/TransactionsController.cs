using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Messaging;
using System.Xml.Serialization;
using GFT.Models;

namespace GFT.Website.Api.Controllers
{
    public class TransactionsController : ApiController
    {

        static MessageQueue bakToMiddleTierQueue = new MessageQueue(@".\private$\bak.to.mt.queue");
        [HttpGet]
        [EnableCors("*", "*", "*")]
        public List<Models.Transaction> GetTransactions()
        {
            List<Transaction> feedList = new List<Transaction>();
            bakToMiddleTierQueue.MessageReadPropertyFilter.AppSpecific = true;
            Message[] messages = bakToMiddleTierQueue.GetAllMessages();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Models.Transaction>));
            foreach (Message message in messages)
            {

                if (message.AppSpecific == 2)
                {
                    bakToMiddleTierQueue.ReceiveById(message.Id);
                    feedList.AddRange((List<Models.Transaction>)xmlSerializer.Deserialize(message.BodyStream));
                }
            }
            bakToMiddleTierQueue.Dispose();
            return feedList;
        }

    }
}
