/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Directives;
    (function (Directives) {
        var OrderDirective = (function () {
            function OrderDirective() {
                this.restrict = 'AE';
                this.templateUrl = "../Views/_order.html";
                this.scope = {
                    orderModel: "=",
                    OrderHandlerService: "=service"
                };
                this.link = function (scope, element, attrs) {
                };
            }
            OrderDirective.Factory = function () {
                var directive = function () { return new OrderDirective(); };
                return directive;
            };
            return OrderDirective;
        }());
        Directives.OrderDirective = OrderDirective;
        angular.module("main").directive("orderObject", OrderDirective.Factory());
    })(Directives = GFTMarket.Directives || (GFTMarket.Directives = {}));
})(GFTMarket || (GFTMarket = {}));
//# sourceMappingURL=orderdirective.js.map