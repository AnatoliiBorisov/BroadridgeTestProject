angular
    .module('taskModule')
    .component('task', {
        templateUrl: '/BroadridgeTestProject/Scripts/app/spa/task/task.template.html',
        controller: taskController})        
    .config(function($mdDateLocaleProvider) {
                $mdDateLocaleProvider.formatDate = GetDateFormat;
            });

var dateFormat = "YYYY-MM-DD";

function GetDateFormat(date) {
    return moment(date).format(dateFormat);
}

function taskController($http, $scope, $window, settingService) {
    $scope.loading = true;

    $scope.taskUpdated = {};

    settingService.getSettings().then(function (data) {
        dateFormat = data.DateFormat;
        var date = new Date();
        $scope.taskUpdated.TimeToComplete = date;
    });

    $http({
        method: 'GET',
        url: '/BroadridgeTestProject/api/priority'
    }).then(function (success) {
        $scope.priorities = success.data;
        $scope.loading = false;
    }, function (error) {
        $scope.loading = true;
    });

    $scope.SaveTask = function () {
        $scope.loading = true;

        $http({
            method: 'POST',
            url: '/BroadridgeTestProject/api/task',
            params: $scope.taskUpdated
        }).then(function (success) {
            $window.alert("Task saved!");

            $scope.taskUpdated.Name = '';
            $scope.taskUpdated.Description = '';
            $scope.taskUpdated.Priority = 1; //Normal

            $scope.loading = false;
        }, function (error) {
            $window.alert('Error of saving task');
            $scope.loading = false;
        });
    };
}