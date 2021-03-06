﻿(function () {
    var http;

    var satellitesDropDownEditor = function(container, options) {
        $("<input required data-text-field='name' data-value-field='id' data-bind='value: " + options.field + "'/>")
            .appendTo(container)
            .kendoComboBox({
                autoBind: false,
                dataSource: {
                    transport: {
                        read: {
                            url: "http://10.209.10.10/Api/Satellites",
                            dataType: "json",
                            type: "GET"
                        }
                    }
               }
           });
    }
    var satellitesDropDownFilter = function (element) {
        element.kendoComboBox({
                autoBind: false,
                dataSource: {
                    transport: {
                        read: {
                            url: "http://10.209.10.10/Api/Satellites",
                            dataType: "json",
                            type: "GET"
                        }
                    }
                },
                dataTextField: "name",
                dataValueField: "id"
            });
    }

    var polarisationEditor = function(container, options) {
        $("<input required data-text-field='text' data-value-field='value' data-bind='value: " + options.field + "'/>")
           .appendTo(container)
           .kendoComboBox({
               autoBind: false,
               dataSource: {
                   transport: {
                       read: {
                           url: "http://10.209.10.10/Api/Enums?enumtype=PolarisationEnum",
                           dataType: "json",
                           type: "GET"
                       }
                   }
               }
           });
    }

    var polarisationFilter = function (element) {
        element.kendoComboBox({
               autoBind: false,
               dataSource: {
                   transport: {
                       read: {
                           url: "http://10.209.10.10/Api/Enums?enumtype=PolarisationEnum",
                           dataType: "json",
                           type: "GET"
                       }
                   }
               },
               dataTextField: "text",
               dataValueField: "value"
           });
    }

    var fecEditor = function (container, options) {
        $("<input required data-text-field='text' data-value-field='value' data-bind='value: " + options.field + "'/>")
           .appendTo(container)
           .kendoComboBox({
               autoBind: false,
               dataSource: {
                   transport: {
                       read: {
                           url: "http://10.209.10.10/Api/Enums?enumtype=FECEnum",
                           dataType: "json",
                           type: "GET"
                       }
                   }
               }
           });
    }

    var fecFilter = function (element) {
        element.kendoComboBox({
            autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: "http://10.209.10.10/Api/Enums?enumtype=FECEnum",
                        dataType: "json",
                        type: "GET"
                    }
                }
            },
            dataTextField: "text",
            dataValueField: "value"
        });
    }

    var rollOffEditor = function (container, options) {
        $("<input required data-text-field='text' data-value-field='value' data-bind='value: " + options.field + "'/>")
           .appendTo(container)
           .kendoComboBox({
               autoBind: false,
               dataSource: {
                   transport: {
                       read: {
                           url: "http://10.209.10.10/Api/Enums?enumtype=RollOffEnum",
                           dataType: "json",
                           type: "GET"
                       }
                   }
               }
           });
    }

    var rollOffFilter = function (element) {
        element.kendoComboBox({
            autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: "http://10.209.10.10/Api/Enums?enumtype=RollOffEnum",
                        dataType: "json",
                        type: "GET"
                    }
                }
            },
            dataTextField: "text",
            dataValueField: "value"
        });
    }

    var sdhdEditor = function (container, options) {
        $("<input required data-text-field='text' data-value-field='value' data-bind='value: " + options.field + "'/>")
           .appendTo(container)
           .kendoComboBox({
               autoBind: false,
               dataSource: {
                   transport: {
                       read: {
                           url: "http://10.209.10.10/Api/Enums?enumtype=SDHD",
                           dataType: "json",
                           type: "GET"
                       }
                   }
               }
           });
    }

    var sdhdFilter = function (element) {
        element.kendoComboBox({
            autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: "http://10.209.10.10/Api/Enums?enumtype=SDHD",
                        dataType: "json",
                        type: "GET"
                    }
                }
            },
            dataTextField: "text",
            dataValueField: "value"
        });
    }
    var modulationEditor = function (container, options) {
        $("<input required data-text-field='text' data-value-field='value' data-bind='value: " + options.field + "'/>")
           .appendTo(container)
           .kendoComboBox({
               autoBind: false,
               dataSource: {
                   transport: {
                       read: {
                           url: "http://10.209.10.10/Api/Enums?enumtype=ModulationEnum",
                           dataType: "json",
                           type: "GET"
                       }
                   }
               }
           });
    }

    var modulationFilter = function (element) {
        element.kendoComboBox({
            autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: "http://10.209.10.10/Api/Enums?enumtype=ModulationEnum",
                        dataType: "json",
                        type: "GET"
                    }
                }
            },
            dataTextField: "text",
            dataValueField: "value"
        });
    }

    var getEnumTextForValue = function (enumneme, enumvalue) {
        if (!(parseInt(enumvalue) === enumvalue)) {
            return "";
        }

        var jsonObjectInstance = $.parseJSON(
                       $.ajax(
                           {
                               url: "http://10.209.10.10/Api/Enums?enumtype=" + enumneme + "&val=" + enumvalue,
                               async: false,
                               dataType: 'json'
                           }
                       ).responseText
                   );
        return jsonObjectInstance;
    }

    function onDataBound(e) {
        var grid = $("#downlinkChanels").data("kendoGrid");
        $(grid.tbody).on("click", "td", function (e) {
            var row = $(this).closest("tr");
            var rowIdx = $("tr", grid.tbody).index(row);
            //var colIdx = $("td", row).index(this);
            //alert(rowIdx + '-' + colIdx);
            angular.element("#downlinkChanels").scope().clickedRow = rowIdx;
        });
    }

    var applyPreset = function (e) {
        e.preventDefault();
        angular.element("#downlinkChanels").scope().openDialog();
    }

    var duplicateGridItem = function (e, $http) {
        e.preventDefault();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        dataItem.satellite = null;

        http.post('http://10.209.10.10/Api/DownlinkChannels', dataItem).success(function (data) {
            $("#downlinkChanels").data("kendoGrid").dataSource.read();
        });
    }

    var downlinkChannelsOptions = {
        selectable: "row",
        dataBound: onDataBound,
        filterable: {
            extra: false
        },
        sortable: {
            mode: "single",
            allowUnsort: true
        },
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "http://10.209.10.10/Api/DownlinkChannels",
                    dataType: "json",
                    type: "GET"
                },
                update: {
                    url: function(data) {
                        return "http://10.209.10.10/Api/DownlinkChannels/" + data.id;
                    },
                    dataType: "json",
                    type: "PUT",
                    complete: function (data) {
                        $("#downlinkChanels").data("kendoGrid").dataSource.read();
                    }
                },
                destroy: {
                    url: function(data) {
                        return "http://10.209.10.10/Api/DownlinkChannels/" + data.id;
                    },
                    dataType: "json",
                    type: "DELETE"
                },
                create: {
                    url: "http://10.209.10.10/Api/DownlinkChannels",
                    dataType: "json",
                    type: "POST",
                    complete: function (data) {
                        $("#downlinkChanels").data("kendoGrid").dataSource.read();
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
                        satelliteId: {
                            type: "number",
                            editable: true,
                            nullable: false,
                            validation: {
                                required: true
                            }
                        },
                        name: {
                            validation: {
                                //set validation rules
                                required: true
                            },
                            defaultValue: "NewChannel"
                        },
                        frequency: {
                            type: "number",
                            validation: {
                                required: true,
                                min: 10700,
                                max: 12750 
                            }
                        },
                        symbolRate: {
                            type: "number",
                            validation: {
                                required: true
                            },
                            format: "n3",
                            decimals: 3
                        },
                        polarisation: {
                            validation: {
                                required: true
                            }
                        },
                        fec: {
                            validation: {
                                required: true
                            }
                        },
                        rollOff: {
                            validation: {
                                required: true
                            }
                        },
                        sdhd: {
                            validation: {
                                required: true
                            }
                        },
                        ebuChannel: { type : "boolean", defaultValue : false, nullable : false },
                        modulation: { validation: { required: true } }
                    }
                }
            },
            pageSize: 25,
            error: function (e) {
                $("#downlinkChanels").data("kendoGrid").dataSource.cancelChanges();
                var notificationElement = $("#notif").data("kendoNotification");
                notificationElement.show("Status: " + e.status + "; Error message: " + e.xhr.responseText, "error");
            }
        },
        columns: [
            {
                field: "id",
                title: "ID",
                width: "50px",
                filterable: false
            },
            {
                field: "name",
                title: "Channel name",
                width: "150px",
                filterable: {
                    extra: false, //do not show extra filters
                    operators: { // redefine the string operators
                        string: {
                            contains: "Contains",
                            eq: "Is equal to",
                            neq: "Is not equal to",
                            startswith: "Starts With",
                            doesnotcontain: "Does not contain",
                            endswith: "Ends with"
                        }
                    }

                }
            },
            {
                field: "satelliteId",
                title: "Satellite",
                editor: satellitesDropDownEditor,
                template: "#=(typeof satellite !== 'undefined' && satellite != null) ? satellite.name : ''#",
                width: "100px",
                filterable: {
                    ui: satellitesDropDownFilter
                }
            },
            {
                field: "frequency",
                title: "Frequency[Mhz]",
                format: "{0:#.000 MHz}",
                editor: function (container, options) {
                    $('<input data-bind="value:' + options.field + '"/>')
                        .appendTo(container)
                        .kendoNumericTextBox({
                            format: "{0:#.000 Mhz}",
                            decimals: 3,
                            value: 10700,
                            min: 10700,
                            max: 12750
                        });
                }
            },
            {
                field: "symbolRate",
                title: "Symbol rate",
                format: "{0:#.000 Msps}",
                editor: function(container, options) {
                $('<input data-bind="value:' + options.field + '"/>')
                    .appendTo(container)
                    .kendoNumericTextBox({
                        format: "{0:#.000 Msps}",
                        decimals: 3
                    });               
                }
            },
            {
                field: "polarisation",
                title: "Polarisation",
                editor: polarisationEditor,
                template: function (dataItem) {
                    return getEnumTextForValue("PolarisationEnum", dataItem.polarisation);
                },
                filterable: {
                    ui: polarisationFilter
                }
            },
            {
                field: "fec",
                title: "FEC",
                editor: fecEditor,
                template: function (dataItem) {
                    return getEnumTextForValue("FECEnum", dataItem.fec);
                },
                filterable: {
                    ui: fecFilter
                }
            },
            {
                field: "modulation",
                title: "Modulation",
                editor: modulationEditor,
                template: function (dataItem) {
                    return getEnumTextForValue("ModulationEnum", dataItem.modulation);
                },
                filterable: {
                    ui: modulationFilter
                }
            },
            {
                field: "rollOff",
                title: "Roll off",
                editor: rollOffEditor,
                template: function (dataItem) {
                    return getEnumTextForValue("RollOffEnum", dataItem.rollOff);
                },
                filterable: {
                    ui: rollOffFilter
                }
            },
            {
                field: "sdhd",
                title: "SD or HD",
                editor: sdhdEditor,
                template: function (dataItem) {
                    return getEnumTextForValue("SDHD", dataItem.sdhd);
                },
                filterable: {
                    ui: sdhdFilter
                }
            },
            {
                field: "ebuChannel",
                title: "EBU Channel"
            },
            { command: [{ name: "edit", text: "" }, { name: "destroy", text: "" }, { text: "apply", click: applyPreset }, { text: "duplicate", click: duplicateGridItem }], title: "action", width: "330px" }
        ],
        pageable: {
            refresh: true,
            pageSizes: false,
            buttonCount: 5
        },
       
        height: 500
    }
   

//******************  CONTROLLER   ******************************************************
    var DownlinkChannelsCtrl = function ($scope, $modal, $http) {
        http = $http;
        $scope.downlinkChannelsOptions = downlinkChannelsOptions;
        $scope.clickedRow = -1;

        $scope.$on("kendoRendered", function (e) {
            var grid = $("#downlinkChanels").data("kendoGrid");
            grid.dataSource.originalFilter = grid.dataSource.filter;
            grid.dataSource.filter = function () {
                if (arguments.length > 0) {
                    this.trigger("filtering", arguments);
                }

                var result = grid.dataSource.originalFilter.apply(this, arguments);
                return result;
            }

            grid.dataSource.bind("filtering", function (e) {
                $("#downlinkChanels th .k-link").each(function (index, value) { $(value).css('color', 'white'); });
                if (e[0] !== null) {
                    for (var i = 0; i < e[0].filters.length; i++) {
                        $("#downlinkChanels th[data-field=" + e[0].filters[i].field + "] .k-link").css('color', 'red');
                    }
                }
            });
        });

        $scope.openDialog = function (e) {
            var grid = $("#downlinkChanels").data("kendoGrid"),
            row = grid.tbody.find(">tr:not(.k-grouping-row)").eq($scope.clickedRow);
            grid.select(row);
            //here to open modal dialog
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: "/Home/ApplyDownlinkPresetDialog",
                controller: "ApplyDownlinkChannelDialogCtrl",
                size : "lg",
                resolve : {
                    dwlChannel : function() {
                        return $scope.selectedchannel;
                    }
                }

            });

            //modalInstance.result.then();

        };
        $scope.onChannelSelected = function(dataitem) {
            $scope.selectedchannel = dataitem;
        }
    }

    // register my controller module
    var myControllerModule = angular.module("TV2Presets2.ctrl.downlink", ["TV2Presets2.ctrl.downlink.dialog"]);
    myControllerModule.controller("DownlinkPageCtrl", ["$scope", "$modal", "$http", DownlinkChannelsCtrl]);
}());