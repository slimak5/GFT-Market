﻿/// <reference path="../_references.ts" />

namespace GFTMarket.Services
{
    export class OrderHandlerService
    {
        orderList: Array<GFTMarket.Models.Order> = [];
        $http: ng.IHttpService;
        public ClientId: number;

        static $inject = ["$http"];
        constructor($http: ng.IHttpService)
        {
            this.$http = $http;
        }

        public PushOrderToList(object: GFTMarket.Models.Order)
        {
            this.orderList.unshift(object);
        }

        public CleanOrderList()
        {
            this.orderList = [];
        }

        //API CALLS:
        public SendBuyRequest(object: GFTMarket.Models.Order)
        {
            object.orderType = "BUY";
            object.clientId = this.ClientId;
            var self = this;

            this.$http.get("http://localhost:54919/api/Orders/GenerateOrderId")
                .then(function (response)
                {
                    object.orderId = <number>response.data;
                    console.log("BUY ID Get:" + object.orderId);

                    self.$http.post("http://localhost:54919/api/Orders/SendBuyOrder", object)
                        .then(function (res)
                        {
                            toastr.success(<string>res.data);
                        });
                });
        }

        public SendSellRequest(object: GFTMarket.Models.Order)
        {
            object.orderType = "SELL";
            object.clientId = this.ClientId;
            var self = this;
            this.$http.get("http://localhost:54919/api/Orders/GenerateOrderId")
                .then(function (response)
                {
                    object.orderId = <number>response.data;
                    console.log("SELL ID Get:" + object.orderId);
                    self.$http.post("http://localhost:54919/api/Orders/SendSellOrder", object)
                        .then(function (res)
                        {
                            toastr.success(<string>res.data);
                        });
                });
        }

        public GetAvaibleItems()
        {
            var self = this;
            this.$http.get("http://localhost:54919/api/Orders/GetItems/")
                .then(function (response: ng.IHttpPromiseCallbackArg<Array<Models.Order>>)
                {
                    self.CleanOrderList();
                    for (let i = 0; i < response.data.length; i++)
                    {
                        self.PushOrderToList(<Models.Order>response.data[i]);
                    }
                });
        }
    }

    angular.module("main").service("OrderHandlerService", OrderHandlerService);
}