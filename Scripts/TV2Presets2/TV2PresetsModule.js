(function () {
    // this is definition of application module
    var app = angular.module("TV2Presets2", [
        "ngRoute",
        "TV2Presets2.ctrl.start",
        "TV2Presets2.ctrl.downlink",
        "TV2Presets2.ctrl.uplink",
        "TV2Presets2.ctrl.setup",
        "kendo.directives",
        "ui.bootstrap"
    ]);

    // define client side navigation
    app.config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "/Home/Start",
                controller: "StartPageCtrl"
            })
            .when("/Downlink", {
                templateUrl: "/Home/Downlink",
                controller: "DownlinkPageCtrl"
            })
            .when("/Uplink", {
                templateUrl: "/Home/Uplink",
                controller: "UplinkPageCtrl"
            })
            .when("/Setup", {
                templateUrl: "/Home/Setup",
                controller: "SetupPageCtrl"
            })
            .otherwise({
                redirectTo: "/"
            });

        $locationProvider.html5Mode(false);//.hashPrefix('!');
    }]);

}());