/// <reference path="../_references.ts" />
namespace  GFTMarket.Models
{
    export class Transaction implements GFTMarket.Interfaces.IMarketObject
    {
        public transactionId: number;
        public clientId: number;
        public transactionDate: string;
        public orderedItem: Models.Item;
        public sellOrderId: number;
        public buyOrderId: number;
        public quantity: number;
        public price: number;
    }
}