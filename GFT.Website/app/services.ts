/// <reference path="_references.ts" />
module GFTMarket.Services {
    export class ItemHandler {
        somedata: string = "orginal";
        test() {
            console.log(this.somedata);
            this.somedata = "modified";
        }

    }
    angular.module("main").service("ItemHandlerService", () => new ItemHandler());
}