/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditCtrl(
    $stateParams,
    $state,
    $scope,
    Application
    ) {
    $scope.application = Application.get({ id: $stateParams.id });

    $scope.documentData = {
      docPartType: null,
      docFileId: null,
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
