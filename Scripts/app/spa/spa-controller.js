angular.module('TaskSpaApp', ['ngAria', 'ngAnimate', 'ngMaterial', 'ui.bootstrap', 'settingsModule', 'taskModule', 'taskListModule', 'taskChartModule'])
.controller('TaskSpaController', ['$scope', function ($scope) {
    $scope.tab = 1;

    $scope.setTab = function (newTab) {
        $scope.tab = newTab;
    };

    $scope.isSet = function (tabNum) {
        return $scope.tab === tabNum;
    };
}]);