/// <reference path="../_references.ts" />
namespace GFTMarket.Models {
    export class Item implements GFTMarket.Interfaces.IMarketObject {
        public itemId: number;
        public itemName: string;
        public supportedServiceId: string;
    }
}