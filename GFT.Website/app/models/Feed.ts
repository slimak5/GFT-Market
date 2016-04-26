/// <reference path="../_references.ts" />
module GFTMarket.Models {
    export class Feed implements GFTMarket.Interfaces.IMarketObject {
        name: string;
        quantity: number;
        id: number;
        type: string;

        constructor() {
            this.name = "feed.name";
            this.quantity = 1;
            this.id = 0;
            this.type = "sell";
        }
    }
}