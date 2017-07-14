angular.module('TaskSpaApp', ['ngAria', 'ngAnimate', 'ngMaterial', 'ui.bootstrap', 'settingsModule', 'taskModule', 'taskListModule', 'taskChartModule'])
.controller('TaskSpaController', ['$scope', 'taskChartModuleService', function ($scope, taskChartModuleService) {
    $scope.tab = 1;

    $scope.setTab = function (newTab) {
        $scope.tab = newTab;

        if ($scope.tab == 2) {
            taskChartModuleService.drawChart();
        }
    };

    $scope.isSet = function (tabNum) {
        return $scope.tab === tabNum;
    };
}]);