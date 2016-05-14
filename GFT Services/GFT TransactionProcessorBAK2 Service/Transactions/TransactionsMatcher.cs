using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFT.Services.TransactionProcessor.TransactionMatcher
{
    public class TransactionMatcher
    {
        static public List<Models.FeedObject> matchTransactions(List<DBModels.Order> buyOrders, List<DBModels.Order> sellOrders)
        {
            List<Models.FeedObject> returnList = new List<Models.FeedObject>();
            if (buyOrders.Count() > 0)
                foreach (DBModels.Order buyOrder in buyOrders)
                {
                    if (sellOrders.Count() > 0)
                    {
                        try
                        {
                            if (buyOrder.Item != null)
                            {
                                DBModels.Order bestCandidate = sellOrders.First(o => o.ItemID == buyOrder.ItemID);
                                foreach (DBModels.Order sellOrder in sellOrders)
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
                                    Models.FeedObject feedobj = new Models.FeedObject(buyOrder, bestCandidate);
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
