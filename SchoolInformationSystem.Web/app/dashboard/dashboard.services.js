var dashboardServices = angular.module("dashboardServices", []);


dashboardServices.service("DashboardSvc", ["$http", function($http){
  function getDashboardData(){
    return $http.get("/api/dashboard");
  }
  return {
    getDashboardData: getDashboardData
  }
}]);
