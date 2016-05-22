/// <reference path="../_references.ts" />
namespace GFTMarket.Controllers {
    class SignalRHubConnectionOptions implements SignalR.ConnectionOptions {
        jsonp = true;
    }
    export class TransactionController {
        $scope: ng.IScope;
        TransactionHandlerService: GFTMarket.Services.TransactionHandlerService;
        $http: ng.IHttpService;
        hubConnection: SignalR.Hub.Connection;
        hubProxy: SignalR.Hub.Proxy;
        static $inject = ["$scope", "TransactionHandlerService", "$http"];
        constructor($scope: ng.IScope, TransactionHandlerService: GFTMarket.Services.TransactionHandlerService, $http: ng.IHttpService) {
            this.TransactionHandlerService = TransactionHandlerService;
            this.$http = $http;
            this.$scope = $scope;
            this.hubConnection = $.hubConnection("http://localhost:53008");
            this.hubProxy = this.hubConnection.createHubProxy("Transactions");
            this.hubProxy.on("SendFeed", (transaction: Models.Transaction) => {
                this.TransactionHandlerService.PushTransactionToList(transaction);
            });
            this.hubConnection.start(new SignalRHubConnectionOptions());
        }
    }
    angular.module("main").controller("TransactionController", TransactionController);
}