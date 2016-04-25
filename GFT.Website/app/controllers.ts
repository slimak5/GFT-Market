﻿/// <reference path="_references.ts" />
module GFTMarket.Controllers {
    export class ItemController {
        ItemHandlerService: GFTMarket.Services.ItemHandler;
        FeedHandlerService: GFTMarket.Services.FeedHandler;
        
        static $inject = ["$scope","ItemHandlerService"];
        constructor($scope: ng.IScope, ItemHandlerService: GFTMarket.Services.ItemHandler,
            FeedHandlerService: GFTMarket.Services.FeedHandler) {
            this.ItemHandlerService = ItemHandlerService;
            this.FeedHandlerService = FeedHandlerService
        }

    }
    angular.module("main").controller("ItemController", ItemController);

    export class FeedController {
        FeedHandlerService: GFTMarket.Services.FeedHandler;

        static $inject = ["$scope", "FeedHandlerService"];
        constructor($scope: ng.IScope, FeedHandlerService: GFTMarket.Services.FeedHandler) {
            this.FeedHandlerService = FeedHandlerService;
        }

    }
    angular.module("main").controller("FeedController", FeedController);
}