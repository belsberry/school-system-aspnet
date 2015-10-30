var configurationServices = angular.module("configurationServices", []);

configurationServices.service("ConfigurationSvc", ["$http", function($http){
  return {
    //TODO put anything in here.
  }
}]);


configurationServices.service("SchoolSvc", ["$http", function($http){
  function getSchools(){
    return $http.get("/api/configuration/schools");
  }

  function addSchool(school){
    return $http.post("/api/configuration/schools", school);
  }
  return {
    getSchools: getSchools,
    addSchool: addSchool
  }
}]);
