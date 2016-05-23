(function () {

    var satellitePositionDropDownEditor = function (container, options) {
        $("<input required data-text-field='name' data-value-field='id' data-bind='value: " + options.field + "'/>")
            .appendTo(container)
            .kendoComboBox({
                autoBind: false,
                dataSource: {
                    transport: {
                        read: {
                            url: "http://localhost:49423/Api/SatellitePositions",
                            dataType: "json",
                            type: "GET"
                        }
                    }
                }
            });
    }

    var steerableAntennasOptions = {
        selectable: "row",
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "http://localhost:49423/Api/SteerableAntennas",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: function (data) {
                        return "http://localhost:49423/Api/SteerableAntennas/" + data.id;
                    },
                    dataType: "json",
                    type: "PUT"
                },
                destroy: {
                    url: function (data) {
                        return "http://localhost:49423/Api/SteerableAntennas/" + data.id;
                    },
                    dataType: "json",
                    type: "DELETE"
                },
                create: {
                    url: "http://localhost:49423/Api/SteerableAntennas",
                    dataType: "json",
                    type: "POST"
                }
            },
            schema: {
                model: {
                    id: "id",
                    fields: {
                        id: {
                            editable: false,
                            nullable: false,
                            type: "number"
                        },
                        name: {
                            validation: {
                                //set validation rules
                                required: true
                            }
                        },
                        azimuth: {},
                        elevation: {},
                        polarization: {},
                        size: {},
                        comment: {},
                        xHighInput: { type: "number", min: 1, max: 64 },
                        xLowInput: { type: "number", min: 1, max: 64 },
                        yHighInput: { type: "number", min: 1, max: 64 },
                        yLowInput: { type: "number", min: 1, max: 64 },
                        xHighFreq: {},
                        xLowFreq: {},
                        yHighFreq: {},
                        yLowFreq: {}
                    }
                }
            },
            pageSize: 25,
            error: function (e) {
                $("#steerable-antenas").data("kendoGrid").dataSource.cancelChanges();
                var notificationElement = $("#notif").data("kendoNotification");
                notificationElement.show("Status: " + e.status + "; Error message: " + e.xhr.responseText, "error");
            }
        },
        columns: [
            { field: "id", title: "ID" },
            { field: "name", title: "antenna name" },
            { field: "azimuth", title: "azimuth" },
            { field: "elevation", title: "elevation" },
            { field: "polarization", title: "polarization" },
            { field: "size", title: "Size" },
            { field: "xHighInput", title: "X High Input", hidden: false, format: "{0:#}" },
            { field: "xLowInput", title: "X Low Input", hidden: false, format: "{0:#}" },
            { field: "yHighInput", title: "Y High Input", hidden: false, format: "{0:#}" },
            { field: "yLowInput", title: "Y Low Input", hidden: false, format: "{0:#}" },
            { field: "xHighFreq", title: "X High Freq", hidden: false, format: "{0:#.000 MHz}" },
            { field: "xLowFreq", title: "X Low Freq", hidden: false, format: "{0:#.000 MHz}" },
            { field: "yHighFreq", title: "Y High Freq", hidden: false, format: "{0:#.000 MHz}" },
            { field: "yLowFreq", title: "Y Low Freq", hidden: false, format: "{0:#.000 MHz}" },
            { field: "comment", title: "Comment", hidden: false }
        ],
        scrollable: false,
        pageable: {
            refresh: true,
            pageSizes: false,
            buttonCount: 5
        },

    };


    var irdTypeEditor = function (container, options) {
        $("<input required data-text-field='text' data-value-field='value' data-bind='value: " + options.field + "'/>")
            .appendTo(container)
            .kendoComboBox({
                autoBind: false,
                dataSource: {
                    transport: {
                        read: {
                            url: "http://localhost:49423/Api/Enums?enumtype=IRDTypes",
                            dataType: "json",
                            type: "GET"
                        }
                    }
                }
            });
    }

    var bissTypeEditor = function (container, options) {
        $("<input required data-text-field='text' data-value-field='value' data-bind='value: " + options.field + "'/>")
            .appendTo(container)
            .kendoComboBox({
                autoBind: false,
                dataSource: {
                    transport: {
                        read: {
                            url: "http://localhost:49423/Api/Enums?enumtype=BISSTypeEnum",
                            dataType: "json",
                            type: "GET"
                        }
                    }
                }
            });
    }

    var ipAddressEditor = function (container, options) {
        $("<input class='k-input k-textbox' required data-bind='value: " + options.field + "' />")
            .appendTo(container)
            .mask("0ZZ.0ZZ.0ZZ.0ZZ", {
                translation: {
                    'Z': {
                        pattern: /[0-9]/, optional: true
                    }
                }
            });
    }

    var bissCodeValidateKey = function (input) {
        var tr = $(input).closest('tr');
        console.log($("span[ng-bind='dataItem.name']", tr).text() + " & " + input.val());

        var keyName = $("span[ng-bind='dataItem.name']", tr).text().toUpperCase();

        var length = 0;
        if (keyName === "BISS-1") {
            length = 12;
        }
        else if (keyName === "BISS-E") {
            length = 16;
        }
        else if (keyName === "RAS") {
            length = 7;
        }

        var numberOfChars = input.val().length;
        if (input.is("[name='bissKey']") && ((keyName == "BISS-1" && numberOfChars > length) || (keyName == "BISS-E" && numberOfChars > length) || (keyName == "RAS" && numberOfChars > length))) {
            input.attr("data-bisskeyvalidation-msg", keyName + " key cannot be more then " + length + " characters.");
            return false;
        }

        return true;
    }

    var bissCodeValidateName = function (input) {
        var tr = $(input).closest('tr');
        console.log($("span[ng-bind='dataItem.bissKey']", tr).text() + " & " + input.val());

        var numberOfChars = $("span[ng-bind='dataItem.bissKey']", tr).text().length;

        var keyName = input.val().toUpperCase();

        var length = 0;
        if (keyName === "BISS-1") {
            length = 12;
        }
        else if (keyName === "BISS-E") {
            length = 16;
        }
        else if (keyName === "RAS") {
            length = 7;
        }

        if (input.is("[name='name']") && ((keyName == "BISS-1" && numberOfChars > length) || (keyName == "BISS-E" && numberOfChars > length) || (keyName == "RAS" && numberOfChars > length))) {
            input.attr("data-namevalidation-msg", keyName + " key cannot be more then " + length + " characters.");
            return false;
        }

        return true;
    }

    var bissCodesOptions = {
        selectable: "row",
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "http://localhost:49423/Api/BISSCodes",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: function (data) {
                        return "http://localhost:49423/Api/BISSCodes/" + data.id;
                    },
                    dataType: "json",
                    type: "PUT"
                },
                destroy: {
                    url: function (data) {
                        return "http://localhost:49423/Api/BISSCodes/" + data.id;
                    },
                    dataType: "json",
                    type: "DELETE"
                },
                create: {
                    url: "http://localhost:49423/Api/BISSCodes",
                    dataType: "json",
                    type: "POST"
                }
            },
            schema: {
                model: {
                    id: "id",
                    fields: {
                        id: {
                            editable: false,
                            nullable: false,
                            type: "number"
                        },
                        name: {
                            validation: {
                                //set validation rules
                                required: true,

                                namevalidation: bissCodeValidateName
                            }
                        },
                        bissType: {
                            validation: {
                                //set validation rules
                                required: true
                            }
                        },
                        bissKey: {
                            validation: {
                                //set validation rules
                                required: true,
                                
                                bisskeyvalidation: bissCodeValidateKey
                            }
                        }
                    }
                }
            },
            pageSize: 25,
            error: function (e) {
                $("#biss-codes").data("kendoGrid").dataSource.cancelChanges();
                var notificationElement = $("#notif").data("kendoNotification");
                notificationElement.show("Status: " + e.status + "; Error message: " + e.xhr.responseText, "error");
            }
        },
        columns: [
            { field: "id", title: "ID" },
            { field: "name", title: "Name" },
            {
                field: "bissType", title: "BISS Type", width: "200px", editor: bissTypeEditor, template: function (dataItem) {
                    if (isNaN(dataItem.bisType))
                        dataItem.bisType = 0;

                    var jsonObjectInstance = $.parseJSON(
                            $.ajax(
                                {
                                    url: "http://localhost:49423/Api/Enums?enumtype=BISSTypeEnum&val=" + dataItem.bisType,
                                    async: false,
                                    dataType: 'json'
                                }
                            ).responseText
                        );

                    return jsonObjectInstance;
                }
            },
            { field: "bissKey", title: "Key" }
        ],
        scrollable: false,
        pageable: {
            refresh: true,
            pageSizes: false,
            buttonCount: 5
        },

    };


    // ***************************  CONTROLLER ********************************
    var BisscodesPageCtrl = function ($scope) {
        $scope.steerableAntennasOptions = steerableAntennasOptions;
        $scope.bissCodesOptions = bissCodesOptions;
    }

    var myControllerModule = angular.module("TV2Presets2.ctrl.bisscodes", []);
    myControllerModule.controller("BisscodesPageCtrl", ["$scope", BisscodesPageCtrl]);
}());