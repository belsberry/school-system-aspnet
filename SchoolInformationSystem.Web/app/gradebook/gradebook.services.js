var gradebookServices = angular.module("gradebookServices", []);

gradebookServices.service("GradebookSvc", ["$http", function($http){
  function getGradebookLandingPageData(){
    return $http.get("/api/gradebook");
  }


  function getClassAssignments(classRosterId){
    return $http.get("/api/gradebook/classRosters/" + classRosterId + "/assignments");
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
    return $http.delete("/api/gradebook/classRosters/" + classId + "/assignments/" + assignment.id);
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
