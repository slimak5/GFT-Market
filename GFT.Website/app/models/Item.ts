/// <reference path="../_references.ts" />
module GFTMarket.Models {
    export class Item implements GFTMarket.Interfaces.IMarketObject {
        name: string;
        quantity: number;
        id: number;

        constructor() {
            this.name = "item.name";
            this.quantity = 1;
            this.id = 0;
        }
    }
}