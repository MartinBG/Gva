/*global angular*/
(function (angular) {
  'use strict';

  function PersonApplicationCtrl($scope, $state) {
    $scope.viewApplication = function (id) {
      return $state.go('applications/edit/case', { id: id });
    };
  }

  PersonApplicationCtrl.$inject = ['$scope', '$state'];

  angular.module('gva').controller('PersonApplicationCtrl', PersonApplicationCtrl);
}(angular));
