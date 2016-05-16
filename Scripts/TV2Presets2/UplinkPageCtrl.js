(function () {

    var UplinkChannelsCtrl = function ($scope) {
        $scope.message = "UPLINK PAGE";
    }

    var myControllerModule = angular.module("TV2Presets2.ctrl.uplink", []);
    myControllerModule.controller("UplinkPageCtrl", ["$scope", UplinkChannelsCtrl]);
}());