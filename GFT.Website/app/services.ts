/// <reference path="_references.ts" />
module GFTMarket.Services {
    export class ItemHandler {
        itemList: Array<GFTMarket.Models.Item> = [];
        activeObject = new GFTMarket.Models.Item;
         constructor() {
        }

        public push(object: GFTMarket.Models.Item) {
            this.pushJSON(JSON.stringify(object));
        }
        public pushJSON(object: string) {
            var helper:GFTMarket.Models.Item = <GFTMarket.Models.Item>JSON.parse(object);
            this.itemList.push(helper);
            console.log(this.itemList);
            
        }
        public pop() {
            this.itemList.pop();
        }
        public removeObject(object: GFTMarket.Models.Item) {
            for (let i = 0; i < this.itemList.length; i++) {
                if (this.itemList[i].id == object.id && this.itemList[i].name == object.name) {
                    this.itemList.splice(i, 1);
                    //for (let i = 0; i < this.itemList.length; i++) {
                    //    this.itemList[i].id = i;
                    //}
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