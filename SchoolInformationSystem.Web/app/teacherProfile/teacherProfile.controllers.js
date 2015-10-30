var teacherProfileControllers = angular.module("teacherProfileControllers", ["teacherProfileServices", "toastr"]);

teacherProfileControllers.controller("TeacherProfileLandingCtrl", ["$scope", "TeacherProfileSvc", "toastr", "$rootScope", function($scope, TeacherProfileSvc, toastr, $rootScope){
  /**
   *  Properties
   */

  $scope.teacherProfile = {};
  $scope.averageObservationScore = 0;

  /**
   *  Init
   */
  TeacherProfileSvc.getTeacherProfile().then(function(response){
    var data = response.data;
    $scope.teacherProfile = data;

    if($scope.teacherProfile.completedObservations && $scope.teacherProfile.completedObservations.length > 0){
      //get total score
      var totals = _.reduce($scope.teacherProfile.completedObservations, function(total, obj){
        return total + obj.totalScore;
      }, 0.0);
      //get average score
      $scope.averageObservationScore = totals / $scope.teacherProfile.completedObservations.length;
    }
  }, function(response){
    console.log(response);
  });

  TeacherProfileSvc.addObservationScheduledCallback(function(obsv){
    $scope.teacherProfile.scheduledObservations.push(obsv);
    toastr.info("Observation Added", "An observation was scheduled");
    $rootScope.$apply();
  });


}]);
