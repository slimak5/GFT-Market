/// <reference path="_references.ts" />
module GFTMarket.Controllers {
    export class ItemController {
        ItemHandlerService: GFTMarket.Services.ItemHandler;
        FeedHandlerService: GFTMarket.Services.FeedHandler;
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
            this.$http.get("http://localhost:54919/api/Items/getItems/").success(function (response: Array<Models.Item>) {
                for (let i = 0; i < response.length; i++) {
                    self.ItemHandlerService.push(<Models.Item>response[i]);
                }
            });
        }

    }
    angular.module("main").controller("ItemController", ItemController);

    export class FeedController {
        FeedHandlerService: GFTMarket.Services.FeedHandler;
        $http: ng.IHttpService;
        static $inject = ["$scope", "FeedHandlerService", "$http"];
        constructor($scope: ng.IScope, FeedHandlerService: GFTMarket.Services.FeedHandler, $http: ng.IHttpService) {
            this.FeedHandlerService = FeedHandlerService;
                this.$http = $http;
        }

        public getFeeds() {
            //TODO change host
            var self = this;
            this.$http.get("http://localhost:54919/api/Feeds/getFeeds/").success(function (response: Array<Models.Feed>) {
                for (let i = 0; i < response.length; i++) {
                    self.FeedHandlerService.push(<Models.Feed>response[i]);
                }

            });
        }

    }
    angular.module("main").controller("FeedController", FeedController);
}