/// <reference path="../_references.ts" />
namespace  GFTMarket.Directives {
    export class TransactionDirective implements ng.IDirective {
        restrict: string = 'AE';
        templateUrl: string = "../Views/_transaction.html";
        scope = {
            transactionModel: "=",
            transactionHandler: "=service"
        }
        link = function (scope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes) {
        }

        constructor() {
        }
        static Factory() {
            const directive = () => new TransactionDirective();
            return directive;
        }
    }
    angular.module("main").directive("transactionObject", TransactionDirective.Factory());
}