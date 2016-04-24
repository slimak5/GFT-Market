/// <reference path="_references.ts" />
module GFTMarket.Models {
    export class Item implements GFTMarket.Interfaces.IMarketObject {
        name: string;
        quantity: number;
        id: number;
    }

    export class Feed implements GFTMarket.Interfaces.IMarketObject {
        name: string;
        quantity: number;
        id: number;
    }
}