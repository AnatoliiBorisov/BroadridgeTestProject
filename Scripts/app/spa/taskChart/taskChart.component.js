angular.
    module('taskChartModule', ['ui.bootstrap', 'ui.chart'])
    .component('taskChart', {
        templateUrl: '/BroadridgeTestProject/Scripts/app/spa/taskChart/taskChart.template.html',
        controller: taskChartController
    })
    //.value('charting', {
    //    pieChartOptions: { 
    //        seriesDefaults: {
    //            // Make this a pie chart.
    //            renderer: jQuery.jqplot.PieRenderer, 
    //            rendererOptions: {
    //                // Put data labels on the pie slices.
    //                // By default, labels show the percentage of the slice.
    //                showDataLabels: true
    //            }
    //        }, 
    //        legend: { show:true, location: 'e' }
    //    }
    //})

//    .directive('uiChart', function() {
//    return {
//        restrict : 'EACM',
//        template : '<div></div>',
//        replace : true,
//        link : function(scope, elem, attrs) {
//            var renderChart = function() {
//                var data = scope.$eval(attrs.uiChart);
//                elem.html('');
//                if (!angular.isArray(data)) {
//                    return;
//                }

//                var opts = {};
//                if (!angular.isUndefined(attrs.chartOptions)) {
//                    opts = scope.$eval(attrs.chartOptions);
//                    if (!angular.isObject(opts)) {
//                        throw 'Invalid ui.chart options attribute';
//                    }
//                }

//                elem.jqplot(data, opts);
//            };

//            scope.$watch(attrs.uiChart, function() {
//                renderChart();
//            }, true);

//            scope.$watch(attrs.chartOptions, function() {
//                renderChart();
//            });
//        }
//    };
//})
;

function taskChartController($http, $scope) {
    //$scope.someData = [[
    //  ['Heavy Industry', 12],['Retail', 9], ['Light Industry', 14], 
    //  ['Out of home', 16],['Commuting', 7], ['Orientation', 9]
    //]];

    //$scope.myChartOpts = charting.pieChartOptions;

    //pie chart data - scope
    $scope.someData = [[['Chris', 12], ['Manuel', 9], ['Dustin', 14], ['Anu', 16], ['Vijay', 7], ['El Luchadore', 9]]];


    // pie chart options - scope
    $scope.myChartOpts = {
        seriesDefaults : {
            // use the pie chart renderer
            renderer : jQuery.jqplot.PieRenderer,
            rendererOptions : {
                // Put data labels on the pie slices.
                // By default, labels show the percentage of the slice.
                showDataLabels : true
            }
        },
        legend : {
            show : true,
            location : 's'
        }
    };


    $scope.drawChart = function () {
        var slice_1 = ['North America', 150];
		var slice_2 = ['Europe', 50];
		var series = [slice_1, slice_2];
		var data = [series];
		 
		var options = {
		title: 'Sales by Region',
		seriesDefaults: {
			renderer: jQuery.jqplot.PieRenderer
		},
		legend: { show:true, location: 'e' }
		};
		   
		$.jqplot('chartDivId', data, options);
    }
}