using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Messaging;

namespace GFT.Services.TransactionProcessor
{
    public class TransactionProcessorBAK1 : ITransactionProcessor
    {
        static MessageQueue messageQueue = new MessageQueue(@".\private$\mt.to.bak1.queue");
        static Message[] msgs;

        
        
        public void processItems()
        {
            using(var db = new DBModels.MarketContext())
            {
                DBModels.Feed f = new DBModels.Feed();
                f.Id = 5;
                f.ItemName = "test";
                f.OperationType = "sell";
                f.Quantity = 300;
                db.Feeds.Add(f);
                db.SaveChanges();
            }
        }

        void sendFeed(Feed feed)
        {
        
        }
        
        public static void Configure()
        {
            
            
        }

    }
}
