/// <reference path="../_references.ts" />
namespace  GFTMarket.Models {
    export class Order implements GFTMarket.Interfaces.IMarketObject
    {
        constructor() { }

        public orderId: number;
        public clientId: number;
        public item: Models.Item;
        public price: number;
        public quantity: number;
        public orderType: string;
    }
}