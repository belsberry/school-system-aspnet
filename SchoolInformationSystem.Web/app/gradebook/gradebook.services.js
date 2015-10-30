var gradebookServices = angular.module("gradebookServices", []);

gradebookServices.service("GradebookSvc", ["$http", function($http){
  function getGradebookLandingPageData(){
    return $http.get("/api/gradebook");
  }


  function getClassAssignments(classRosterId){
    return $http.get("/api/gradebook/classRosters/" + classRosterId + "/assignments").then(function(response){
      response.data = response.data.assignments;
      return response;
    });
  }

  function getClassRosterList(){
    return $http.get("/api/gradebook/classRosters");
  }

  function getClassRosterDetail(classRosterId){
    return $http.get("/api/gradebook/classRosters/" + classRosterId);
  }

  function addClassRoster(classRoster){
    return $http.post("/api/gradebook/classRosters", classRoster);
  }

  function addAssignmentToClass(classId, assignment){
    return $http.post("/api/gradebook/classRosters/" + classId + "/assignments", assignment);
  }

  function deleteAssignment(classId, assignment){
    return $http.delete("/api/gradebook/classRosters/" + classId + "/assignments/" + assignment._id);
  }

  function addStudentToRoster(classId, student){
    return $http.post("/api/gradebook/classRosters/" + classId + "/students", student);
  }

  return {
    getGradebookLandingPageData: getGradebookLandingPageData,
    getClassAssignments: getClassAssignments,
    getClassRosterList: getClassRosterList,
    getClassRosterDetail: getClassRosterDetail,
    addClassRoster: addClassRoster,
    addAssignmentToClass: addAssignmentToClass,
    deleteAssignment: deleteAssignment,
    addStudentToRoster: addStudentToRoster
  }
}]);
