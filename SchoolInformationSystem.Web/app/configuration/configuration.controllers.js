var configurationControllers = angular.module("configurationControllers", ["ui.bootstrap",
                  "configurationServices",
                  "userServices",
                  "commonConstants"]);

configurationControllers.controller("ConfigurationIndexCtrl", ["$scope", function($scope){
  //TODO load information about the schools registered in the system.
  //TODO load user list and have a way to create users
}]);


configurationControllers.controller("SchoolListCtrl", ["$scope", "$modal", "SchoolSvc", "toastr", function($scope, $modal, SchoolSvc, toastr){
  $scope.schools = [];
  $scope.openAddSchoolModal = function(){
    var modalInstance = $modal.open({
      size: "lg",
      templateUrl: "app/configuration/addSchoolModal.template.html",
      controller: "AddSchoolModalCtrl",
      animation: true
    });

    modalInstance.result.then(function(school){
      $scope.schools.push(school);
      toastr.success("Added school");
    });
  }

  SchoolSvc.getSchools().then(function(response){
    $scope.schools = response.data;
  });
}]);


configurationControllers.controller("AddSchoolModalCtrl", ["$scope", "$modalInstance", "SchoolSvc", function($scope, $modalInstance, SchoolSvc){
  $scope.newSchool = {};

  $scope.save = function(){
    if($scope.form.$valid){
      SchoolSvc.addSchool($scope.newSchool).then(function(response){
        $modalInstance.close(response.data);
      }, function(response){
        $scope.errorMessage = "Unable to save new school";
      });
    }
  }
  $scope.close = function(){
    $modalInstance.dismiss("close");
  }
}]);

configurationControllers.controller("ConfigurationUserListCtrl", ["$scope", "$modal", "UserSvc", "toastr", function($scope, $modal, UserSvc, toastr){
  $scope.users = [];
  $scope.search = {};

  $scope.showAddUser = function(){
    var modalInstance = $modal.open({
      templateUrl: "app/configuration/addUserModal.template.html",
      controller: "ConfigurationAddUserModalCtrl",
      size: "lg",
      animation: true
    });

    modalInstance.result.then(function(user){
      $scope.users.push(user);
    });
  }

  $scope.searchUsers = function(){
    UserSvc.searchUsers($scope.search).then(function(response){
      $scope.users = response.data;
    }, function(response){
      toastr.error("Unable to search users");
    });
  }

  /**
  * Init
  */
  $scope.searchUsers();

}]);

configurationControllers.controller("ConfigurationAddUserModalCtrl", ["$scope", "$modalInstance", "UserSvc", "SCOPE_LEVELS", "SchoolSvc", function($scope, $modalInstance, UserSvc, SCOPE_LEVELS, SchoolSvc){
  $scope.errorMessage = null;
  $scope.user = {};
  $scope.scopeLevels = _.map(SCOPE_LEVELS, function(val){
    return val;
  });
  $scope.scopeLevels.splice(0, 0, { value: null, description: "-- Select --"});

  $scope.isSchoolLevel = false;
  $scope.updateScopeLevelValues = function(){
    $scope.isSchoolLevel = $scope.user.scopeLevel == SCOPE_LEVELS.SCHOOL.value;
  }

  $scope.save = function(){
    if($scope.form.$valid){
      UserSvc.createUser($scope.user).then(function(response){
        $modalInstance.close(response.data);
      }, function(){
        $scope.errorMessage = "Error Saving User";
      });
    }
  }

  $scope.close = function(){
    $modalInstance.dismiss("close");
  }
  $scope.schools = [];
  function loadSchools(){
    SchoolSvc.getSchools().then(function(response){
      $scope.schools = response.data;
      $scope.schools.splice(0, 0, { id: null, name: "-- Select --"});
    });
  }
  loadSchools();
}]);



configurationControllers.controller("SystemSetupCtrl", ["$scope", "ConfigurationSvc", "toastr", function($scope, ConfigurationSvc, toastr){
  $scope.newGrade = {};
  $scope.addGrade = function(){
    if($scope.newGradeForm.$valid){
      $scope.systemSetup.grades = $scope.systemSetup.grades || [];
      $scope.systemSetup.grades.push($scope.newGrade);
      
      $scope.newGrade = {};
      $scope.newGradeForm.$setPristine(); 
    }
  }
  
  $scope.removeGrade = function(grade){
    var ndx = $scope.systemSetup.grades.indexOf(grade);
    if(ndx > -1){
      $scope.systemSetup.grades.splice(ndx,1); 
    }
  }
  
  $scope.saveChanges = function(){
    ConfigurationSvc.updateSystemSetup($scope.systemSetup).then(function(){
      toastr.success("Setup saved");
    });
  }
  
  ConfigurationSvc.getSystemSetup().then(function(res){

    if(res.data == ""){
      res.data = undefined;
    }
    $scope.systemSetup = res.data || {};
  });
}]);