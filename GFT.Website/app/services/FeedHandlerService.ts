/// <reference path="../_references.ts" />
module GFTMarket.Services {
    export class FeedHandler {
        feedList: Array<GFTMarket.Models.Feed> = [];
        activeFeed = new GFTMarket.Models.Feed();

        constructor() {
        }

        public push(object: GFTMarket.Models.Feed) {
            this.pushJSON(JSON.stringify(object));
        }
        private pushJSON(object: string) {
            var helper: GFTMarket.Models.Feed = <GFTMarket.Models.Feed>JSON.parse(object);
            this.feedList.push(helper);
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