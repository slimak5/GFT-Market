/// <reference path="../_references.ts" />
namespace GFTMarket.Controllers {
    export class WebClientController {
        public webClient: Models.WebClient = new Models.WebClient();
        $scope: ng.IScope;

        static $inject = ['$scope'];
        constructor($scope: ng.IScope) {
            this.$scope = $scope;
            this.webClient.clientId = this.GetClientId();
        }

        GetClientId(): number {
            $.get("http://localhost:54919/api/Webclient/GenerateWebClientId/", function (response: JQueryXHR) {
                return <number>response.responseBody;
            });
            return -1;
        }
    }
    angular.module("main").controller("WebClientController", WebClientController);
}