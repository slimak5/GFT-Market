/// <reference path="../_references.ts" />
module GFTMarket.Controllers {
    class opt implements SignalR.ConnectionOptions {
        constructor(){ }
        jsonp = true;
    }
    export class FeedController {
        FeedHandlerService: GFTMarket.Services.FeedHandler;
        $http: ng.IHttpService;
        con: SignalR.Hub.Connection;
        hub: SignalR.Hub.Proxy;
        opt: SignalR.ConnectionOptions;
        static $inject = ["$scope", "FeedHandlerService", "$http"];
        constructor($scope: ng.IScope, FeedHandlerService: GFTMarket.Services.FeedHandler, $http: ng.IHttpService) {
            this.FeedHandlerService = FeedHandlerService;
            this.$http = $http;
            this.getFeeds();

            this.con = $.hubConnection("http://localhost:53008");
            this.hub = this.con.createHubProxy("Feeds");
            this.hub.on("pushFeed", (feed: Models.Feed) => {
                this.FeedHandlerService.push(feed);
            });
            this.opt = new opt();
            this.con.start(this.opt);
        }

        public getFeeds() {
            //TODO change host
            var self = this;
            this.$http.get("http://localhost:54919/api/Feeds/getFeeds/").then(function (response: ng.IHttpPromiseCallbackArg<Array<Models.Feed>>) {
                self.FeedHandlerService.clean();
                for (let i = 0; i < response.data.length; i++) {
                    self.FeedHandlerService.push(<Models.Feed>response.data[i]);
                }
            });
        }
    }
    angular.module("main").controller("FeedController", FeedController);
}