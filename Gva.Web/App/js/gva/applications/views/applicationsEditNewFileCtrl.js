/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditNewFileCtrl(
    $scope,
    $state,
    $stateParams
    ) {
    $scope.cancel = function () {
      return $state.transitionTo('root.applications.edit.case', $stateParams, { reload: true });
    };

    $scope.addPart = function () {
      $scope.addDocPartType.$validate()
        .then(function () {
          if ($scope.addDocPartType.$valid) {
            return $state.go('root.applications.edit.addpart');
          }
        });
    };
  }

  ApplicationsEditNewFileCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams'
  ];

  angular.module('gva').controller('ApplicationsEditNewFileCtrl', ApplicationsEditNewFileCtrl);
}(angular
));
