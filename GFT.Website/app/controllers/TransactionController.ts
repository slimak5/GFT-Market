/// <reference path="../_references.ts" />
namespace GFTMarket.Controllers
{
    export class TransactionController
    {
        public $http: ng.IHttpService;
        public HubConnection: SignalR.Hub.Connection = $.hubConnection("http://localhost:53008");
        public HubProxy: SignalR.Hub.Proxy = this.HubConnection.createHubProxy("ExecutedTransactionsHub");;
        public $scope: ng.IScope;
        public transactionList: Array<GFTMarket.Models.Transaction> = [];

        static $inject = ["$scope", "$http"];
        constructor($scope: ng.IScope, $http: ng.IHttpService)
        {
            var self = this;
            this.$scope = $scope;
            this.$http = $http;

            this.HubProxy.on("SendNewTransaction", function (transaction: Models.Transaction)
            {
                console.log("Transaction received:" + transaction.transactionDate);
                self.PushTransactionToList(transaction);
            });

            console.log("Connecting to ExecutedTrades. . .");
            this.HubConnection.start(new SignalRHubConnectionOptions()).done(function ()
            {
                console.log("Connected to ExecutedTrades: " + self.HubConnection.id);
            }).fail(function () { console.log("Failed") });

        }

        public PushTransactionToList(transaction: GFTMarket.Models.Transaction)
        {
            if (this.transactionList.length > 8)
            {
                this.transactionList.splice(this.transactionList.length - 1, 1);
            }
            this.transactionList.unshift(transaction);
            this.$scope.$apply();
        }
    }

    class SignalRHubConnectionOptions implements SignalR.ConnectionOptions
    {
        jsonp = true;
    }


    angular.module("main").controller("TransactionController", TransactionController);
}