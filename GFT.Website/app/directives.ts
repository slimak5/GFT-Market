/// <reference path="_references.ts" />
module GFTMarket.Directives {
    export class ItemDirective implements ng.IDirective {
        restrict: string = 'AE';
        templateUrl: string = "../Views/_item.html";
        scope = {
            itemModel: "=",
            ItemHandlerService: "=service"
        }
        link = function (scope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes) {
        }
        
        constructor() {
        } 
        static Factory() {
            const directive = () => new ItemDirective();
            return directive;
        }
    }
    angular.module("main").directive("itemObject", ItemDirective.Factory());


    export class FeedDirective implements ng.IDirective {
        restrict: string = 'AE';
        templateUrl: string = "../Views/_feed.html";
        scope = {
            feedModel: "=",
            FeedHandlerService: "=service"
        }
        link = function (scope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes) {
        }

        constructor() {
        }
        static Factory() {
            const directive = () => new FeedDirective();
            return directive;
        }
    }
    angular.module("main").directive("feedObject", FeedDirective.Factory());
}