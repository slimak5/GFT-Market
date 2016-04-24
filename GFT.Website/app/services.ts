/// <reference path="_references.ts" />
module GFTMarket.Services {
    export class ItemHandler {
        itemList: Array<GFTMarket.Models.Item> = [];
        activeObject: GFTMarket.Models.Item = {
            id: 0,
            name: "asdf",
            quantity: 0
        }
        constructor() {
        }

        public push(object: GFTMarket.Models.Item) {
            let helper = object;
            helper.id = this.itemList.length;
            helper.name += "nest";
            this.itemList.push(helper);
            console.log(this.itemList);
            
        }
        public pop(object: GFTMarket.Models.Item) {
            this.itemList.pop();
        }
        public removeObject(object: GFTMarket.Models.Item) {
            for (let i = 0; i < this.itemList.length; i++) {
                if (this.itemList[i] === object) {
                    this.itemList.splice(i, 1);
                    for (let i = 0; i < this.itemList.length; i++) {
                        this.itemList[i].id = i;
                    }
                    return true;
                }
            }
            return false;
        }
    }
    angular.module("main").service("ItemHandlerService", ItemHandler);

    export class FeedHandler {

    }
    angular.module("main").service("FeedHandlerService", FeedHandler);
}