/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEducationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentEducations,
    edu
  ) {
    $scope.personDocumentEducation = edu;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newDocumentEducationForm.$validate()
        .then(function () {
          if ($scope.newDocumentEducationForm.$valid) {
            return PersonDocumentEducations
              .save({ id: $stateParams.id }, $scope.personDocumentEducation)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentEducations.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentEducations.search');
    };
  }

  DocumentEducationsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEducations',
    'edu'
  ];

  DocumentEducationsNewCtrl.$resolve = {
    edu: [
      '$stateParams',
      'PersonDocumentEducations',
      function ($stateParams, PersonDocumentEducations) {
        return PersonDocumentEducations.newEducation({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentEducationsNewCtrl', DocumentEducationsNewCtrl);
}(angular));
