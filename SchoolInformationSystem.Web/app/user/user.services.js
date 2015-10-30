var userServices = angular.module("userServices", []);

userServices.service("UserSvc", ["$http", "$q", function($http, $q){

  function getUserInfo(){
    return $http.get("/api/auth/profile").then(function(response){ return response.data; });
  }

  function updateUserProfile(user){
    return $http.post("/api/auth/profile", user);
  }

  function setNewPassword(pwd){
    return $http.post("/api/auth/setPassword", { newPassword: pwd });
  }

  function createUser(user){
    return $http.post("/api/auth/users", user);
  }

  function searchUsers(search){
    return $http.get("/api/auth/users", { params: search });
  }

  function getCurrentSchool(){
    return $http.get("/api/auth/session/currentSchoolId");
  }

  function setCurrentSchool(schoolId){
    return $http.post("/api/auth/session/currentSchoolId", { currentSchoolId: schoolId });
  }

  return {
    getUserInfo: getUserInfo,
    updateUserProfile: updateUserProfile,
    setNewPassword: setNewPassword,
    createUser: createUser,
    searchUsers: searchUsers,
    getCurrentSchool: getCurrentSchool,
    setCurrentSchool: setCurrentSchool
  }
}])
