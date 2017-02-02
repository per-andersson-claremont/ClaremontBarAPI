/************************************************************************/
/* common.services
 * A separate module for calling the WEB API 
 * A local server path should be replaced by the path to production server
 */

(function () {
    'use strict';
 

    //serverPath: "http://localhost:53832/"

    angular.module("common.services", ["ngResource"])
        .constant("appSettings", {
            serverPath: "http://claremontbarwebapi.azurewebsites.net/"
    });

}());