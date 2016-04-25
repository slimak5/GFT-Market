var GFTMarket;
(function (GFTMarket) {
    var Models;
    (function (Models) {
        var Item = (function () {
            function Item() {
                this.name = "item.name";
                this.quantity = 1;
                this.id = 0;
            }
            return Item;
        }());
        Models.Item = Item;
        var Feed = (function () {
            function Feed() {
                this.name = "feed.name";
                this.quantity = 1;
                this.id = 0;
                this.type = "sell";
            }
            return Feed;
        }());
        Models.Feed = Feed;
    })(Models = GFTMarket.Models || (GFTMarket.Models = {}));
})(GFTMarket || (GFTMarket = {}));
var GFTMarket;
(function (GFTMarket) {
    var Modules;
    (function (Modules) {
        angular.module("main", []);
    })(Modules = GFTMarket.Modules || (GFTMarket.Modules = {}));
})(GFTMarket || (GFTMarket = {}));
var GFTMarket;
(function (GFTMarket) {
    var Services;
    (function (Services) {
        var ItemHandler = (function () {
            function ItemHandler() {
                this.itemList = [];
                this.activeObject = new GFTMarket.Models.Item();
            }
            ItemHandler.prototype.push = function (object) {
                this.pushJSON(JSON.stringify(object));
            };
            ItemHandler.prototype.pushJSON = function (object) {
                var helper = JSON.parse(object);
                this.itemList.push(helper);
            };
            ItemHandler.prototype.remove = function (object) {
                for (var i = 0; i < this.itemList.length; i++) {
                    if (this.itemList[i].quantity <= 0) {
                        this.itemList.splice(i, 1);
                        i = -1;
                    }
                    if (this.itemList[i].id == object.id && this.itemList[i].name == object.name) {
                        this.itemList.splice(i, 1);
                        i = -1;
                    }
                }
                for (var i = 0; i < this.itemList.length; i++) {
                    if (this.itemList[i].id == object.id && this.itemList[i].name == object.name) {
                        this.itemList.splice(i, 1);
                    }
                }
            };
            ItemHandler.prototype.getById = function (id) {
                return this.itemList[id];
            };
            ItemHandler.prototype.getByObject = function (object) {
                for (var i = 0; i < this.itemList.length; i++) {
                    if (this.itemList[i].id == object.id && this.itemList[i].name == object.name) {
                        console.log(this.itemList[i]);
                        return this.itemList[i];
                    }
                }
                console.log("getByObject(): returned empty instance");
                return new GFTMarket.Models.Item();
            };
            ItemHandler.prototype.buyItem = function (object) {
            };
            ItemHandler.prototype.sellItem = function (object) {
            };
            return ItemHandler;
        }());
        Services.ItemHandler = ItemHandler;
        angular.module("main").service("ItemHandlerService", ItemHandler);
        var FeedHandler = (function () {
            function FeedHandler() {
                this.feedList = [];
                this.activeFeed = new GFTMarket.Models.Feed();
            }
            FeedHandler.prototype.push = function (object) {
                this.pushJSON(JSON.stringify(object));
                console.log(this.feedList);
            };
            FeedHandler.prototype.pushJSON = function (object) {
                var helper = JSON.parse(object);
                this.feedList.push(helper);
            };
            FeedHandler.prototype.remove = function (object) {
                for (var i = 0; i < this.feedList.length; i++) {
                    if (this.feedList[i].id == object.id && this.feedList[i].name == object.name) {
                        this.feedList.splice(i, 1);
                        i = -1;
                    }
                }
            };
            FeedHandler.prototype.getById = function (id) {
                return this.feedList[id];
            };
            FeedHandler.prototype.getByObject = function (object) {
                for (var i = 0; i < this.feedList.length; i++) {
                    if (this.feedList[i].id == object.id && this.feedList[i].name == object.name) {
                        console.log(this.feedList[i]);
                        return this.feedList[i];
                    }
                }
                console.log("getByObject(): returned empty instance");
                return new GFTMarket.Models.Feed();
            };
            FeedHandler.prototype.pushFeed = function (object) {
            };
            FeedHandler.prototype.popFeed = function (object) {
            };
            return FeedHandler;
        }());
        Services.FeedHandler = FeedHandler;
        angular.module("main").service("FeedHandlerService", FeedHandler);
    })(Services = GFTMarket.Services || (GFTMarket.Services = {}));
})(GFTMarket || (GFTMarket = {}));
var GFTMarket;
(function (GFTMarket) {
    var Directives;
    (function (Directives) {
        var ItemDirective = (function () {
            function ItemDirective() {
                this.restrict = 'AE';
                this.templateUrl = "../Views/_item.html";
                this.scope = {
                    itemModel: "=",
                    ItemHandlerService: "=service"
                };
                this.link = function (scope, element, attrs) {
                };
            }
            ItemDirective.Factory = function () {
                var directive = function () { return new ItemDirective(); };
                return directive;
            };
            return ItemDirective;
        }());
        Directives.ItemDirective = ItemDirective;
        angular.module("main").directive("itemObject", ItemDirective.Factory());
        var FeedDirective = (function () {
            function FeedDirective() {
                this.restrict = 'AE';
                this.templateUrl = "../Views/_feed.html";
                this.scope = {
                    feedModel: "=",
                    FeedHandlerService: "=service"
                };
                this.link = function (scope, element, attrs) {
                };
            }
            FeedDirective.Factory = function () {
                var directive = function () { return new FeedDirective(); };
                return directive;
            };
            return FeedDirective;
        }());
        Directives.FeedDirective = FeedDirective;
        angular.module("main").directive("feedObject", FeedDirective.Factory());
    })(Directives = GFTMarket.Directives || (GFTMarket.Directives = {}));
})(GFTMarket || (GFTMarket = {}));
var GFTMarket;
(function (GFTMarket) {
    var Controllers;
    (function (Controllers) {
        var ItemController = (function () {
            function ItemController($scope, ItemHandlerService, FeedHandlerService) {
                this.ItemHandlerService = ItemHandlerService;
                this.FeedHandlerService = FeedHandlerService;
            }
            ItemController.$inject = ["$scope", "ItemHandlerService"];
            return ItemController;
        }());
        Controllers.ItemController = ItemController;
        angular.module("main").controller("ItemController", ItemController);
        var FeedController = (function () {
            function FeedController($scope, FeedHandlerService) {
                this.FeedHandlerService = FeedHandlerService;
            }
            FeedController.$inject = ["$scope", "FeedHandlerService"];
            return FeedController;
        }());
        Controllers.FeedController = FeedController;
        angular.module("main").controller("FeedController", FeedController);
    })(Controllers = GFTMarket.Controllers || (GFTMarket.Controllers = {}));
})(GFTMarket || (GFTMarket = {}));
//# sourceMappingURL=app.js.map