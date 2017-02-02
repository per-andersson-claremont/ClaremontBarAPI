/*****************************************************************************
 * Product Resource
 * Web API Resource
 * 
 */

(function () {
    'use strict';
    
    angular.module("common.services")
        .factory("productResource", ["$resource",
                                     "appSettings",
                                      productResource]);

    // Add Web API methods for products
    function productResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/products/:id", null, 
        {
            'update': { method: 'PUT' }
        })
    };

}());