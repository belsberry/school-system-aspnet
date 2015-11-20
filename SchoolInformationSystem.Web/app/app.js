var app = angular.module("app",[
  "ngRoute",
  "dashboardControllers",
  "gradebookControllers",
  "attendanceControllers",
  "enrollmentControllers",
  "teacherProfileControllers",
  "userControllers",
  "ui.bootstrap",
  "userServices",
  "configurationControllers",
  "commonConstants"
]);


app.run(["$rootScope", "UserSvc", "SCOPE_LEVELS", "SchoolSvc", "$route", function($rootScope, UserSvc, SCOPE_LEVELS, SchoolSvc, $route){
  $rootScope.userName = "";
  $rootScope.$on("$locationChangeStart", function(){
    $rootScope.navbarIsVisible = false; //Set navbar invisible
  });

  $rootScope.currentSchoolId = null;

  /**
   * Get basic user information
   */
  SchoolSvc.getSchools().then(function(response){
     $rootScope.schools = response.data;
     $rootScope.schools.splice(0,0, { id: null, name: "All Schools" });
     return UserSvc.getCurrentSchool();
  }).then(function(response){    
    $rootScope.currentSchoolId = response.data.currentSchoolID;   
    return UserSvc.getUserInfo();
  }).then(function(user){
    $rootScope.userName = user.userName;
    $rootScope.school = user.school;
    $rootScope.canSelectSchool = user.scopeLevel >= SCOPE_LEVELS.DISTRICT.value ? true : false;
    $rootScope.$broadcast("sessionLoaded");
  });

  $rootScope.schoolChanged = function(){
    UserSvc.setCurrentSchool($rootScope.currentSchoolId).then(function () {
      $rootScope.$broadcast("schoolChanged");
    });
  }

}]);

app.config(["$routeProvider", function($routeProvider){
  $routeProvider.when("/", {
    templateUrl: "app/dashboard/dashboard.template.html",
    controller: "DashboardCtrl"
  }).when("/gradebook", {
    templateUrl: "app/gradebook/gradebookLanding.template.html",
    controller: "GradebookLandingCtrl"
  }).when("/gradebook/assignments/:classId?", {
    templateUrl: "app/gradebook/gradebookAssignments.template.html",
    controller: "GradebookAssignmentsCtrl"
  }).when("/gradebook/classRoster", {
    templateUrl: "app/gradebook/gradebookClassRosterList.template.html",
    controller: "GradebookClassRosterListCtrl"
  }).when("/gradebook/classRoster/:classRosterId", {
    templateUrl: "app/gradebook/gradebookClassRosterDetail.template.html",
    controller: "GradebookClassRosterDetailCtrl"
  }).when("/gradebook/classRoster/:classId/students", {
    templateUrl: "app/gradebook/gradebookClassRosterStudentList.template.html",
    controller: "GradebookClassRosterStudentListCtrl"
  }).when("/attendance", {
    templateUrl: "app/attendance/attendanceLanding.template.html",
    controller: "AttendanceLandingCtrl"
  }).when("/teacherProfile", {
    templateUrl: "app/teacherProfile/teacherProfileLanding.template.html",
    controller: "TeacherProfileLandingCtrl"
  }).when("/configuration", {
    templateUrl: "app/configuration/configurationIndex.template.html",
    controller: "ConfigurationIndexCtrl"
  }).when("/enrollment", {
    templateUrl: "app/enrollment/enrollmentLanding.template.html",
    controller: "EnrollmentLandingCtrl"
  }).when("/enrollment/students", {
    templateUrl: "app/enrollment/enrollmentStudentList.template.html",
    controller: "EnrollmentStudentListCtrl"
  }).when("/enrollment/students/:studentId", {
    templateUrl: "app/enrollment/enrollmentStudentEdit.template.html",
    controller: "EnrollmentStudentEditCtrl"
  }).when("/enrollment/enroll", {
    templateUrl: "app/enrollment/enrollmentNewStudent.template.html",
    controller: "EnrollmentNewStudentCtrl"
  }).when("/userprofile", {
    templateUrl: "app/user/userProfile.template.html",
    controller: "UserProfileCtrl"
  }).when("/configuration/schoollist", {
    templateUrl: "app/configuration/schoolList.template.html",
    controller: "SchoolListCtrl"
  }).when("/configuration/userlist", {
    templateUrl: "app/configuration/userList.template.html",
    controller: "ConfigurationUserListCtrl"
  }).when("/configuration/systemsetup", {
    templateUrl: "app/configuration/systemSetup.template.html",
    controller: "SystemSetupCtrl"
  }).otherwise({
    redirectTo: "/"
  });
}]);


app.factory("httpRequestInterceptor", ["$location", function($location){
  return {
    "responseError": function(res, two, three){
      if(res.status == 401){
        console.log(res);
        var currentPath = $location.path();
        window.location = "/auth/login?next=" + currentPath;
      }
    }
  }
}])
app.config(["$httpProvider", "$injector", function($httpProvider, $injector){
  $httpProvider.interceptors.push("httpRequestInterceptor");
}]);
