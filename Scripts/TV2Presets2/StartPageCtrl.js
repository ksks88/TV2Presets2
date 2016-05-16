(function() {
   
    var StartPageCtrl = function($scope) {
        $scope.message = "Hello, i am on Start Page!";
    }

    angular.module("TV2Presets2.ctrl.start", []).controller("StartPageCtrl", ["$scope", StartPageCtrl]);
}());