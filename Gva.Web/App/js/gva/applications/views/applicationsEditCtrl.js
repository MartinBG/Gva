/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditCtrl(
    $stateParams,
    $state,
    $scope,
    Application,
    application
    ) {
    $scope.application = application;

    $scope.documentData = {
      docPartType: null,
      docFiles: [],
      currentDocId: null,
      isLinkNew: false
    };

    $scope.viewPerson = function (id) {
      return $state.go('root.persons.view', { id: id });
    };
  }

  ApplicationsEditCtrl.$inject = [
    '$stateParams',
    '$state',
    '$scope',
    'Application',
    'application'
  ];

  ApplicationsEditCtrl.$resolve = {
    application: [
      '$stateParams',
      'Application',
      function ResolveApplication($stateParams, Application) {
        return Application.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApplicationsEditCtrl', ApplicationsEditCtrl);
}(angular
));
