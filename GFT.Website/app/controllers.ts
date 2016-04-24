/// <reference path="_references.ts" />
module GFTMarket.Controllers {
    export class ItemController {
        ItemHandlerService: GFTMarket.Services.ItemHandler;

        static $inject = ["$scope","ItemHandlerService"];
        constructor($scope: ng.IScope, ItemHandlerService: GFTMarket.Services.ItemHandler) {
            this.ItemHandlerService = ItemHandlerService;
        }

    }
    angular.module("main").controller("ItemController", ItemController);

    export class FeedController {
        ItemHandlerService: GFTMarket.Services.ItemHandler;

        static $inject = ["$scope","ItemHandlerService"];
        constructor($scope: ng.IScope, ItemHandlerService: GFTMarket.Services.ItemHandler) {
            this.ItemHandlerService = ItemHandlerService;
        }

    }
    angular.module("main").controller("FeedController", FeedController);
}