var GFTMarket;
(function (GFTMarket) {
    var Models;
    (function (Models) {
        var Item = (function () {
            function Item() {
                this.name = "item.name";
                this.quantity = 0;
                this.id = 0;
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
    var Services;
    (function (Services) {
        var ItemHandler = (function () {
            function ItemHandler() {
                this.itemList = [];
                this.activeObject = new GFTMarket.Models.Item;
            }
            ItemHandler.prototype.push = function (object) {
                this.pushJSON(JSON.stringify(object));
            };
            ItemHandler.prototype.pushJSON = function (object) {
                var helper = JSON.parse(object);
                this.itemList.push(helper);
                console.log(this.itemList);
            };
            ItemHandler.prototype.pop = function () {
                this.itemList.pop();
            };
            ItemHandler.prototype.removeObject = function (object) {
                for (var i = 0; i < this.itemList.length; i++) {
                    if (this.itemList[i].id == object.id && this.itemList[i].name == object.name) {
                        this.itemList.splice(i, 1);
                        return true;
                    }
                }
                return false;
            };
            return ItemHandler;
        }());
        Services.ItemHandler = ItemHandler;
        angular.module("main").service("ItemHandlerService", ItemHandler);
        var FeedHandler = (function () {
            function FeedHandler() {
            }
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
                    item: "=itemModel",
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
            function FeedController($scope, ItemHandlerService, FeedHandlerService) {
                this.ItemHandlerService = ItemHandlerService;
                this.FeedHandlerService = FeedHandlerService;
            }
            FeedController.$inject = ["$scope", "ItemHandlerService", "FeedHandlerService"];
            return FeedController;
        }());
        Controllers.FeedController = FeedController;
        angular.module("main").controller("FeedController", FeedController);
    })(Controllers = GFTMarket.Controllers || (GFTMarket.Controllers = {}));
})(GFTMarket || (GFTMarket = {}));
//# sourceMappingURL=app.js.map