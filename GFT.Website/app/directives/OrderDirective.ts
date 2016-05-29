/// <reference path="../_references.ts" />
namespace  GFTMarket.Directives {
    export class OrderDirective implements ng.IDirective {
        restrict: string = 'AE';
        templateUrl: string = "../Views/_order.html";
        scope = {
            orderModel: "=",
            OrderHandlerService: "=service"
        }
        link = function (scope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes) {
        }
        
        constructor() {
        } 
        static Factory() {
            const directive = () => new OrderDirective();
            return directive;
        }
    }
    angular.module("main").directive("orderObject", OrderDirective.Factory());
}