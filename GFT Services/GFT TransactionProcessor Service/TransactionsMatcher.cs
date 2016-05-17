using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFT.Services.TransactionProcessor.TransactionMatcher
{
    public class TransactionMatcher
    {
        static public List<Models.Transaction> matchTransactions(List<DbModels.OrderEntity> buyOrders, List<DbModels.OrderEntity> sellOrders)
        {
            List<Models.Transaction> returnList = new List<Models.Transaction>();
            if (buyOrders.Count() > 0)
                foreach (DbModels.OrderEntity buyOrder in buyOrders)
                {
                    if (sellOrders.Count() > 0)
                    {
                        try
                        {
                            if (buyOrder.Item != null)
                            {
                                DbModels.OrderEntity bestCandidate = sellOrders.First(o => o.ItemID == buyOrder.ItemID);
                                foreach (DbModels.OrderEntity sellOrder in sellOrders)
                                {
                                    if (sellOrder.Item != null)
                                    {
                                        if (sellOrder.Price < bestCandidate.Price)
                                        {
                                            bestCandidate = sellOrder;
                                        }
                                    }
                                }
                                if (bestCandidate != null && bestCandidate.Item != null)
                                {
                                    Models.Transaction feedobj = new Models.Transaction(buyOrder, bestCandidate);
                                    returnList.Add(feedobj);
                                }
                                bestCandidate = null;
                            }
                        }
                        catch (Exception e)
                        {

                            break;
                        }
                    }
                }
            if (returnList.Count() != 0)
                return returnList;
            else
                throw new InvalidOperationException();
        }
    }
}
