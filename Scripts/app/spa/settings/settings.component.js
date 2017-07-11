angular.
    module('settingsModule').
    component('settings', {
        templateUrl: '/BroadridgeTestProject/Scripts/app/spa/settings/settings.template.html',
        controller: settingsController});

function settingsController($http, $scope) {
    $scope.loading = true;

    $scope.GetSettings = function () {
        $scope.working = true;

        $http({
            method: 'GET',
            url: '/BroadridgeTestProject/api/setting'
        }).then(function (success) {
            $scope.settings = success.data;
            $scope.loading = false;
        }, function (error) {
            $scope.loading = true;
        });
    };

    $scope.SaveSetting = function () {
        $scope.loading = true;

        $http({
            method: 'POST',
            url: '/BroadridgeTestProject/api/setting',
            params: $scope.settings
        }).then(function (success) {
            $scope.loading = false;
        }, function (error) {
            alert("Saving error");
            $scope.loading = false;
        });
    };

    $http({
        method: 'GET',
        url: '/BroadridgeTestProject/api/colorName'
    }).then(function (success) {
        $scope.colorNames = success.data;
        $scope.loading = false;

        $scope.GetSettings.call(); //refactor
    }, function (error) {        
        $scope.loading = true;
    });    

    $http({
        method: 'GET',
        url: '/BroadridgeTestProject/api/DateFormate'
    }).then(function (success) {
        $scope.dateFormates = success.data;
        $scope.loading = false;

        $scope.GetSettings.call(); //refactor
    }, function (error) {        
        $scope.loading = true;
    });
}