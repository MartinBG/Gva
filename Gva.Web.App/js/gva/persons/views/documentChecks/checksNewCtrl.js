/*global angular*/
(function (angular) {
  'use strict';

  function DocumentChecksNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentChecks,
    personDocumentCheck
  ) {
    $scope.personDocumentCheck = personDocumentCheck;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newDocumentCheckForm.$validate()
        .then(function () {
          if ($scope.newDocumentCheckForm.$valid) {
            return PersonDocumentChecks
              .save({ id: $stateParams.id }, $scope.personDocumentCheck)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.checks.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.checks.search');
    };
  }

  DocumentChecksNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentChecks',
    'personDocumentCheck'
  ];

  DocumentChecksNewCtrl.$resolve = {
    personDocumentCheck: [
      '$stateParams',
      'PersonDocumentChecks',
      function ($stateParams, PersonDocumentChecks) {
        return PersonDocumentChecks.newCheck({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentChecksNewCtrl', DocumentChecksNewCtrl);
}(angular));
