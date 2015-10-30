var attendanceServices = angular.module("attendanceServices", []);

attendanceServices.service("AttendanceSvc", ["$http", function($http){
  function getAttendanceLandingPageData(){
    return $http.get("/api/attendance");
  }
  return {
    getAttendanceLandingPageData: getAttendanceLandingPageData
  }
}]);
