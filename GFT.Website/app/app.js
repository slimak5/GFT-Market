/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Modules;
    (function (Modules) {
        angular.module("main", []);
    })(Modules = GFTMarket.Modules || (GFTMarket.Modules = {}));
})(GFTMarket || (GFTMarket = {}));
/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Services;
    (function (Services) {
        var OrderHandlerService = (function () {
            function OrderHandlerService($http) {
                this.orderList = [];
                this.$http = $http;
            }
            OrderHandlerService.prototype.PushOrderToList = function (object) {
                this.orderList.unshift(object);
            };
            OrderHandlerService.prototype.CleanOrderList = function () {
                this.orderList = [];
            };
            //API CALLS:
            OrderHandlerService.prototype.SendBuyRequest = function (object) {
                object.orderType = "BUY";
                object.clientId = this.ClientId;
                var self = this;
                this.$http.get("http://localhost:54919/api/Orders/GenerateOrderId")
                    .then(function (response) {
                    object.orderId = response.data;
                    console.log("BUY ID Get:" + object.orderId);
                    self.$http.post("http://localhost:54919/api/Orders/SendBuyOrder", object)
                        .then(function (res) {
                        toastr.success(res.data);
                    });
                });
            };
            OrderHandlerService.prototype.SendSellRequest = function (object) {
                object.orderType = "SELL";
                object.clientId = this.ClientId;
                var self = this;
                this.$http.get("http://localhost:54919/api/Orders/GenerateOrderId")
                    .then(function (response) {
                    object.orderId = response.data;
                    console.log("SELL ID Get:" + object.orderId);
                    self.$http.post("http://localhost:54919/api/Orders/SendSellOrder", object)
                        .then(function (res) {
                        toastr.success(res.data);
                    });
                });
            };
            OrderHandlerService.prototype.GetAvaibleItems = function () {
                var self = this;
                this.$http.get("http://localhost:54919/api/Orders/GetItems/")
                    .then(function (response) {
                    self.CleanOrderList();
                    for (var i = 0; i < response.data.length; i++) {
                        self.PushOrderToList(response.data[i]);
                    }
                });
            };
            OrderHandlerService.$inject = ["$http"];
            return OrderHandlerService;
        }());
        Services.OrderHandlerService = OrderHandlerService;
        angular.module("main").service("OrderHandlerService", OrderHandlerService);
    })(Services = GFTMarket.Services || (GFTMarket.Services = {}));
})(GFTMarket || (GFTMarket = {}));
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
/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Directives;
    (function (Directives) {
        var TransactionDirective = (function () {
            function TransactionDirective() {
                this.restrict = 'AE';
                this.templateUrl = "../Views/_transaction.html";
                this.scope = {
                    transactionModel: "=",
                    transactionHandler: "=service"
                };
                this.link = function (scope, element, attrs) {
                };
            }
            TransactionDirective.Factory = function () {
                var directive = function () { return new TransactionDirective(); };
                return directive;
            };
            return TransactionDirective;
        }());
        Directives.TransactionDirective = TransactionDirective;
        angular.module("main").directive("transactionObject", TransactionDirective.Factory());
    })(Directives = GFTMarket.Directives || (GFTMarket.Directives = {}));
})(GFTMarket || (GFTMarket = {}));
/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Controllers;
    (function (Controllers) {
        var TransactionController = (function () {
            function TransactionController($scope, $http) {
                this.HubConnection = $.hubConnection("http://localhost:53008");
                this.HubProxy = this.HubConnection.createHubProxy("ExecutedTransactionsHub");
                this.transactionList = [];
                var self = this;
                this.$scope = $scope;
                this.$http = $http;
                this.HubProxy.on("SendNewTransaction", function (transaction) {
                    console.log("Transaction received:" + transaction.transactionDate);
                    self.PushTransactionToList(transaction);
                });
                console.log("Connecting to ExecutedTrades. . .");
                this.HubConnection.start(new SignalRHubConnectionOptions()).done(function () {
                    console.log("Connected to ExecutedTrades: " + self.HubConnection.id);
                }).fail(function () { console.log("Failed"); });
            }
            ;
            TransactionController.prototype.PushTransactionToList = function (transaction) {
                if (this.transactionList.length > 8) {
                    this.transactionList.splice(this.transactionList.length - 1, 1);
                }
                this.transactionList.unshift(transaction);
                this.$scope.$apply();
            };
            TransactionController.$inject = ["$scope", "$http"];
            return TransactionController;
        }());
        Controllers.TransactionController = TransactionController;
        var SignalRHubConnectionOptions = (function () {
            function SignalRHubConnectionOptions() {
                this.jsonp = true;
            }
            return SignalRHubConnectionOptions;
        }());
        angular.module("main").controller("TransactionController", TransactionController);
    })(Controllers = GFTMarket.Controllers || (GFTMarket.Controllers = {}));
})(GFTMarket || (GFTMarket = {}));
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
/// <reference path="modules/mainmodule.ts" />
/// <reference path="controllers/ordercontroller.ts" />
/// <reference path="services/transactionhandlerservice.ts" />
/// <reference path="services/orderhandlerservice.ts" />
/// <reference path="directives/orderdirective.ts" />
/// <reference path="directives/transactiondirective.ts" />
/// <reference path="controllers/transactioncontroller.ts" />
/// <reference path="controllers/webclientcontroller.ts" />
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
/// <reference path="../../_references.ts" />
/// <reference path="../_references.ts" />
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
    })(Models = GFTMarket.Models || (GFTMarket.Models = {}));
})(GFTMarket || (GFTMarket = {}));
/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Models;
    (function (Models) {
        var Transaction = (function () {
            function Transaction() {
            }
            return Transaction;
        }());
        Models.Transaction = Transaction;
    })(Models = GFTMarket.Models || (GFTMarket.Models = {}));
})(GFTMarket || (GFTMarket = {}));
/// <reference path="../scripts/typings/signalr/signalr.d.ts" />
/// <reference path="../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../scripts/typings/angularjs/angular-animate.d.ts" />
/// <reference path="../scripts/typings/angularjs/angular-component-router.d.ts" />
/// <reference path="../scripts/typings/angularjs/angular-cookies.d.ts" />
/// <reference path="../scripts/typings/angularjs/angular-mocks.d.ts" />
/// <reference path="../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="../scripts/typings/angularjs/angular-sanitize.d.ts" />
/// <reference path="../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="modules/mainmodule.ts" />
/// <reference path="models/interfaces/imarketobject.ts" />
/// <reference path="models/item.ts" />
/// <reference path="models/transaction.ts" />
/// <reference path="services/transactionhandlerservice.ts" />
/// <reference path="services/orderhandlerservice.ts" />
/// <reference path="directives/transactiondirective.ts" />
/// <reference path="directives/orderdirective.ts" />
/// <reference path="controllers/transactioncontroller.ts" />
/// <reference path="controllers/ordercontroller.ts" />
/// <reference path="controllers/webclientcontroller.ts" />
/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Models;
    (function (Models) {
        var Order = (function () {
            function Order() {
            }
            return Order;
        }());
        Models.Order = Order;
    })(Models = GFTMarket.Models || (GFTMarket.Models = {}));
})(GFTMarket || (GFTMarket = {}));
/// <reference path="../_references.ts" />
var GFTMarket;
(function (GFTMarket) {
    var Models;
    (function (Models) {
        var WebClient = (function () {
            function WebClient() {
            }
            return WebClient;
        }());
        Models.WebClient = WebClient;
    })(Models = GFTMarket.Models || (GFTMarket.Models = {}));
})(GFTMarket || (GFTMarket = {}));
//# sourceMappingURL=app.js.map