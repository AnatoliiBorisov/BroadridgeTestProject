angular
    .module('shared.services')
    .service('settingService', function($http) {
        this.getSettings = function() {
            var promise = $http({
                method: 'GET',
                url: '/BroadridgeTestProject/api/setting'
            }).then(function(response) {
                return response.data;
            }, function(error) {
                return error;
            });
            return promise;
        }
    });