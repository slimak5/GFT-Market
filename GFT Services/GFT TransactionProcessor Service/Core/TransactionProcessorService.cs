using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Messaging;

namespace GFT.Services.TransactionProcessor
{
    public class TransactionProcessor : ITransactionProcessor
    {
        MessageQueue mq = new MessageQueue("./private$/MTtoBAK");

        public void sendItem(Item item)
        {

        }

        public void sendFeed(Feed feed)
        {
        
        }
    }
}
