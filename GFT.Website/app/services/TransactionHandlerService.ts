/// <reference path="../_references.ts" />
namespace GFTMarket.Services {
    export class TransactionHandlerService {
        transactionList: Array<GFTMarket.Models.Transaction> = [];
        activeTransaction = new GFTMarket.Models.Transaction();
        $http: ng.IHttpService;

        static $inject = ['$http'];
        constructor($http: ng.IHttpService) {
            this.$http = $http;
        }

        public PushTransactionToList(object: GFTMarket.Models.Transaction) {
            if (this.transactionList.length > 8) {
                this.transactionList.splice(this.transactionList.length - 1, 1);
            }
            this.transactionList.unshift(object);
        }
        
        //public remove(object: GFTMarket.Models.Transaction) {
        //    for (let i = 0; i < this.transactionList.length; i++) {
        //        if (this.transactionList[i].id == object.id && this.transactionList[i].name == object.name) {
        //            this.transactionList.splice(i, 1);
        //            i = -1;
        //        }
        //    }

        //}
        //public getById(id: number) {
        //    return this.transactionList[id];
        //}
        //public getByObject(object: GFTMarket.Models.Transaction) {
        //    for (let i = 0; i < this.transactionList.length; i++) {
        //        if (this.transactionList[i].id == object.id && this.transactionList[i].name == object.name) {
        //            return this.transactionList[i];
        //        }
        //    }
        //    console.log("getByObject(): returned empty instance");
        //    return new GFTMarket.Models.Transaction();
        //}
        public CleanTransactionList() {
            this.transactionList = [];
        }

        public GetNewestTransactions() {
            var self = this;
            this.$http.get("http://localhost:54919/api/Feeds/getFeeds/").then(function (response: ng.IHttpPromiseCallbackArg<Array<Models.Transaction>>) {
                self.CleanTransactionList();
                for (let i = 0; i < response.data.length; i++) {
                    self.PushTransactionToList(<Models.Transaction>response.data[i]);
                }
            });

        }
       
    }
    angular.module("main").service("TransactionHandlerService", TransactionHandlerService);
}