/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Controllers;
    (function (Controllers) {
        var OrderController = (function () {
            function OrderController($scope, OrderHandlerService, $http) {
                this.OrderHandlerService = OrderHandlerService;
                this.$http = $http;
                this.OrderHandlerService.GetAvaibleItems();
            }
            OrderController.$inject = ["$scope", "OrderHandlerService", "$http"];
            return OrderController;
        }());
        Controllers.OrderController = OrderController;
        angular.module("main").controller("OrderController", OrderController);
    })(Controllers = GFTMarket.Controllers || (GFTMarket.Controllers = {}));
})(GFTMarket || (GFTMarket = {}));
//# sourceMappingURL=ordercontroller.js.map