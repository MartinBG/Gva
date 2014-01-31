﻿/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditNewFileCtrl(
    $scope,
    $state,
    $stateParams
    ) {
    $scope.cancel = function () {
      return $state.transitionTo('applications/edit/case', $stateParams, { reload: true });
    };

    $scope.addPart = function () {
      return $state.go('applications/edit/addpart');
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
