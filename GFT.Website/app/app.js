var GFTMarket;
(function (GFTMarket) {
    var Controllers;
    (function (Controllers) {
        var SignalRHubConnectionOptions = (function () {
            function SignalRHubConnectionOptions() {
                this.jsonp = true;
            }
            return SignalRHubConnectionOptions;
        }());
        var TransactionController = (function () {
            function TransactionController($scope, TransactionHandlerService, $http) {
                var _this = this;
                this.TransactionHandlerService = TransactionHandlerService;
                this.$http = $http;
                this.$scope = $scope;
                this.hubConnection = $.hubConnection("http://localhost:53008");
                this.hubProxy = this.hubConnection.createHubProxy("Transactions");
                this.hubProxy.on("SendFeed", function (transaction) {
                    _this.TransactionHandlerService.PushTransactionToList(transaction);
                });
                this.hubConnection.start(new SignalRHubConnectionOptions());
            }
            TransactionController.$inject = ["$scope", "TransactionHandlerService", "$http"];
            return TransactionController;
        }());
        Controllers.TransactionController = TransactionController;
        angular.module("main").controller("TransactionController", TransactionController);
    })(Controllers = GFTMarket.Controllers || (GFTMarket.Controllers = {}));
})(GFTMarket || (GFTMarket = {}));
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
var GFTMarket;
(function (GFTMarket) {
    var Controllers;
    (function (Controllers) {
        var WebClientController = (function () {
            function WebClientController($scope) {
                this.webClient = new GFTMarket.Models.WebClient();
                this.$scope = $scope;
                this.webClient.clientId = this.GetClientId();
            }
            WebClientController.prototype.GetClientId = function () {
                $.get("http://localhost:54919/api/Webclient/GenerateWebClientId/", function (response) {
                    return response.responseBody;
                });
                return -1;
            };
            WebClientController.$inject = ['$scope'];
            return WebClientController;
        }());
        Controllers.WebClientController = WebClientController;
        angular.module("main").controller("WebClientController", WebClientController);
    })(Controllers = GFTMarket.Controllers || (GFTMarket.Controllers = {}));
})(GFTMarket || (GFTMarket = {}));
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
        var TransactionHandlerService = (function () {
            function TransactionHandlerService($http) {
                this.transactionList = [];
                this.activeTransaction = new GFTMarket.Models.Transaction();
                this.$http = $http;
            }
            TransactionHandlerService.prototype.PushTransactionToList = function (object) {
                if (this.transactionList.length > 8) {
                    this.transactionList.splice(this.transactionList.length - 1, 1);
                }
                this.transactionList.unshift(object);
            };
            TransactionHandlerService.prototype.CleanTransactionList = function () {
                this.transactionList = [];
            };
            TransactionHandlerService.prototype.GetNewestTransactions = function () {
                var self = this;
                this.$http.get("http://localhost:54919/api/Feeds/getFeeds/").then(function (response) {
                    self.CleanTransactionList();
                    for (var i = 0; i < response.data.length; i++) {
                        self.PushTransactionToList(response.data[i]);
                    }
                });
            };
            TransactionHandlerService.$inject = ['$http'];
            return TransactionHandlerService;
        }());
        Services.TransactionHandlerService = TransactionHandlerService;
        angular.module("main").service("TransactionHandlerService", TransactionHandlerService);
    })(Services = GFTMarket.Services || (GFTMarket.Services = {}));
})(GFTMarket || (GFTMarket = {}));
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
            OrderHandlerService.prototype.SendBuyRequest = function (object) {
                this.$http.post("http://localhost:54919/api/Items/buyItem", object).then(function (res) {
                    toastr.success(res.data);
                });
            };
            OrderHandlerService.prototype.SendSellRequest = function (object) {
                this.$http.post("http://localhost:54919/api/Items/sellItem", object).then(function (res) {
                    toastr.success(res.data);
                });
            };
            OrderHandlerService.prototype.GetAvaibleItems = function () {
                var self = this;
                this.$http.get("http://localhost:54919/api/Orders/getItems/").then(function (response) {
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
//# sourceMappingURL=app.js.map