/// <reference path="../_references.ts" />
namespace  GFTMarket.Controllers
{
    export class TransactionController
    {
        public TransactionHandlerService: GFTMarket.Services.TransactionHandlerService;
        public $http: ng.IHttpService;
        public HubConnection: SignalR.Connection = $.connection("http://localhoasdasdst:53008");
        public HubProxy: SignalR.Hub.Proxy = this.HubConnection.hub.createHubProxy("ExecutedTransactionsHub");;
       
        static $inject = ["TransactionHandlerService", "$http"];
        constructor(TransactionHandlerService: GFTMarket.Services.TransactionHandlerService, $http: ng.IHttpService)
        {
            console.log("Creating hub");
            this.TransactionHandlerService = TransactionHandlerService;
            this.$http = $http;
            this.HubProxy.on("sendNewTransaction", function (transaction: Models.Transaction)
            {
                console.log(transaction.transactionDate);
                this.TransactionHandlerService.PushTransactionToList(transaction);
            });

            this.HubProxy.on("test", function ()
            {
                console.log("responded");
            });
            var self = this;
            this.HubConnection.start(new SignalRHubConnectionOptions()).done(function ()
            {
                console.log("Connected to ExecutedTrades: " + self.HubConnection.id);
            }).fail(function () { console.log("Failed") });
        }
    }

    class SignalRHubConnectionOptions implements SignalR.ConnectionOptions
    {
        jsonp = true;
    }


    angular.module("main").controller("TransactionController", TransactionController);
}