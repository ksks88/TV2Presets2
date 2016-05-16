(function() {
    var serviceModule = angular.module("customServices", []);

    var TV2DataServices = function($http) {
        var applyDownlinkPreset = function(presetdata) {
            return $http.post("http://localhost:49423/Api/TV2DataServices", presetdata);
        };

        


        return {
            applyDownlinkPreset: applyDownlinkPreset
    };
    };


    serviceModule.factory("dataservice", ["$http", TV2DataServices]);


}());