var attendanceControllers = angular.module("attendanceControllers", []);

attendanceControllers.controller("AttendanceLandingCtrl", ["$scope", function($scope){
  $scope.title = "This is the Attendance landing page";
}]);
