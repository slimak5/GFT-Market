/// <reference path="_references.ts" />
module GFTMarket.Services {
    export class ItemHandler {
        itemList: Array<GFTMarket.Models.Item> = [];
        activeObject: GFTMarket.Models.Item = {
            id: 0,
            name: "activeObject.name",
            quantity: 0
        }
        constructor() {
            console.log(this.push(this.activeObject));
            console.log(this.push(this.activeObject));
            console.log(this.push(this.activeObject));
            
            
            

        }

        public push(object: GFTMarket.Models.Item) {
            object.id = this.itemList.length;
            console.log(object);
            for (let i = 0; i < this.itemList.length; i++) {
                if (this.itemList[i].id === object.id) {
                    return false;
                }
            }
            this.itemList.push(object);
            return true;
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
                }
            }
        }
    }
    angular.module("main").service("ItemHandlerService", ItemHandler);

    export class FeedHandler {

    }
    angular.module("main").service("FeedHandlerService", FeedHandler);
}