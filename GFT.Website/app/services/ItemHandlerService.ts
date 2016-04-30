/// <reference path="../_references.ts" />
module GFTMarket.Services {
    export class ItemHandler {
        itemList: Array<GFTMarket.Models.Item> = [];
        activeObject = new GFTMarket.Models.Item();
        $http: ng.IHttpService;
        static $inject = ["$http"];
        constructor($http: ng.IHttpService) {
            this.$http = $http;
        }

        public push(object: GFTMarket.Models.Item) {
            this.pushJSON(JSON.stringify(object));
        }
        private pushJSON(object: string) {
            var helper: GFTMarket.Models.Item = <GFTMarket.Models.Item>JSON.parse(object);
            this.itemList.unshift(helper);
        }
        public remove(object: GFTMarket.Models.Item) {
            for (let i = 0; i < this.itemList.length; i++) {
                if (this.itemList[i].quantity <= 0) {
                    this.itemList.splice(i, 1);
                    i = -1;

                } else {
                    if (this.itemList[i].id == object.id && this.itemList[i].name == object.name) {
                        this.itemList.splice(i, 1);
                        i = -1;
                    }
                }
            }
        }
        public getById(id: number) {
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
        //API CALLS:
        public buyItem(object: GFTMarket.Models.Item) {
            this.$http.post("http://localhost:54919/api/Items/buyItem", object).then(function (res) {
                //TODO handle response
            });

        }
        public sellItem(object: GFTMarket.Models.Item) {
            this.$http.post("http://localhost:54919/api/Items/sellItem", object).then(function (res) {
                //TODO Handle response
            });
        }
    }
    angular.module("main").service("ItemHandlerService", ItemHandler);
}