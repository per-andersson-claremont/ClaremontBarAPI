/************************************************
 * Product Edit Controller
 * View, Edit, Create
 */

(function () {
    'use strict';
    
    angular.module("productManagement")
    .controller("ProductEditCtrl", ["productResource", "$state", "$stateParams", ProductEditCtrl]);


    //Product Edit Controller
    function ProductEditCtrl(productResource, $state, $stateParams) {
        var vm = this;
        vm.product = {};
        vm.message = "";

        // GET by id
        productResource.get({ id: $stateParams.id }, 
            function (data) {
                vm.product = data;

            }, 
            function (response) {
                vm.message = response.statusText + "\r\n";
                if (response.data.exceptionMessage)
                    vm.message += response.data.exceptionMessage;
            });

        if (vm.product && vm.product.id) {
            vm.title = "Edit: " + vm.product.productName;
        }
        else {
            vm.title = "New Product";
        }

        // Save
        vm.submit = function () {
            vm.message = '';

            if (vm.product.id) {

                // PUT
                vm.product.$update({ id: vm.product.id },
                    function (data) {
                        vm.message = "Save Complete";
                        vm.product = data;
                    },
                    function (response) {
                        vm.message = response.statusText + "\r\n";
                        if (response.data.modelState) {
                            for (var key in resp.data.modelState)
                                vm.message += response.modelState[key] + "\r\n";
                        }
                    });
            }
            else {

                // POST
                vm.product.$save(
                    function (data) {
                       // vm.message = "Save Complete";
                        $state.go('products'); //'app/products/productListView.html';
                    },
                    function (response) {
                        vm.message = response.statusText + "\r\n";
                        if (response.data.modelState) {
                            for (var key in resp.data.modelState)
                                vm.message += response.modelState[key] + "\r\n";
                        }
                    });
            }
        };

        vm.cancel = function () {
            $state.go('products'); //'app/products/productListView.html';
        };
    };

}());