/// <reference path="../_references.ts" />
module GFTMarket.Services {
    export class FeedHandler {
        feedList: Array<GFTMarket.Models.Feed> = [];
        activeFeed = new GFTMarket.Models.Feed();
        con: SignalR.Hub.Connection;
        hub: SignalR.Hub.Proxy;
        constructor() {
            //this.con = $.hubConnection("http://localhost:53008");
            //this.hub = this.con.createHubProxy("Feeds");
            //this.hub.on("pushFeed", (feed: Models.Feed) => {
            //    this.push(feed);
            //});
        }

        public push(object: GFTMarket.Models.Feed) {
            this.pushJSON(JSON.stringify(object));
        }
        private pushJSON(object: string) {
            var helper: GFTMarket.Models.Feed = <GFTMarket.Models.Feed>JSON.parse(object);
            if (this.feedList.length > 8) {
                this.feedList.splice(this.feedList.length-1, 1);
            }
            this.feedList.unshift(helper);
        }
        public remove(object: GFTMarket.Models.Feed) {
            for (let i = 0; i < this.feedList.length; i++) {
                if (this.feedList[i].id == object.id && this.feedList[i].name == object.name) {
                    this.feedList.splice(i, 1);
                    i = -1;
                }
            }

        }
        public getById(id: number) {
            return this.feedList[id];
        }
        public getByObject(object: GFTMarket.Models.Feed) {
            for (let i = 0; i < this.feedList.length; i++) {
                if (this.feedList[i].id == object.id && this.feedList[i].name == object.name) {
                    return this.feedList[i];
                }
            }
            console.log("getByObject(): returned empty instance");
            return new GFTMarket.Models.Feed();
        }
        public clean() {
            this.feedList = [];
        }
        //API CALLS:
        public pushFeed(object: GFTMarket.Models.Feed) {
            //TODO
        }
        public popFeed(object: GFTMarket.Models.Feed) {
            //TODO
        }
    }
    angular.module("main").service("FeedHandlerService", FeedHandler);
}