using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Messaging;

namespace GFT.Services.TransactionProcessor
{
    public class TransactionProcessorBAK2 : ITransactionProcessor
    {
        static MessageQueue messageQueue = new MessageQueue(@".\private$\mt.to.bak1.queue");
        static Message[] msgs;
        public void processItems()
        {
            
            
        }

        void sendFeed(Feed feed)
        {
        
        }

        
    }
}
