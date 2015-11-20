var dashboardServices = angular.module("dashboardServices", []);


dashboardServices.service("DashboardSvc", ["$http", "$rootScope", function ($http, $rootScope) {
  function getDashboardData() {
    if ($rootScope.currentSchoolId) {
      return $http.get("/api/dashboard/" + $rootScope.currentSchoolId);
    }
    else {
      return $http.get("/api/dashboard");
    }
  }
  return {
    getDashboardData: getDashboardData
  }
}]);
