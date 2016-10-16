(function () {
    var IRDsDataSource = {
        type: "json",
        transport: {
            read: {//TODO: Refactor hardcoded url
                url: "http://10.209.10.10/Api/IRDs",
                dataType: "json",
                type: "GET"
            }
        }
    }

    var ApplyDownlinkChannelDialogCtrl = function ($scope,$http, $modalInstance, dwlChannel, dataservice) {
        $scope.dwlChannel = dwlChannel;
        $scope.promise = null;
        $scope.isCollapsed = true;

        $scope.AntennasDataSource = [];
        $scope.selectedAntennaType = "";

        //TODO: Refactor hardcoded urls
        $http.get("http://10.209.10.10/Api/Enums?enumtype=PolarisationEnum&val=" + dwlChannel.polarisation).success(function (result) {
            $scope.PolarisationEnum = result;
        });
        $http.get("http://10.209.10.10/Api/Enums?enumtype=FECEnum&val=" + dwlChannel.fec).success(function (result) {
            $scope.FECEnum = result;
        });
        $http.get("http://10.209.10.10/Api/Enums?enumtype=RollOffEnum&val=" + dwlChannel.rollOff).success(function (result) {
            $scope.RollOffEnum = result;
        });
        $http.get("http://10.209.10.10/Api/Enums?enumtype=SDHD&val=" + dwlChannel.sdhd).success(function (result) {
            $scope.SDHD = result;
        });
        $http.get("http://10.209.10.10/Api/Enums?enumtype=ModulationEnum&val=" + dwlChannel.modulation).success(function (result) {
            $scope.ModulationEnum = result;
        });

        $http.get("http://10.209.10.10/Api/IRDs").success(function (result) {
            $scope.IRDsDataSource = result;
        });

        $http.get("http://10.209.10.10/Api/BISSCodes").success(function (result) {
            $scope.BISSCodesDataSource = result;
        });
        $http.get("http://10.209.10.10/Api/EXTCards").success(function (result) {
            $scope.EXTCardsDataSource = result;
        });
        $http.get("http://10.209.10.10/Api/SteerableAntennas").success(function (result) {
            $scope.steerableAntennas = result;
        });

        $scope.setAntenaType = function (message, event) {
            var fixedAntenas = message.satellitePosition.fixedAntennas;
            if (!(fixedAntenas === undefined || fixedAntenas.length == 0)) {
                $scope.selectedAntennaType = "Fixed";
                $scope.AntennasDataSource = dwlChannel.satellite.satellitePosition.fixedAntennas;
            }
            else {
                $scope.selectedAntennaType = "Steerable";
                //TODO: Refactor hardcoded url
                $http.get("http://10.209.10.10/Api/SteerableAntennas").success(function (result) {
                    $scope.AntennasDataSource = result;
                });
            }
        }

        $scope.setAntenaDataSource = function (e) {
            var selectedType = e.sender.value();
            if (selectedType == undefined)
                return;
            if (selectedType === "Fixed") {
                $scope.AntennasDataSource = dwlChannel.satellite.satellitePosition.fixedAntennas;
            }
            else if (selectedType === "Steerable") {
                $scope.AntennasDataSource = $scope.steerableAntennas;
            }
        }

        $scope.apply = function () {
            var validator = $("#validator").data("kendoValidator");
            if (validator.validate()) {

                var irdId = $("#selectIRD").data("kendoComboBox").value();
                var antennaType = $("#selectAntennaType").data("kendoComboBox").value();
                var antennaid = $("#selectAntenna").data("kendoComboBox").value();
                var bissId = $("#selectBiss").data("kendoComboBox").value();
                var extId = $("#selectEXT").data("kendoComboBox").value();

                $scope.promise = dataservice.applyDownlinkPreset({
                    "ChannelId": $scope.dwlChannel.id,
                    "irdId": irdId,
                    "antennaType": antennaType,
                    "antennaid": antennaid,
                    "bissId": bissId,
                    "extId": extId
                }).success(function (data) {
                        $scope.applystatus = data;
                }).error(function(data, status) {
                    $scope.applystatus = data.exceptionMessage;
                });
                //$modalInstance.close();
            }
        };

        $scope.cancel = function () {
            $modalInstance.dismiss("cancel");
        };
        


        

    };



    angular.module("TV2Presets2.ctrl.downlink.dialog", ["customServices", "cgBusy", "ngAnimate"])
        .controller("ApplyDownlinkChannelDialogCtrl", ["$scope", "$http", "$modalInstance", "dwlChannel", "dataservice", ApplyDownlinkChannelDialogCtrl]);
}());