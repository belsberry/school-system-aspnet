var gradebookControllers = angular.module("gradebookControllers", ["gradebookServices", "common", "enrollmentServices"]);

gradebookControllers.controller("GradebookLandingCtrl", ["$scope", "GradebookSvc", "ChartData", function($scope, GradebookSvc, ChartData){
  $scope.title = "This is the gradebook landing page";

  $scope.openAssignments = [];
  $scope.recentAssignments = [];
  $scope.classRosterAverages = [];

  $scope.viewAssignment = function(assignment){
    console.log(assignment);
  }

  GradebookSvc.getGradebookLandingPageData().then(function(response){
    var data = response.data;
    $scope.openAssignments = data.openAssignments;

    $scope.recentAssignments = data.recentAssignments;


    $scope.classRosterAverages = _.map(data.quickRosterAverages, function(obj){
      obj.classRosterAverageChartLabels = _.map(obj.averages, function(avg){
        return avg.gradeLetter;
      });
      obj.classRosterAverageChartData = _.map(obj.averages, function(avg){
        return new ChartData(avg, { valueProperty: "numberOfStudents" });
      });

      return obj;
    });


  }, function(response){
    console.log(response);
  });

}]);



gradebookControllers.controller("GradebookAssignmentsCtrl", ["$scope", "GradebookSvc", "$routeParams", "$modal", "toastr", "$window", function($scope, GradebookSvc, $routeParams, $modal, toastr, $window){
  $scope.classId = $routeParams.classId;
  $scope.classAssignments = [];
  $scope.showClassAssignments = false;
  $scope.selectedClass = {};
  $scope.classes = [];

  $scope.selectClass = function(classData){
    $scope.classId = classData._id;
    $scope.selectedClass = classData;
    loadClassAssignments();
  }

  $scope.openAddAssignment = function(){
    var modalInstance = $modal.open({
      size: "lg",
      animation: true,
      templateUrl: "app/gradebook/classAssignmentModal.template.html",
      controller: "GradebookClassAssignmentAddModalCtrl",
      resolve:{
        classId: function(){
          return $scope.classId;
        }
      }
    });

    modalInstance.result.then(function(classAssignment){
      $scope.classAssignments.push(classAssignment);
    }, function(){
      //cancelled logic
    });
  }

  function loadClasses(){
    GradebookSvc.getClassRosterList().then(function(response){
      var data = response.data;
      $scope.classes = data;
    }, function(response){
      console.log(response);
    });
  }

  function loadClassAssignments(){
    if($scope.classId){
      GradebookSvc.getClassAssignments($scope.classId).then(function(response){
        var data = response.data;
        $scope.classAssignments = data;
        $scope.showClassAssignments = true;
      }, function(response){
        console.log(response);
      });
    }else{
      $scope.showClassAssignments = false;
      $scope.classAssignments = [];
    }
  }

  $scope.deleteAssignment = function(assignment){
    if($window.confirm("Are you sure you want to delete " + assignment.description + "?" )){
      GradebookSvc.deleteAssignment($scope.classId, assignment).then(function(response){
        $scope.classAssignments = _.reject($scope.classAssignments, function(ass){ return ass._id == assignment._id; });
      }, function(response){
        toastr.error("Error removing assignment");
      });
    }
  }

  function init(){
    loadClasses();
    loadClassAssignments();
  }
  init();
}]);



gradebookControllers.controller("GradebookClassRosterListCtrl", ["$scope", "GradebookSvc", "$modal", function($scope, GradebookSvc, $modal){
  /**
   *  Properties
   */
   $scope.classRosters = [];

   /**
    * Methods
    */
  $scope.openAddClassRoster = function(){
    var modalInstance = $modal.open({
      animation: true,
      templateUrl: "app/gradebook/gradebookAddRosterModal.template.html",
      controller: "GradebookAddClassRosterCtrl",
      size: "lg"
    });

    modalInstance.result.then(function(newRoster){
      $scope.classRosters.push(newRoster);
    }, function(){
      //No roster added
    });
  }

  /**
   * Init
   */

  GradebookSvc.getClassRosterList().then(function(response){
    $scope.classRosters = response.data;
  }, function(response){
    console.log(response);
  });

}]);

gradebookControllers.controller("GradebookClassRosterDetailCtrl", ["$scope", "GradebookSvc", "$location", "$routeParams", "StudentSvc", function($scope, GradebookSvc, $location, $routeParams, StudentSvc){
  /**
   *  Properties
   */
  $scope.classRoster = {};
  $scope.possibleStudents = [];

  /**
   *  Methods
   */

   $scope.searchStudents = function(){
     //TODO for now leaving out the search string to get all students
     StudentSvc.doStudentSearch().then(function(response){
      $scope.possibleStudents = response.data;
     });
   }

   $scope.addStudentToRoster = function(student){
     GradebookSvc.addStudentToRoster($scope.classRoster, student).then(function(response){
       //Success
       $scope.classRoster.students.push(student);
       $scope.newStudent = {};
     }, function(response){
       console.log(response);
     });
   }

  /**
   * Init
   */
  GradebookSvc.getClassRosterDetail($routeParams.classRosterId).then(function(response){
    $scope.classRoster = response.data;
  }, function(response){
    console.log(response);
  });

}]);


gradebookControllers.controller("GradebookAddClassRosterCtrl", ["$scope", "$modalInstance", "GradebookSvc", function($scope, $modalInstance, GradebookSvc){
  $scope.newRoster = {};

  $scope.periods = [{
    periodId: null,
    description: "-- Select a Class Period --"
  },{
    periodId: 1,
    description: "1st Period"
  },{
    periodId: 2,
    description: "2nd Period"
  }];

  $scope.save = function(){
    if($scope.form.$valid){
      GradebookSvc.addClassRoster($scope.newRoster).then(function(response){
        $modalInstance.close(response.data);
      });
    }
  }
  $scope.cancel = function(){
    $modalInstance.dismiss("cancel");
  }

}]);

gradebookControllers.controller("GradebookClassAssignmentAddModalCtrl", ["$scope", "$modalInstance", "GradebookSvc", "classId", function($scope, $modalInstance, GradebookSvc, classId){
  $scope.classId = classId;
  $scope.newAssignment = {
    // description: String,
    // openDate: Date,
    // dueDate: Date,
    // averageGrade: Number
  };

  $scope.errorMessage = null;

  $scope.save = function(){
    if($scope.form.$valid){
      GradebookSvc.addAssignmentToClass($scope.classId, $scope.newAssignment).then(function(response){
        $modalInstance.close(response.data);
      }, function(response){
        $scope.errorMessage = "Unable to save assignment";
      });
    }
  }

  $scope.cancel = function(){
    $modalInstance.dismiss("cancel");
  }

}]);


gradebookControllers.controller("GradebookClassRosterStudentListCtrl", ["$scope", "$modal", "GradebookSvc", "$routeParams", "toastr", function($scope, $modal, GradebookSvc, $routeParams, toastr){
  /**
  * Properties
  */
  $scope.classId = $routeParams.classId;
  $scope.classRoster = {};

  /**
  * Methods
  */

  $scope.openAddStudent = function(){
    var modalInstance = $modal.open({
      size: "lg",
      animation: true,
      templateUrl: "app/gradebook/addStudentModal.template.html",
      controller: "GradebookClassRosterAddStudentModalCtrl",
      resolve: {
        classRoster: function(){
          return $scope.classRoster;
        }
      }
    });

    modalInstance.result.then(function(student){
      $scope.classRoster.students.push(student);
      console.log($scope.classRoster);
    });
  }

  /**
  * Init
  */
  GradebookSvc.getClassRosterDetail($scope.classId).then(function(response){
    $scope.classRoster = response.data;
  }, function(response){
    toastr.error("Unable to load class roster");
  })
}]);


gradebookControllers.controller("GradebookClassRosterAddStudentModalCtrl", ["$scope", "$modalInstance", "StudentSvc", "classRoster", "GradebookSvc", function($scope, $modalInstance, StudentSvc, classRoster, GradebookSvc){
  $scope.searchString = "";
  $scope.students = [];
  var studentIds = _.map(classRoster.students, function(stu){ return stu._id; });
  var classRoster = classRoster;

  /**
  * Methods
  */
  $scope.doSearch = function(){

    StudentSvc.doStudentSearch($scope.searchString).then(function(response){
      $scope.students = _.map(response.data, function(stu){
        stu.isAlreadyAssigned = _.contains(studentIds, stu._id);
        return stu;
      });
    }, function(response){
      $scope.errorMessage = "Unable to load student list";
    });
  }

  $scope.addStudentToRoster = function(student){
    GradebookSvc.addStudentToRoster(classRoster._id, student).then(function(response){
      $modalInstance.close(student);

    }, function(response){
      $scope.errorMessage = "Unable to add student";
    });
  }

  $scope.close = function(){
    $modalInstance.dismiss("close");
  }

  /**
  * Init
  */
  $scope.doSearch();
}]);
