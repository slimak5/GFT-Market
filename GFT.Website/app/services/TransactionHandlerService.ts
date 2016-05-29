/// <reference path="../_references.ts" />
namespace GFTMarket.Services
{
    export class TransactionHandlerService
    {
        transactionList: Array<GFTMarket.Models.Transaction> = [];
        $http: ng.IHttpService;

        static $inject = ['$http'];
        constructor($http: ng.IHttpService)
        {
            this.$http = $http;
            this.transactionList.push(new Models.Transaction());
        }

        public PushTransactionToList(transaction: GFTMarket.Models.Transaction)
        {
            if (this.transactionList.length > 8)
            {
                this.transactionList.splice(this.transactionList.length - 1, 1);
            }
            this.transactionList.unshift(transaction);
        }

        public CleanTransactionList()
        {
            this.transactionList = [];
        }

    }
    angular.module("main").service("TransactionHandlerService", TransactionHandlerService);
}