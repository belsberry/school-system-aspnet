var dashboardControllers = angular.module("dashboardControllers", ["dashboardServices", "chart.js", "common"]);

dashboardControllers.controller("DashboardCtrl", ["$scope", "DashboardSvc", "ChartData", function($scope, DashboardSvc, ChartData){
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

  DashboardSvc.getDashboardData().then(function(res){
    var data = res.data;
    $scope.dailyAssignmentGradeLabels = _.map(data.dailyAssignmentGrades, function(obj){ return obj.grade; });
    $scope.dailyAssignmentGradeData = _.map(data.dailyAssignmentGrades, function(obj){
      return new ChartData(obj, { valueProperty: "recordCount" });
    });
    $scope.referralCountLabels = _.map(data.referralCounts, function(obj){ return obj.gradeLevel; });
    $scope.referralCountData.push(_.map(data.referralCounts, function(obj){
      return new ChartData(obj, { valueProperty: "numberOfReferrals"});
    }));

    $scope.attendanceLabels = _.map(data.attendance, function(obj){ return obj.day; });
    $scope.attendanceData.push(_.map(data.attendance, function(obj){ return new ChartData(obj, { valueProperty: "count" })}));

  }, function(err){
    console.log(err);
  });


}]);
