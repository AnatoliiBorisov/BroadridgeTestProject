angular.
    module('taskChartModule')
    .component('taskChart', {
        templateUrl: '/BroadridgeTestProject/Scripts/app/spa/taskChart/taskChart.template.html',
        controller: taskChartController
    });

function taskChartController($http, $scope, $timeout, chartService, taskChartModuleService) {
    $scope.taskChartData = [[[]]];    
    
    $http({
        method: 'GET',
        url: '/BroadridgeTestProject/api/TaskChart'
    }).then(function (success) {
        var data = success.data.sort(function(a, b) {
            return b.Priority - a.Priority;
        });;
        
        var taskChartValues = [];
        for (var i = 0; i < data.length; i++) {
            var dataValue = data[i];

            var taskChartValue = [dataValue.PriorityName, dataValue.Count];

            taskChartValues.push(taskChartValue);
        }        
        
        $scope.taskChartData = [taskChartValues];
        taskChartModuleService.setChartData($scope.taskChartData);
    }, function (error) {
        
    });

    $scope.drawChart = function() {        
        chartService.drawChart('chartDivId', 'Tasks by type', $scope.taskChartData);
    };    
}