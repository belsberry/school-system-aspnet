var enrollmentControllers = angular.module("enrollmentControllers", ["enrollmentServices", "toastr", "ui.bootstrap"]);

enrollmentControllers.controller("EnrollmentLandingCtrl", ["$scope", "EnrollmentSvc", "toastr", function($scope, EnrollmentSvc, toastr){
  $scope.studentPopulationData = [];

  EnrollmentSvc.getStudentPopulations().then(function(response){
    $scope.studentPopulationData = _.map(response.data, function(pop){
      return{
        title: pop.name,
        labels: _.map(pop.values, function(val){
          return val.name;
        }),
        data: _.map(pop.values, function(val){
          return val.value;
        })
      }
    });
  }, function(response){
    toastr.error("Error getting student population", response);
  });

}]);



enrollmentControllers.controller("EnrollmentStudentListCtrl", ["$scope", "StudentSvc", "toastr", function($scope, StudentSvc, toastr){
  /**
  * Properties
  */
  $scope.students = [];
  $scope.searchString = "";
  $scope.domains = {};

  /**
  * methods
  */
  $scope.getStudents = function(){
    StudentSvc.doStudentSearch($scope.searchString).then(function(response){
      $scope.students = response.data;
    }, function(response){
      toastr.error(response);
    });
  }


  /**
  * Init
  */
  $scope.getStudents();



}]);


enrollmentControllers.controller("EnrollmentNewStudentCtrl", ["$scope", "StudentSvc", "toastr", "$location", function($scope, StudentSvc, toastr, $location){
  $scope.student = {};

  //Set this to allow the template to set the usid
  $scope.canEditUsid = true;

  $scope.domains = {};

  $scope.enrollStudent = function(){

    if($scope.form.$valid){
      StudentSvc.enrollStudent($scope.student).then(function(response){
        toastr.success("Successfully Added Student");
        $location.path("/enrollment");
      }, function(response){
        toastr.error("Error adding student");
      });
    }
  }


  StudentSvc.getStudentDomainData().then(function(response){
    $scope.domains = response.data;
  }, function(response){
    toastr.error("Unable to load page");
  });

}]);

enrollmentControllers.controller("EnrollmentStudentEditCtrl", ["$scope", "StudentSvc", "toastr", "$routeParams", "$location", function($scope, StudentSvc, toastr, $routeParams, $location){
  /**
  * Properties
  */
  var studentId = $routeParams.studentId;

  $scope.student = {};
  $scope.domains = {};

  $scope.updateStudent = function(){
    if($scope.form.$valid){

      StudentSvc.updateStudent($scope.student).then(function(response){
        toastr.success("Updated Student");
        $scope.form.$setPristine();
      }, function(response){
        toastr.error("Error Updating Student");
      });

    }
  }

  /**
  * Init
  */
  StudentSvc.getStudentDomainData().then(function(response){
    $scope.domains = response.data;
    StudentSvc.getStudentRecord(studentId).then(function(response){
      if(_.isEmpty(response.data)){
        toastr.info("Student Not Found");
        $location.path("/enrollment/students");
      }
      $scope.student = response.data;

    }, function(response){
      toastr.error(response);
    });
  }, function(response){
    toastr.error("Unable to load page");
  });



}]);
