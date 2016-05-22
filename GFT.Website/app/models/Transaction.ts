/// <reference path="../_references.ts" />
namespace GFTMarket.Models {
    export class Transaction implements GFTMarket.Interfaces.IMarketObject
    {
        public transactionId: number;
        public clientId: number;
        public orderType: string;
        public orderDate: Date;
        public orderedItem: Models.Item;
        public quantity: number;
        public price: number;
    }
}