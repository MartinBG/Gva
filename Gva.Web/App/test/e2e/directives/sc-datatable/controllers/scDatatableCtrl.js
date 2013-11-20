/*global angular*/
(function (angular) {
  'use strict';

  function ScDatatableCtrl($scope, $timeout) {
    function loadUsers() {
      $scope.users = [{
        username: 'admin',
        fullname: 'Administrator',
        roles: 'Role1, Role2',
        isActive: true
      }, {
        username: 'peter',
        fullname: 'Peter Ivanov',
        roles: 'Role1, Role2',
        isActive: true
      }, {
        username: 'georgi',
        fullname: 'Georgi Petrov',
        roles: 'Role1, Role2',
        isActive: true
      }, {
        username: 'test1',
        fullname: 'iztrit',
        roles: 'Role1, Role2',
        isActive: false
      }];

      $scope.users2 = $scope.users;
    }

    $timeout(loadUsers, 500);

    $scope.btnResult = '';
    $scope.loadMany = function(){
      for(var i = 0; i < 10; i++){
        $scope.users = $scope.users.concat($scope.users);
        $scope.users2 = $scope.users2.concat($scope.users2);
      }
    };

    $scope.editUser = function (user) {
      $scope.btnResult = user.username;
    };
  }

  ScDatatableCtrl.$inject = ['$scope', '$timeout'];

  angular.module('directive-tests').controller('directive-tests.ScDatatableCtrl', ScDatatableCtrl);
}(angular));
