var configurationServices = angular.module("configurationServices", []);

configurationServices.service("ConfigurationSvc", ["$http", function($http){
  function getSystemSetup(){
    return $http.get("/api/configuration/systemsetup");
  }
  
  function updateSystemSetup(setup){
    return $http.put("/api/configuration/systemsetup", setup);
  }
  return {
    getSystemSetup: getSystemSetup,
    updateSystemSetup: updateSystemSetup
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
