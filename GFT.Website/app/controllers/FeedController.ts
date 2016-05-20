/// <reference path="../_references.ts" />
module GFTMarket.Controllers {
    class SignalRHubConnectionOptions implements SignalR.ConnectionOptions {
        constructor() { }
        jsonp = true;
    }
    export class FeedController {
        $scope: ng.IScope;
        FeedHandlerService: GFTMarket.Services.FeedHandler;
        $http: ng.IHttpService;
        hubConnection: SignalR.Hub.Connection;
        hubProxy: SignalR.Hub.Proxy;
        static $inject = ["$scope", "FeedHandlerService", "$http"];
        constructor($scope: ng.IScope, FeedHandlerService: GFTMarket.Services.FeedHandler, $http: ng.IHttpService) {
            this.FeedHandlerService = FeedHandlerService;
            this.$http = $http;
            this.$scope = $scope;
            this.hubConnection = $.hubConnection("http://localhost:53008");
            this.hubProxy = this.hubConnection.createHubProxy("Feeds");
            this.hubProxy.on("SendFeed", (feed: Models.Feed) => {
                this.FeedHandlerService.PushFeedToList(feed);
            });
            this.hubConnection.start(new SignalRHubConnectionOptions());
        }

        public GetNewestFeeds() {
            var self = this;
            this.$http.get("http://localhost:54919/api/Feeds/getFeeds/").then(function (response: ng.IHttpPromiseCallbackArg<Array<Models.Feed>>) {
                self.FeedHandlerService.CleanFeedList();
                for (let i = 0; i < response.data.length; i++) {
                    self.$scope.$apply(function () {
                        self.FeedHandlerService.PushFeedToList(<Models.Feed>response.data[i]);
                    });
                }
            });

        }
    }
    angular.module("main").controller("FeedController", FeedController);
}