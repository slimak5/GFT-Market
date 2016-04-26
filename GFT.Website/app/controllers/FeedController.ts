/// <reference path="../_references.ts" />
module GFTMarket.Controllers {
    export class FeedController {
        FeedHandlerService: GFTMarket.Services.FeedHandler;
        $http: ng.IHttpService;
        static $inject = ["$scope", "FeedHandlerService", "$http"];
        constructor($scope: ng.IScope, FeedHandlerService: GFTMarket.Services.FeedHandler, $http: ng.IHttpService) {
            this.FeedHandlerService = FeedHandlerService;
                this.$http = $http;
        }

        public getFeeds() {
            //TODO change host
            var self = this;
            this.$http.get("http://localhost:54919/api/Feeds/getFeeds/").then(function (response: ng.IHttpPromiseCallbackArg<Array<Models.Feed>>) {
                for (let i = 0; i < response.data.length; i++) {
                    self.FeedHandlerService.push(<Models.Feed>response.data[i]);
                }

            });
        }

    }
    angular.module("main").controller("FeedController", FeedController);
}