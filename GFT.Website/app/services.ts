/// <reference path="_references.ts" />
module GFTMarket.Services {
    export class ItemHandler {
        itemList: Array<GFTMarket.Models.Item> = [];
        activeObject = new GFTMarket.Models.Item;
        test(object) {
            console.log(object);
        }
        constructor() {
        }

        public push(object: GFTMarket.Models.Item) {
            this.pushJSON(JSON.stringify(object));
        }
        public pushJSON(object: string) {
            var helper: GFTMarket.Models.Item = <GFTMarket.Models.Item>JSON.parse(object);
            this.itemList.push(helper);
            //console.log(this.itemList);
        }
        public remove(object: GFTMarket.Models.Item) {
            for (let i = 0; i < this.itemList.length; i++) {
                if (this.itemList[i].quantity <= 0) {
                    this.itemList.splice(i, 1);
                    i = 0;
                }
                if (this.itemList[i].id == object.id && this.itemList[i].name == object.name) {
                    this.itemList.splice(i, 1);
                    i = 0;
                }
            }
            for (let i = 0; i < this.itemList.length; i++) {
                if (this.itemList[i].id == object.id && this.itemList[i].name == object.name) {
                    this.itemList.splice(i, 1);
                }
            }
        }
        public getById(id:number) {
            return this.itemList[id];
        }
        public getByObject(object: GFTMarket.Models.Item) {
            for (let i = 0; i < this.itemList.length; i++) {
                if (this.itemList[i].id == object.id && this.itemList[i].name == object.name) {
                    console.log(this.itemList[i]);
                    return this.itemList[i];
                }
            }
            console.log("getByObject(): returned empty instance");
            return new GFTMarket.Models.Item();
        }
    }
    angular.module("main").service("ItemHandlerService", ItemHandler);

    export class FeedHandler {

    }
    angular.module("main").service("FeedHandlerService", FeedHandler);
}