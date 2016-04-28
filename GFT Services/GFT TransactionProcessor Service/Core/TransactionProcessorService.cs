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
        MessageQueue messageQueue = new MessageQueue(@".\private$\mt.to.bak1.queue");
        static Message[] msgs;
        public void sendItem()
        {
            MessageQueueTransaction transaction = new MessageQueueTransaction();
            transaction.Begin();
            msgs = messageQueue.GetAllMessages();
            transaction.Commit();
            
        }

        void sendFeed(Feed feed)
        {
        
        }

        public string getData()
        {
            
            return msgs[0].Body.ToString();
        }
    }
}
