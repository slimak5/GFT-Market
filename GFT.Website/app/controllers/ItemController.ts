/// <reference path="../_references.ts" />
module GFTMarket.Controllers {
    export class ItemController {
        ItemHandlerService: GFTMarket.Services.ItemHandler;
        $http: ng.IHttpService;
        static $inject = ["$scope", "ItemHandlerService", "$http"];
        constructor($scope: ng.IScope, ItemHandlerService: GFTMarket.Services.ItemHandler,
            $http: ng.IHttpService) {
            this.ItemHandlerService = ItemHandlerService;
            this.$http = $http;
        }

        public getItems() {
            //TODO change host
            var self = this;
            this.$http.get("http://localhost:54919/api/Items/getItems/").then(function (response: ng.IHttpPromiseCallbackArg<Array<Models.Item>>) {
                for (let i = 0; i < response.data.length; i++) {
                    self.ItemHandlerService.push(<Models.Item>response.data[i]);
                }
            });
        }

    }
    angular.module("main").controller("ItemController", ItemController);
}