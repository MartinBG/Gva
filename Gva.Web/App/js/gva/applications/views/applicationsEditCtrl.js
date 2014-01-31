/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditCtrl(
    $stateParams,
    $state,
    $scope,
    Application
    ) {
    //$scope.application = Application.get({ id: $stateParams.id });

    Application.get({ id: $stateParams.id }).$promise.then(function (application) {
      $scope.application = application;
    });

    $scope.documentData = {
      docPartType: null,
      docFiles: [],
      currentDocId: null,
      isLinkNew: false
    };

    $scope.viewPerson = function (id) {
      return $state.go('persons.view', { id: id });
    };
  }

  ApplicationsEditCtrl.$inject = [
    '$stateParams',
    '$state',
    '$scope',
    'Application'
  ];

  angular.module('gva').controller('ApplicationsEditCtrl', ApplicationsEditCtrl);
}(angular
));
