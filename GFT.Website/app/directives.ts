/// <reference path="_references.ts" />
module GFTMarket.Directives {
    export class ItemDirective implements ng.IDirective {
        restrict: string = 'AE';
        templateUrl: string = "../Views/_item.html";
        scope = {
            itemModel: "=",
        }
        link = function (scope, element, attrs) {
            scope.ItemHandlerService = this.ItemHandlerService;
        }
        constructor(ItemHandlerService: GFTMarket.Services.ItemHandler) {
        }
         
        static Factory() {
            const directive = (ItemHandlerService: GFTMarket.Services.ItemHandler) => new ItemDirective(ItemHandlerService);
            directive.$inject = ["ItemHandlerService"];
            return directive;
        }
    }
    angular.module("main").directive("itemObject", ItemDirective.Factory());
}