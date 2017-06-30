angular.
    module('taskListModule', ['ui.bootstrap']).
    component('taskList', {
        templateUrl: '/BroadridgeTestProject/Scripts/app/spa/taskList/taskList.template.html',
        controller: taskListController
    })
    .service('priorityService', function($http) {
        this.getPriorities = function() {
            var promise = $http({
                method: 'GET',
                url: '/BroadridgeTestProject/api/priority'
            }).then(function(response) {
                return response.data;
            }, function(error) {
                return error;
            });
            return promise;
        }
    })
    .service('settingService', function ($http) {
        this.getSettings = function () {
            var promise = $http({
                method: 'GET',
                url: '/BroadridgeTestProject/api/setting'
            }).then(function (response) {
                return  response.data;                
            }, function (error) {
                return error;
            });
            return promise;
        }
    });

function taskListController($http, $scope, $window, $timeout, priorityService, settingService, $uibModal) {
    $scope.loading = true;
    $scope.isPrioritiesLoaded = false;
    $scope.isSettingsLoaded = false;
    
    //Pagination. Start.
    $scope.currentPage = 1;
    $scope.totalItems = 10;
    $scope.itemsPerPage = 10;

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };

    $scope.pageChanged = function () {
        $scope.getTaskList($scope.selectedTaskListType).call();//$log.log('Page changed to: ' + $scope.currentPage);
    };

    $scope.maxSize = 5;
    $scope.bigTotalItems = 175;
    $scope.bigCurrentPage = 1;

    $http({
        method: 'GET',
        url: '/BroadridgeTestProject/api/taskListSettigs'
    }).then(function (success) {
        var data = success.data;
        $scope.totalItems = data.TotalItems;
        $scope.itemsPerPage = data.ItemsPerPage;
    }, function (error) {
        
    });

    //Pagination. Stop.

    $scope.priorities = [];    
    priorityService.getPriorities().then(function (data) {
        $scope.priorities = data;
        $scope.isPrioritiesLoaded = true;
        $scope.loading = !($scope.isPrioritiesLoaded && $scope.isSettingsLoaded);
    });    

    $scope.settings = [];
    settingService.getSettings().then(function (data) {
        $scope.settings = data;
        $scope.isSettingsLoaded = true;
        $scope.loading = !($scope.isPrioritiesLoaded && $scope.isSettingsLoaded);
    });

    $scope.selectedTaskListType = 'All';
    
    $scope.getTaskList = function (taskListType) {
        $scope.selectedTaskListType = taskListType;

        //set all tasks buttons unactivated. start
        var buttonsUnactivatedClassName = "btn btn-info btn-lg";
        var buttonsActivatClassName = "btn btn-info btn-lg active";

        var btnTasksAll = angular.element(document.querySelector('#btnTasksAll'))[0];
        var btnTasksActive = angular.element(document.querySelector('#btnTasksActive'))[0];
        var btnTasksCompleted = angular.element(document.querySelector('#btnTasksCompleted'))[0];

        btnTasksAll.className = buttonsUnactivatedClassName;
        btnTasksActive.className = buttonsUnactivatedClassName;
        btnTasksCompleted.className = buttonsUnactivatedClassName;
        //set all tasks buttons unactivated. stop

        $http({
            method: 'GET',
            url: '/BroadridgeTestProject/api/task/get',
            params: {
                taskListType: taskListType,
                pageNo: $scope.currentPage
            }
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

            switch($scope.selectedTaskListType) {
                case "All":
                    btnTasksAll.className = buttonsActivatClassName;
                    break;
                case "Active":
                    btnTasksActive.className = buttonsActivatClassName;
                    break;
                case "Completed":
                    btnTasksCompleted.className = buttonsActivatClassName;
                    break;
            }
        }, function (error) {
            $scope.title = "Oops... something went wrong";
        });
    }

    $scope.getters = {
        firstName: function (value) {
            //this will sort by the length of the first name string
            return value.firstName.length;
        }
    };

    $scope.taskListSelectRow = function (row) {
        $scope.selectedTask = row;
    }

    $scope.taskListSelectClick = function (row) {
        if (row.buttonClickMethod === 'remove') {
            $scope.removeTask(row);
        }

        if (row.buttonClickMethod === 'complete') {
            $scope.completeTask(row);
        }
    }

    $scope.removeTask = function (row) {
        //MODAL WINDOW. CONFIRMATION OF DELETING TASK. START
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/BroadridgeTestProject/Scripts/app/spa/shared/ModalConfirmation.html',
            windowTemplateUrl: '/BroadridgeTestProject/Content/angular-ui/uib/template/modal/window.html',
            controller: 'ModalConfirmationCtrl',
            controllerAs: this,
            bindToController: false,                
            resolve: {
                title: function () {                    
                    return 'Подтвердите удаление';
                },
                task: function() {
                    return row;
                }
            }
        });
        
        modalInstance.result.then(function (result) {
            //ok
            var taskID = row.TaskID;
            $http({
                method: 'POST',
                url: '/BroadridgeTestProject/api/task/RemoveTask',
                params: { taskID }
            }).then(function (success) {
                $scope.getTaskList($scope.selectedTaskListType).call();
                $scope.loading = false;
            }, function (error) {
                $scope.title = "Oops... something went wrong";
                $scope.loading = false;
            });
        }, function () {
            //dismiss
        });        
        //MODAL WINDOW. CONFIRMATION OF DELETING TASK. STOP        
    }

    $scope.completeTask = function (row) {
        var taskID = row.TaskID;
        $http({
            method: 'POST',
            url: '/BroadridgeTestProject/api/task/CompleteTask',
            params: { taskID }
        }).then(function (success) {
            $scope.getTaskList($scope.selectedTaskListType).call();
            $scope.loading = false;
        }, function (error) {
            $scope.title = "Oops... something went wrong";
            $scope.loading = false;
        });
    }

    $scope.editTask = function (row) {
        //MODAL WINDOW. CONFIRMATION OF DELETING TASK. START
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/BroadridgeTestProject/Scripts/app/spa/taskList/taskEdit.template.html',
            windowTemplateUrl: '/BroadridgeTestProject/Content/angular-ui/uib/template/modal/window.html',
            controller: 'ModalEditTaskCtrl',
            controllerAs: this,
            bindToController: false,
            resolve: {
                task: function () {
                    return row;
                },

                priorities: function() {
                    return $scope.priorities;
                }
            }
        });

        modalInstance.result.then(function (result) {
            //ok
            $http({
                method: 'POST',
                url: '/BroadridgeTestProject/api/task',
                params: result
            }).then(function (success) {
                $window.alert("Task saved!");

                $scope.getTaskList($scope.selectedTaskListType).call();
                $scope.loading = false;
            }, function (error) {
                $window.alert('Error of saving task');
                $scope.loading = false;
            });
           
        }, function () {
            //dismiss
        });
    }

    $scope.getTaskList($scope.selectedTaskListType);

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
                url: '/BroadridgeTestProject/api/task/GetUpdates',
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
}

angular.module('taskListModule').controller('ModalConfirmationCtrl', function ($scope, $uibModalInstance, title, task) {
    $scope.title = title;
    $scope.taskName = task.Name;

    $scope.ok = function () {
        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});

angular.module('taskListModule').controller('ModalEditTaskCtrl', function ($scope, $uibModalInstance, task, priorities) {
    $scope.loading = true;
    $scope.priorities = priorities;

    var editedTask = {
        TaskID: task.TaskID,
        Name: task.Name,
        Description: task.Description,
        Priority: task.Priority,
        TimeCreate: task.TimeCreate,
        TimeToComplete: task.TimeToComplete
    };

    $scope.task = editedTask;

    $scope.ok = function () {
        $uibModalInstance.close($scope.task);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.loading = false;
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