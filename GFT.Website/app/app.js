var GFTMarket;
(function (GFTMarket) {
    var Models;
    (function (Models) {
        var Item = (function () {
            function Item() {
            }
            return Item;
        }());
        Models.Item = Item;
        var Feed = (function () {
            function Feed() {
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
    var Controllers;
    (function (Controllers) {
        var ItemController = (function () {
            function ItemController($scope, ItemHandlerService) {
                this.ItemHandlerService = ItemHandlerService;
            }
            ItemController.$inject = ["$scope", "ItemHandlerService"];
            return ItemController;
        }());
        Controllers.ItemController = ItemController;
        angular.module("main").controller("ItemController", ItemController);
        var FeedController = (function () {
            function FeedController($scope, ItemHandlerService) {
                this.ItemHandlerService = ItemHandlerService;
            }
            FeedController.$inject = ["$scope", "ItemHandlerService"];
            return FeedController;
        }());
        Controllers.FeedController = FeedController;
        angular.module("main").controller("FeedController", FeedController);
    })(Controllers = GFTMarket.Controllers || (GFTMarket.Controllers = {}));
})(GFTMarket || (GFTMarket = {}));
var GFTMarket;
(function (GFTMarket) {
    var Services;
    (function (Services) {
        var ItemHandler = (function () {
            function ItemHandler() {
                this.somedata = "orginal";
            }
            ItemHandler.prototype.test = function () {
                console.log(this.somedata);
                this.somedata = "modified";
            };
            return ItemHandler;
        }());
        Services.ItemHandler = ItemHandler;
        angular.module("main").service("ItemHandlerService", function () { return new ItemHandler(); });
    })(Services = GFTMarket.Services || (GFTMarket.Services = {}));
})(GFTMarket || (GFTMarket = {}));
//# sourceMappingURL=app.js.map