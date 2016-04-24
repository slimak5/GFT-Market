/// <reference path="_references.ts" />
module GFTMarket.Directives {
    export class ItemDirective implements ng.IDirective {
        restrict: string = 'AE';
        templateUrl: string = "../Views/_item.html";
        scope = {
            itemModel: "&",
        }
        link = function (scope, element, attrs) {
            scope.ItemHandlerService = this.ItemHandlerService;
        }
        constructor() {
        }
         
        static Factory() {
            const directive = () => new ItemDirective();
            directive.$inject = ["ItemHandlerService"];
            return directive;
        }
    }
    angular.module("main").directive("itemObject", ItemDirective.Factory());
}