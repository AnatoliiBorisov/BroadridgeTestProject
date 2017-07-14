angular.
    module('taskChartModule').
    service('taskChartModuleService', function (chartService) {
        var isChartDrawed = false;
        var chartData = undefined;

        this.drawChart = function () {
            if (!isChartDrawed && chartData != undefined) {                
                window.setTimeout(function () {
                    chartService.drawChart('chartDivId', 'Tasks by type', chartData);
                    isChartDrawed = true;
                }, 10);                               
            }
        }

        this.setChartData = function(value) {
            chartData = value;
        }
    });