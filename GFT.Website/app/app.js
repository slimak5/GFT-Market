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
    var Services;
    (function (Services) {
        var ItemHandler = (function () {
            function ItemHandler() {
                this.itemList = [];
                this.activeObject = {
                    id: 0,
                    name: "activeObject.name",
                    quantity: 0
                };
                console.log(this.push(this.activeObject));
                console.log(this.push(this.activeObject));
                console.log(this.push(this.activeObject));
            }
            ItemHandler.prototype.push = function (object) {
                object.id = this.itemList.length;
                console.log(object);
                for (var i = 0; i < this.itemList.length; i++) {
                    if (this.itemList[i].id === object.id) {
                        return false;
                    }
                }
                this.itemList.push(object);
                return true;
            };
            ItemHandler.prototype.pop = function (object) {
                this.itemList.pop();
            };
            ItemHandler.prototype.removeObject = function (object) {
                for (var i = 0; i < this.itemList.length; i++) {
                    if (this.itemList[i] === object) {
                        this.itemList.splice(i, 1);
                        for (var i_1 = 0; i_1 < this.itemList.length; i_1++) {
                            this.itemList[i_1].id = i_1;
                        }
                    }
                }
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
            function ItemDirective(ItemHandlerService) {
                this.restrict = 'AE';
                this.templateUrl = "../Views/_item.html";
                this.scope = {
                    itemModel: "=",
                };
                this.link = function (scope, element, attrs) {
                    scope.ItemHandlerService = this.ItemHandlerService;
                };
            }
            ItemDirective.Factory = function () {
                var directive = function (ItemHandlerService) { return new ItemDirective(ItemHandlerService); };
                directive.$inject = ["ItemHandlerService"];
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