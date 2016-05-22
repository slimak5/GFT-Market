/// <reference path="../_references.ts" />
namespace GFTMarket.Controllers {
    export class OrderController {
        OrderHandlerService: GFTMarket.Services.OrderHandlerService;
        $http: ng.IHttpService;
        static $inject = ["$scope", "OrderHandlerService", "$http"];
        constructor($scope: ng.IScope, OrderHandlerService: GFTMarket.Services.OrderHandlerService,
            $http: ng.IHttpService) {
            this.OrderHandlerService = OrderHandlerService;
            this.$http = $http;
            this.OrderHandlerService.GetAvaibleItems();
        }
    }
    angular.module("main").controller("OrderController", OrderController);
}