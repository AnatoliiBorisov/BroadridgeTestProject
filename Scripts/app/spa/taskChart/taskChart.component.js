angular.
    module('taskChartModule', ['ui.bootstrap']).
    component('taskChart', {
        templateUrl: '/BroadridgeTestProject/Scripts/app/spa/taskChart/taskChart.template.html',
        controller: taskChartController
    });

function taskChartController($http, $scope) {

}