/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditCaseCtrl(
    $scope,
    $state
    ) {
    $scope.test = $state;
  }

  ApplicationsEditCaseCtrl.$inject = [
    '$scope',
    '$state'
  ];

  angular.module('gva').controller('ApplicationsEditCaseCtrl', ApplicationsEditCaseCtrl);
}(angular));
