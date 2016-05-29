/// <reference path="../_references.ts" />
namespace  GFTMarket.Controllers
{
    export class WebClientController
    {
        public webClient: Models.WebClient = new Models.WebClient();
        OrderHandlerService: Services.OrderHandlerService;
        $scope: ng.IScope;
        $http: ng.IHttpService;
        static $inject = ['$scope', 'OrderHandlerService', '$http'];
        constructor($scope: ng.IScope, OrderHandlerService: Services.OrderHandlerService, $http: ng.IHttpService)
        {
            console.time();
            this.$scope = $scope;
            this.$http = $http;
            this.OrderHandlerService = OrderHandlerService;
            this.$scope.$applyAsync(this.GetClientId());
        }

        GetClientId(): any
        {
            var self = this;
            this.$http.get("http://localhost:54919/api/Webclient/GenerateWebClientId/").then(function (response: ng.IHttpPromiseCallbackArg<number>)
            {
                self.webClient.clientId = response.data;
                self.OrderHandlerService.ClientId = response.data;
            });

        }
    }
    angular.module("main").controller("WebClientController", WebClientController);
}