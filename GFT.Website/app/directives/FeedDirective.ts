/// <reference path="../_references.ts" />
module GFTMarket.Directives {
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