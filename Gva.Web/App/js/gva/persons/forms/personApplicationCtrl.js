/*global angular*/
(function (angular) {
  'use strict';

  function PersonApplicationCtrl($scope, $state) {
    $scope.viewApplication = function (id) {
      return $state.transitionTo('applications/edit/case', { id: id }, { reload: true });
    };
  }

  PersonApplicationCtrl.$inject = ['$scope', '$state'];

  angular.module('gva').controller('PersonApplicationCtrl', PersonApplicationCtrl);
}(angular));
