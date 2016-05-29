/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Directives;
    (function (Directives) {
        var TransactionDirective = (function () {
            function TransactionDirective() {
                this.restrict = 'AE';
                this.templateUrl = "../Views/_transaction.html";
                this.scope = {
                    transactionModel: "=",
                    transactionHandler: "=service"
                };
                this.link = function (scope, element, attrs) {
                };
            }
            TransactionDirective.Factory = function () {
                var directive = function () { return new TransactionDirective(); };
                return directive;
            };
            return TransactionDirective;
        }());
        Directives.TransactionDirective = TransactionDirective;
        angular.module("main").directive("transactionObject", TransactionDirective.Factory());
    })(Directives = GFTMarket.Directives || (GFTMarket.Directives = {}));
})(GFTMarket || (GFTMarket = {}));
//# sourceMappingURL=transactiondirective.js.map