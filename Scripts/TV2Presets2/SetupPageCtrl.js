(function () {
    //TODO: Refactor hardcoded url
    var satellitePositionsOptions = {
        selectable: "row",
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "http://10.209.10.10/Api/SatellitePositions",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: function(data) {
                        return "http://10.209.10.10/Api/SatellitePositions/" + data.id;
                    },
                    dataType: "json",
                    type: "PUT"
                },
                destroy: {
                    url: function(data) {
                        return "http://10.209.10.10/Api/SatellitePositions/" + data.id;
                    },
                    dataType: "json",
                    type: "DELETE"
                },
                create: {
                    url: "http://10.209.10.10/Api/SatellitePositions",
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
                        }
                }
                }
            },
            pageSize: 25,
            error: function (e) {
                $("#satellitePosition").data("kendoGrid").dataSource.cancelChanges();
                var notificationElement = $("#notif").data("kendoNotification");
                notificationElement.show("Status: " + e.status + "; Error message: " + e.xhr.responseText , "error");
            }
        },
        columns: [
            {
                field: "id",
                title: "ID"
                //width : "50px"
            },
            {
                field: "name",
                title: "Position name"
                //width: "100px"
            },
            {
                command: [{ name: "edit", text: "" }, { name: "destroy", text: "" }],
                title: "action"

            }
        ],
        scrollable: false,
        pageable: {
            refresh: true,
            pageSizes: false,
            buttonCount: 5
        },
        
    };
    //TODO: Refactor hardcoded url
    var satellitePositionDropDownEditor = function (container, options) {
        $("<input required data-text-field='name' data-value-field='id' data-bind='value: " + options.field + "'/>")
            .appendTo(container)
            .kendoComboBox({
                autoBind: false,
                dataSource: {
                    transport: {
                        read: {
                            url: "http://10.209.10.10/Api/SatellitePositions",
                            dataType: "json",
                            type: "GET"
                        }
                    }
                }
            });
    }

    //TODO: Refactor hardcoded url
    var satellitesOptions = {
        selectable: "row",
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "http://10.209.10.10/Api/Satellites",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: function(data) {
                        return "http://10.209.10.10/Api/Satellites/" + data.id;
                    },
                    dataType: "json",
                    type: "PUT",
                    complete : function (data) {
                        $("#satellites").data("kendoGrid").dataSource.read();
                    }
                },
                destroy: {
                    url: function(data) {
                        return "http://10.209.10.10/Api/Satellites/" + data.id;
                    },
                    dataType: "json",
                    type: "DELETE"
                },
                create: {
                    url: "http://10.209.10.10/Api/Satellites",
                    dataType: "json",
                    type: "POST",
                    complete: function (data) {
                        $("#satellites").data("kendoGrid").dataSource.read();
                    }
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
                        satellitePositionId: {
                            editable: true,
                            nullable: false,
                            validation: {
                                required: true
                            }
                        },
                        name: {
                            validation: {
                                required: true
                            },
                            defaultValue : "NEW SATELLITE"
                        }

                    }
                }
            },
            pageSize: 25,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
            error: function (e) {
                /* the e event argument will represent the following object:
        
                {
                    errorThrown: "Unauthorized",
                    sender: {... the Kendo UI DataSource instance ...}
                    status: "error"
                    xhr: {... the Ajax request object ...}
                }
        
                */
                $("#satellites").data("kendoGrid").dataSource.cancelChanges();
                var notificationElement = $("#notif").data("kendoNotification");
                notificationElement.show("Status: " + e.status + "; Error message: " + e.errorThrown, "error");
            }
        },
        columns: [
            { field: "id", title: "ID" },
            { field: "name", title: "satellite name" },
            {
                field: "satellitePositionId",
                title: "position",
                hidden: false,
                editor: satellitePositionDropDownEditor,
                template: "#=satellitePosition != null ? satellitePosition.name : ''#"
            },
            { command: [{ name: "edit", text: "" }, { name: "destroy", text: "" }], title: "action" }],
        scrollable: false,
        pageable: {
            refresh: true,
            pageSizes: false,
            buttonCount: 5
        },
        
    }
    //TODO: Refactor hardcoded url
    var fixedAntennasOptions = {
        selectable: "row",
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "http://10.209.10.10/Api/FixedAntennas",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: function (data) {
                        return "http://10.209.10.10/Api/FixedAntennas/" + data.id;
                    },
                    dataType: "json",
                    type: "PUT",
                    complete: function (data) {
                        $("#fixedAntennas").data("kendoGrid").dataSource.read();
                    }
                },
                destroy: {
                    url: function (data) {
                        return "http://10.209.10.10/Api/FixedAntennas/" + data.id;
                    },
                    dataType: "json",
                    type: "DELETE"
                },
                create: {
                    url: "http://10.209.10.10/Api/FixedAntennas",
                    dataType: "json",
                    type: "POST",
                    complete: function (data) {
                        $("#fixedAntennas").data("kendoGrid").dataSource.read();
                    }
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
                                required: true
                            }
                        },
                        size: {
                            validation: {
                                required: true
                            }
                        },
                        comment: {
                            validation: {
                                required: false
                            }
                        },
                        xHighInput: {
                            type: "number",
                            validation: {
                                required: false
                            }
                        },
                        xLowInput: {
                            type: "number",
                            validation: {
                                required: false
                            }
                        },
                        yHighInput: {
                            type: "number",
                            validation: {
                                required: false
                            }
                        },
                        yLowInput: {
                            type: "number",
                            validation: {
                                required: false
                            }
                        },
                        xHighFreq: {
                            type: "number",
                            validation: {
                                required: false
                            }
                        },
                        xLowFreq: {
                            type: "number",
                            validation: {
                                required: false
                            }
                        },
                        yHighFreq: {
                            type: "number",
                            validation: {
                                required: false
                            }
                        },
                        yLowFreq: {
                            type: "number",
                            validation: {
                                required: false
                            }
                        },
                        satellitePositionId: {
                            editable: true,
                            nullable: false,
                            validation: {
                                required: true
                            }
                        }
                    }
                }
            },
            pageSize: 25,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
            error: function (e) {
                /* the e event argument will represent the following object:
        
                {
                    errorThrown: "Unauthorized",
                    sender: {... the Kendo UI DataSource instance ...}
                    status: "error"
                    xhr: {... the Ajax request object ...}
                }
        
                */
                $("#fixedAntennas").data("kendoGrid").dataSource.cancelChanges();
                var notificationElement = $("#notif").data("kendoNotification");
                notificationElement.show("Status: " + e.status + "; Error message: " + e.errorThrown, "error");
            }
        },
        columns: [
            { field: "id", title: "ID", width : "50px" },
            { field: "name", title: "antenna name", width : "300px" },
            {
                 field: "satellitePositionId",
                 title: "position",
                 hidden: false,
                 editor: satellitePositionDropDownEditor,
                 template: "#=satellitePosition != null ? satellitePosition.name : ''#"
            },
            { field: "size", title: "Size" },
            { field: "xHighInput", title: "X High Input", hidden : true, format :"{0:#}" },
            { field: "xLowInput", title: "X Low Input", hidden: true, format: "{0:#}" },
            { field: "yHighInput", title: "Y High Input", hidden: true, format: "{0:#}" },
            { field: "yLowInput", title: "Y Low Input", hidden: true, format: "{0:#}" },
            { field: "xHighFreq", title: "X High Freq", hidden: true, format: "{0:#.000 MHz}" },
            { field: "xLowFreq", title: "X Low Freq", hidden: true, format: "{0:#.000 MHz}" },
            { field: "yHighFreq", title: "Y High Freq", hidden: true, format: "{0:#.000 MHz}" },
            { field: "yLowFreq", title: "Y Low Freq", hidden: true, format: "{0:#.000 MHz}" },
            { field: "comment", title: "Comment", hidden: true },
            {
                command: [{ name: "edit", text: "" }, { name: "destroy", text: "" }],
                title: "action"
            }
        ],
        scrollable: true,
        pageable: {
            refresh: true,
            pageSizes: false,
            buttonCount: 5
        },
        
    }
    //TODO: Refactor hardcoded url
    var steerableAntennasOptions = {
        selectable: "row",
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "http://10.209.10.10/Api/SteerableAntennas",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: function (data) {
                        return "http://10.209.10.10/Api/SteerableAntennas/" + data.id;
                    },
                    dataType: "json",
                    type: "PUT"
                },
                destroy: {
                    url: function (data) {
                        return "http://10.209.10.10/Api/SteerableAntennas/" + data.id;
                    },
                    dataType: "json",
                    type: "DELETE"
                },
                create: {
                    url: "http://10.209.10.10/Api/SteerableAntennas",
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
                        yLowFreq : {}
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

    //TODO: Refactor hardcoded url
    var irdTypeEditor = function(container, options) {
        $("<input required data-text-field='text' data-value-field='value' data-bind='value: " + options.field + "'/>")
            .appendTo(container)
            .kendoComboBox({
                autoBind: false,
                dataSource: {
                    transport: {
                        read : {
                            url: "http://10.209.10.10/Api/Enums?enumtype=IRDTypes",
                            dataType: "json",
                            type: "GET"
                        }
                    }
                }
        });
    }

    var ipAddressEditor = function(container, options) {
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
    //TODO: Refactor hardcoded url
    var irdsOptions = {
        selectable: "row",
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "http://10.209.10.10/Api/IRDs",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: function (data) {
                        return "http://10.209.10.10/Api/IRDs/" + data.id;
                    },
                    dataType: "json",
                    type: "PUT"
                },
                destroy: {
                    url: function (data) {
                        return "http://10.209.10.10/Api/IRDs/" + data.id;
                    },
                    dataType: "json",
                    type: "DELETE"
                },
                create: {
                    url: "http://10.209.10.10/Api/IRDs",
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
                        matrixOutput: {
                            type : "number",
                            validation: {
                                //set validation rules
                                required: true,
                                min: 1,
                                max: 64
                            }
                        },
                        matrixInput: {
                            type: "number",
                            validation: {
                                //set validation rules
                                required: true,
                                min: 1,
                                max: 64
                            }
                        },
                        irdType: {},
                        serialNumber: {},
                        registerNumber: {},
                        fwVersion: {},
                        comments: {},
                        ipAddress: {
                            validation: {
                                //set validation rules
                                required: true
                            }
                        }
                    }
                }
            },
            pageSize: 25,
            error: function (e) {
                $("#irds").data("kendoGrid").dataSource.cancelChanges();
                var notificationElement = $("#notif").data("kendoNotification");
                notificationElement.show("Status: " + e.status + "; Error message: " + e.xhr.responseText, "error");
            }
        },
        columns: [
            { field: "id", title: "ID" },
            { field: "name", title: "IRD name" },
            { field: "matrixOutput", title: "matrix output", format: "{0:#}" },
            { field: "matrixInput", title: "matrix input", format: "{0:#}" },
            {
                field: "irdType",
                title: "Type",
                editor: irdTypeEditor,
                template: function (dataitem) {
                    //switch (dataitem.irdType) {
                    //    case 1:
                    //        return "RX8200";
                    //    case 2:
                    //        return "TX1260";
                    //    case 3:
                    //        return "TX1290";
                    //    case 4:
                    //        return "RX8200S2ip";
                    //    default:
                    //        return "error";
                    //}
                    //TODO: Refactor hardcoded url
                    var jsonObjectInstance = $.parseJSON(
                        $.ajax(
                            {
                                url: "http://10.209.10.10/Api/Enums?enumtype=IRDTypes&val=" + dataitem.irdType,
                                async: false,
                                dataType: 'json'
                            }
                        ).responseText
                    );
                    return jsonObjectInstance;
                }
            },
            { field: "serialNumber", title: "serial number" },
            { field: "registerNumber", title: "reg number" },
            { field: "fwVersion", title: "FW version" },
            { field: "ipAddress", title: "IP address", editor : ipAddressEditor },
            { field: "comments", title: "Comment" }
        ],
        scrollable: false,
        pageable: {
            refresh: true,
            pageSizes: false,
            buttonCount: 5
        },
        
    };

    //TODO: Refactor hardcoded url
    var extCardsOptions = {
        selectable: "row",
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "http://10.209.10.10/Api/EXTCards",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: function (data) {
                        return "http://10.209.10.10/Api/EXTCards/" + data.id;
                    },
                    dataType: "json",
                    type: "PUT"
                },
                destroy: {
                    url: function (data) {
                        return "http://10.209.10.10/Api/EXTCards/" + data.id;
                    },
                    dataType: "json",
                    type: "DELETE"
                },
                create: {
                    url: "http://10.209.10.10/Api/EXTCards",
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
                        matrixOutput: {
                            type:"number",
                            validation: {
                                //set validation rules
                                required: true,
                                min: 0
                                
                            }
                        },
                        comments: {}
                    }
                }
            },
            pageSize: 25,
            error: function (e) {
                $("#ext-cards").data("kendoGrid").dataSource.cancelChanges();
                var notificationElement = $("#notif").data("kendoNotification");
                notificationElement.show("Status: " + e.status + "; Error message: " + e.xhr.responseText, "error");
            }
        },
        columns: [
            { field: "id", title: "ID" },
            { field: "name", title: "Name" },
            { field: "matrixOutput", title: "SDI Matrix Output", width: '200px', format: "{0:#}" },
            { field: "comments", title: "Comments" }
        ],
        scrollable: false,
        pageable: {
            refresh: true,
            pageSizes: false,
            buttonCount: 5
        },
        
    };

// ***************************  CONTROLLER ********************************
    var SetupChannelsCtrl = function ($scope) {
        $scope.satellitePositionsOptions = satellitePositionsOptions;
        $scope.satellitesOptions = satellitesOptions;
        $scope.fixedAntennasOptions = fixedAntennasOptions;
        $scope.steerableAntennasOptions = steerableAntennasOptions;
        $scope.irdsOptions = irdsOptions;
        $scope.extCardsOptions = extCardsOptions;
    }

    var myControllerModule = angular.module("TV2Presets2.ctrl.setup", []);
    myControllerModule.controller("SetupPageCtrl", ["$scope", SetupChannelsCtrl]);
}());