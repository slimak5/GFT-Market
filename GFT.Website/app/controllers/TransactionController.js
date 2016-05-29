/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Controllers;
    (function (Controllers) {
        var TransactionController = (function () {
            function TransactionController(TransactionHandlerService, $http) {
                this.HubConnection = $.connection("http://localhoasdasdst:53008");
                this.HubProxy = this.HubConnection.hub.createHubProxy("ExecutedTransactionsHub");
                console.log("Creating hub");
                this.TransactionHandlerService = TransactionHandlerService;
                this.$http = $http;
                this.HubProxy.on("sendNewTransaction", function (transaction) {
                    console.log(transaction.transactionDate);
                    this.TransactionHandlerService.PushTransactionToList(transaction);
                });
                this.HubProxy.on("test", function () {
                    console.log("responded");
                });
                var self = this;
                this.HubConnection.start(new SignalRHubConnectionOptions()).done(function () {
                    console.log("Connected to ExecutedTrades: " + self.HubConnection.id);
                }).fail(function () { console.log("Failed"); });
            }
            ;
            TransactionController.$inject = ["TransactionHandlerService", "$http"];
            return TransactionController;
        }());
        Controllers.TransactionController = TransactionController;
        var SignalRHubConnectionOptions = (function () {
            function SignalRHubConnectionOptions() {
                this.jsonp = true;
            }
            return SignalRHubConnectionOptions;
        }());
        angular.module("main").controller("TransactionController", TransactionController);
    })(Controllers = GFTMarket.Controllers || (GFTMarket.Controllers = {}));
})(GFTMarket || (GFTMarket = {}));
//# sourceMappingURL=transactioncontroller.js.map