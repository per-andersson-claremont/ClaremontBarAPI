/***************************************************
 * Product List Controller
 * View Products, Delete Product
 */
(function () {
    'use strict';

    angular.module("productManagement")
        .controller("ProductListCtrl", ["productResource", ProductListCtrl]);

    function ProductListCtrl(productResource) {
        var vm = this;
        vm.message = "";

        // inits the list view with a json array of products
        productResource.query(function (data) {
            vm.products = data;
        });

        // handles delete functionality in the product list
        vm.delete = function (productId) {
            vm.message = '';

            if (productId)
            {
                productResource.delete({ id: productId },
                    function () {
                        vm.message = "Product deleted";

                        // refresh list
                        productResource.query(function (data) {
                            vm.products = data;
                        });

                    },
                    // errors
                    function (response) {
                        vm.message = response.statusText + "\r\n";
                        if (response.data.modelState) {
                            for (var key in resp.data.modelState)
                                vm.message += response.modelState[key] + "\r\n";
                        }
                    }
                );
            }
        };
    }
})();
