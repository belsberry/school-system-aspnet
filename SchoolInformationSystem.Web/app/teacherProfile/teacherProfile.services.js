var teacherProfileServices = angular.module("teacherProfileServices", []);


teacherProfileServices.service("TeacherProfileSvc", ["$http", function($http){

  var socket = io();

  function getTeacherProfile(){
    return $http.get("/api/teacherProfile/1");
  }

  function addObservationScheduledCallback(cb){
    socket.removeListener("observationScheduled");
    socket.on("observationScheduled", function(data){
      if(cb){
        cb(data);
      }
    });
  }

  return {
    getTeacherProfile: getTeacherProfile,
    addObservationScheduledCallback: addObservationScheduledCallback
  }
}]);
