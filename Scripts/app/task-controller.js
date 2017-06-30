angular.module('BroadridgeApp', ['ngAria', 'ngAnimate', 'ngMaterial', 'ui.bootstrap'])
    .controller('BroadridgeCtrl', function ($scope, $window, $http, $timeout) {
        $scope.tab = 1;

        $scope.setTab = function(newTab) {            
            $scope.tab = newTab;
        };

        $scope.isSet = function(tabNum) {
            return $scope.tab === tabNum;
        };       

        $scope.colorNames = [];
        $scope.taskList = [];
        $scope.working = false;

        $scope.Init = function() {
            $scope.working = false;

            $scope.GetSettings.call();

            $http({
                method: 'GET',
                url: 'api/setting/GetColorNames'
            }).then(function(success) {
                $scope.colorNames = success.data;
                $scope.working = false;
            }, function(error) {
                $scope.title = "Oops... something went wrong";
                $scope.working = false;
            });

            $http({
                method: 'GET',
                url: 'api/setting/GetPriorities'
            }).then(function(success) {
                $scope.priorities = success.data;
                $scope.working = false;
            }, function(error) {
                $scope.title = "Oops... something went wrong";
                $scope.working = false;
            });

            $scope.selectedTaskListType = 'All';

            $scope.getTaskList($scope.selectedTaskListType).call();
        }

        $scope.GetSettings = function() {
            $scope.working = true;

            $http({
                method: 'GET',
                url: 'api/setting/GetSettingDto'
            }).then(function(success) {
                $scope.settings = success.data;
                $scope.working = false;
            }, function(error) {
                $scope.title = "Oops... something went wrong";
                $scope.working = false;
            });
        };

        $scope.SaveSetting = function() {
            $scope.working = true;

            $http({
                method: 'POST',
                url: 'api/setting/SaveSetting',
                params: $scope.settings
            }).then(function(success) {
                $scope.working = false;
            }, function(error) {
                $scope.title = "Oops... something went wrong";
                $scope.working = false;
            });
        };

        $scope.SaveTask = function() {
            $scope.working = true;

            $http({
                method: 'POST',
                url: 'api/task/SaveTask',
                params: $scope.taskUpdated
            }).then(function(success) {
                $window.alert("Task saved!");

                $scope.taskUpdated.Name = '';
                $scope.taskUpdated.Description = '';
                $scope.taskUpdated.Priority = 1; //Normal

                $scope.working = false;
            }, function(error) {
                $scope.title = "Oops... something went wrong";
                $scope.working = false;
            });
        };
              
        //Table start      
        //$scope.convertTimeToCompleteToString = function (milliSecondsIn) {

        //    var secsIn = milliSecondsIn / 1000;
        //    var milliSecs = milliSecondsIn % 1000;

        //    var hours = secsIn / 3600,
        //    remainder = secsIn % 3600,
        //    minutes = remainder / 60,
        //    seconds = remainder % 60;


        //    var result = "";

        //    if (hours >= 1) {
        //        result += hours + "h: ";
        //    }

        //    if (minutes >= 1) {
        //        result += minutes + "m: ";
        //    }

        //    if (minutes >= 1) {
        //        result += minutes + "m: ";
        //    }

        //    return  result;
        //}

        $scope.getTaskList = function (taskListType) {
            $scope.selectedTaskListType = taskListType;

            $http({
                method: 'GET',
                url: 'api/task/GetTaskList',
                params: {taskListType}
            }).then(function (success) {
                var taskList = [];
                var taskListFromServer = success.data;

                var dateNow = new Date();
                var dateFormat = $scope.settings.DateTimeFormat.toUpperCase();

                for (var i = 0, len = taskListFromServer.length; i < len; i++) {
                    var task = {};
                    var taskFromServer = taskListFromServer[i];

                    task.TaskID = taskFromServer.TaskID;
                    $window.UpdateTaskFieds(task, taskFromServer, dateNow, dateFormat, $scope.priorities);                    

                    taskList.push(task);
                }

                $scope.taskList = taskList;
            }, function (error) {
                $scope.title = "Oops... something went wrong";
            });
        }        

        $scope.getters = {
            firstName: function(value) {
                //this will sort by the length of the first name string
                return value.firstName.length;
            }
        };

        $scope.taskListSelectRow = function(row) {
            $scope.selectedTask = row;
        }

        $scope.taskListSelectClick = function (row) {
            if (row.buttonClickMethod === 'remove') {
                $scope.removeTask(row).call();
            }

            if (row.buttonClickMethod === 'complete') {
                $scope.completeTask(row).call();
            }
        }

        $scope.removeTask = function (row) {
            var taskID = row.TaskID;
            $http({
                method: 'POST',
                url: 'api/task/RemoveTask',
                params: { taskID }
            }).then(function (success) {
                $scope.getTaskList($scope.selectedTaskListType).call();
                $scope.working = false;
            }, function (error) {
                $scope.title = "Oops... something went wrong";
                $scope.working = false;
            });
        }

        $scope.completeTask = function (row) {
            var taskID = row.TaskID;
            $http({
                method: 'POST',
                url: 'api/task/CompleteTask',
                params: { taskID }
            }).then(function (success) {
                $scope.getTaskList($scope.selectedTaskListType).call();
                $scope.working = false;
            }, function (error) {
                $scope.title = "Oops... something went wrong";
                $scope.working = false;
            });
        }
        //Table stop

        //Timer start
        $scope.counter = 0;
        $scope.onTimeout = function () {            
            var taskIds = [];

            for (var i = 0; i < $scope.taskList.length; i++) {
                var task = $scope.taskList[i];
                var taskId = task.TaskID;
                taskIds.push(taskId);
            }

            if (taskIds.length > 0) {
                $http({
                    method: 'POST',
                    url: 'api/task/GetUpdates',
                    params: { nums: taskIds }                    
                }).then(function (success) {
                    var updatedTasks = success.data;
                    var loadedTasks = $scope.taskList;

                    var dateNow = new Date();
                    var dateFormat = $scope.settings.DateTimeFormat.toUpperCase();

                    for (var i = 0; i < updatedTasks.length; i++) {
                        var taskFromServer = updatedTasks[i];
                        var task = undefined;

                        for (var j = 0; j < loadedTasks.length; j++) {
                            if (loadedTasks[j].TaskID === taskFromServer.TaskID) {
                                task = loadedTasks[j];
                                break;
                            }
                        }

                        if (task != undefined) {
                            $window.UpdateTaskFieds(task, taskFromServer, dateNow, dateFormat, $scope.priorities);
                        }
                    }
                }, function (error) {
                    $scope.title = "Oops... something went wrong";
                    $scope.working = false;
                });
            }

            mytimeout = $timeout($scope.onTimeout, 1000);
        }

        var mytimeout = $timeout($scope.onTimeout, 1000);
        //Timer stop
    }).config(function ($mdDateLocaleProvider) {
        $mdDateLocaleProvider.formatDate = function(date) {
            return moment(date).format('YYYY/MM/DD'); //$scope.settings.DateTimeFormat);
        };       
    });

function UpdateTaskFieds(task, taskFromServer, dateNow, dateFormat, priorities) {
    task.Name = taskFromServer.Name;
    task.Description = taskFromServer.Description;

    for (var j = 0; j < priorities.length; j++) {
        var priority = priorities[j];
        if (priority.Priority === taskFromServer.Priority) {
            task.Priority = priority.Name;
        }
    }                                        

    var timeCreate = new Date(taskFromServer.TimeCreate);
    task.TimeCreate = moment(timeCreate).format(dateFormat);

    var timeToComplete = new Date(taskFromServer.TimeToComplete);                    

    if (timeToComplete <= dateNow) {
        task.TimeToComplete = undefined;
        task.buttonCaption = 'Remove';
        task.buttonClickMethod = 'remove';
        task.Status = 'Completed';
    } else {
        //var timeSpanToComplete = timeToComplete - dateNow;
        task.TimeToComplete = moment(timeToComplete).format(dateFormat);;
        task.buttonCaption = 'Complete';
        task.buttonClickMethod = 'complete';
        task.Status = 'Active';
    }
}