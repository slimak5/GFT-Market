/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Services;
    (function (Services) {
        var TransactionHandlerService = (function () {
            function TransactionHandlerService($http) {
                this.transactionList = [];
                this.$http = $http;
                this.transactionList.push(new GFTMarket.Models.Transaction());
            }
            TransactionHandlerService.prototype.PushTransactionToList = function (transaction) {
                if (this.transactionList.length > 8) {
                    this.transactionList.splice(this.transactionList.length - 1, 1);
                }
                this.transactionList.unshift(transaction);
            };
            TransactionHandlerService.prototype.CleanTransactionList = function () {
                this.transactionList = [];
            };
            TransactionHandlerService.$inject = ['$http'];
            return TransactionHandlerService;
        }());
        Services.TransactionHandlerService = TransactionHandlerService;
        angular.module("main").service("TransactionHandlerService", TransactionHandlerService);
    })(Services = GFTMarket.Services || (GFTMarket.Services = {}));
})(GFTMarket || (GFTMarket = {}));
//# sourceMappingURL=transactionhandlerservice.js.map