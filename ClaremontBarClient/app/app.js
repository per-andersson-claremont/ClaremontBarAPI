/**********************************************
 * Angular Module
 * Product Management
 * Claremont AB
 */

(function () {
    'use strict';

    var app = angular.module("productManagement", ["common.services", "ui.router"]);

    app.config(["$stateProvider", "$urlRouterProvider",
        function ($stateProvider, $urlRouterProvider) {

            $urlRouterProvider.otherwise("/products");

            // Routing for:
            // Product List View
            // Product Edit View
            $stateProvider
                .state("products",
                {
                    url: "/products",
                    templateUrl: "app/products/productListView.html",
                    controller: "ProductListCtrl as vm"
                })
                .state("productEdit",
                {
                    url: "/products/edit/:id",
                    templateUrl: "app/products/productEditView.html",
                    controller: "ProductEditCtrl as vm"
                })

        }]);
}());