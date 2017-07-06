angular.
    module('taskChartModule', ['ui.bootstrap', 'ui.chart'])
    .component('taskChart', {
        templateUrl: '/BroadridgeTestProject/Scripts/app/spa/taskChart/taskChart.template.html',
        controller: taskChartController
    });

function taskChartController($http, $scope, chartService) {
    $scope.taskChartData = [[['Chris', 12], ['Manuel', 9], ['Dustin', 14], ['Anu', 16], ['Vijay', 7], ['El Luchadore', 9]]];

    $scope.$watch('taskChartData', function () {
        chartService.drawChart('chartDivId', 'Tasks by type', $scope.taskChartData);
    });

    $scope.drawChart = function() {        
        chartService.drawChart('chartDivId', 'Tasks by type', $scope.taskChartData);
    };

    $scope.changeDataChart = function() {
        $scope.taskChartData = [[['Chris', 55], ['Manuel', 11], ['Dustin', 24]]];
    };
}