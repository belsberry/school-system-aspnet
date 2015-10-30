var enrollmentServices = angular.module("enrollmentServices", []);
enrollmentServices.service("StudentSvc", ["$http", "$rootScope", function($http, $rootScope){

  function doStudentSearch(searchString){
    return $http.get("/api/students?search=" + searchString);
  }

  function getStudentRecord(studentId){
    return $http.get("/api/students/" + studentId).then(function(res){
      res.data.dob = new Date(res.data.dob);
      return res;
    });
  }

  function enrollStudent(student){
    return $http.post("/api/students", student);
  }

  function getStudentDomainData(){
    return $http.get("/api/domain/students");
  }


  function updateStudent(student){
    return $http.post("/api/students/" + student._id, student);
  }

  return {
    doStudentSearch: doStudentSearch,
    getStudentRecord: getStudentRecord,
    enrollStudent: enrollStudent,
    getStudentDomainData: getStudentDomainData,
    updateStudent: updateStudent
  };
}]);


enrollmentServices.service("EnrollmentSvc", ["$http", function($http){
  function getStudentPopulations(){
    return $http.get("/api/enrollment/studentPopulations");
  }

  return {
    getStudentPopulations: getStudentPopulations

  }
}]);
