var userControllers = angular.module("userControllers", ["userServices", "toastr"]);


userControllers.controller("UserProfileCtrl", ["$scope", "UserSvc", "toastr", function($scope, UserSvc, toastr){
  $scope.userData = {};

  UserSvc.getUserInfo().then(function(user){
    $scope.userData = user;
  });

  $scope.setNewPassword = function(pwd){
    if($scope.pwdForm.$valid){
      UserSvc.setNewPassword(pwd).then(function(){
        toastr.success("New Password Set");
        $scope.pwd = "";
        $scope.pwdForm.$setPristine();
      }, function(){
        toastr.error("Error setting New Password");
      });
    }
  }

  $scope.saveUser = function(){
    if($scope.form.$valid){
      UserSvc.updateUserProfile($scope.userData).then(function(){
        toastr.success("Updated User Profile");
      }, function(){
        toastr.error("Error Updating Profile");
      });
    }
  }
}]);
