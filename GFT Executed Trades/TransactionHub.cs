using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using GFT.Models;

namespace GFT.Services.ExecutedTrades
{
    [HubName("ExecutedTransactionsHub")]
    public class TransactionHub : Hub
    {
        public void SendNewTransactionInfo(List<Transaction> transactionList)
        {
            if (transactionList.Count > 0)
                foreach (Transaction transaction in transactionList)
                {
                    Clients.All.sendNewTransaction(transaction);
                }
            Clients.All.test();

        }
    }
}