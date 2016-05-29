/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Controllers;
    (function (Controllers) {
        var WebClientController = (function () {
            function WebClientController($scope, OrderHandlerService, $http) {
                this.webClient = new GFTMarket.Models.WebClient();
                console.time();
                this.$scope = $scope;
                this.$http = $http;
                this.OrderHandlerService = OrderHandlerService;
                this.$scope.$applyAsync(this.GetClientId());
            }
            WebClientController.prototype.GetClientId = function () {
                var self = this;
                this.$http.get("http://localhost:54919/api/Webclient/GenerateWebClientId/").then(function (response) {
                    self.webClient.clientId = response.data;
                    self.OrderHandlerService.ClientId = response.data;
                });
            };
            WebClientController.$inject = ['$scope', 'OrderHandlerService', '$http'];
            return WebClientController;
        }());
        Controllers.WebClientController = WebClientController;
        angular.module("main").controller("WebClientController", WebClientController);
    })(Controllers = GFTMarket.Controllers || (GFTMarket.Controllers = {}));
})(GFTMarket || (GFTMarket = {}));
//# sourceMappingURL=webclientcontroller.js.map