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
            //public remove(object: GFTMarket.Models.Item) {
            //    for (let i = 0; i < this.orderList.length; i++) {
            //        if (this.orderList[i].quantity <= 0) {
            //            this.orderList.splice(i, 1);
            //            i = -1;
            //        } else {
            //            if (this.orderList[i].id == object.id && this.orderList[i].name == object.name) {
            //                this.orderList.splice(i, 1);
            //                i = -1;
            //            }
            //        }
            //    }
            //}
            //public getById(id: number) {
            //    return this.orderList[id];
            //}
            //public getByObject(object: GFTMarket.Models.Order) {
            //    for (let i = 0; i < this.orderList.length; i++) {
            //        if (this.orderList[i].id == object.id && this.orderList[i].name == object.name) {
            //            console.log(this.orderList[i]);
            //            return this.orderList[i];
            //        }
            //    }
            //    console.log("getByObject(): returned empty instance");
            //    return new GFTMarket.Models.Order();
            //}
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
                console.log("method finished");
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
//# sourceMappingURL=orderhandlerservice.js.map