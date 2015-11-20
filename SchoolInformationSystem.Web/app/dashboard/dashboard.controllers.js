var dashboardControllers = angular.module("dashboardControllers", ["dashboardServices", "chart.js", "common"]);

dashboardControllers.controller("DashboardCtrl", ["$scope", "DashboardSvc", "ChartData", "$rootScope", "$timeout", function($scope, DashboardSvc, ChartData, $rootScope, $timeout){
  $scope.message = "Hello Dashboard";
  $scope.assignmentGradeStats = [];

  $scope.dailyAssignmentGradeLabels = [];
  $scope.dailyAssignmentGradeData = [];
  $scope.dailyAssignmentGradesClick = function(points, evnt){
    var point = points[0];
    if(point){
      console.log(point.value);
    }
  }
  $scope.referralCountLabels = [];
  $scope.referralCountData = [];
  $scope.referralCountsClick = function(points, evnt){
    var point = points[0];
    if(point){
      console.log(point.value);
    }
  };

  $scope.attendanceLabels = [];
  $scope.attendanceData = [];
  $scope.attendanceClick = function(points, evnt){
    var point = points[0];
    if(point){
      console.log(point.value);
    }
  }

  function init() {
    DashboardSvc.getDashboardData().then(function (res) {
      var data = res.data;
      $scope.schoolId = $rootScope.currentSchoolId;
      $scope.dailyAssignmentGradeLabels = _.map(data.assignmentGrades, function (obj) { return obj.grade; });
      $scope.dailyAssignmentGradeData = _.map(data.assignmentGrades, function (obj) {
        return new ChartData(obj, { valueProperty: "recordCount" });
      });
      
      $scope.referralCountLabels = _.map(data.referralCounts, function (obj) { return obj.gradeLevel; });
      var refDat = [];
      refDat.push(_.map(data.referralCounts, function (obj) {
        return new ChartData(obj, { valueProperty: "numberOfReferrals" });
      }));
      $scope.referralCountData = refDat;

      $scope.attendanceLabels = _.map(data.attendance, function (obj) { return obj.day; });
      var attendanceDat = [];
      attendanceDat.push(_.map(data.attendance, function (obj) { return new ChartData(obj, { valueProperty: "count" }) }) )
      $scope.attendanceData = attendanceDat;
    }, function (err) {
      console.log(err);
    });
        
  }
  $scope.$on("sessionLoaded", init);
  $scope.$on("schoolChanged", init);
  


}]);
