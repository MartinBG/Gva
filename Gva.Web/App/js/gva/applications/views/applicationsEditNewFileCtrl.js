/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditNewFileCtrl(
    $scope,
    $state
    ) {
    $scope.cancel = function () {
      $scope.documentData = null;
      return $state.go('applications/edit/case');
    };

    $scope.addPart = function () {
      return $state.go('applications/edit/addpart');
    };
  }

  ApplicationsEditNewFileCtrl.$inject = [
    '$scope',
    '$state'
  ];

  angular.module('gva').controller('ApplicationsEditNewFileCtrl', ApplicationsEditNewFileCtrl);
}(angular
));
