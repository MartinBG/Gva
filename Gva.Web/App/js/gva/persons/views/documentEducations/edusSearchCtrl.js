/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEducationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentEducation,
    edus
    ) {
    $scope.documentEducations = edus;

    $scope.editDocumentEducation = function (documentEducation) {
      return $state.go('root.persons.view.documentEducations.edit',
        {
          id: $stateParams.id,
          ind: documentEducation.partIndex
        });
    };

    $scope.newDocumentEducation = function () {
      return $state.go('root.persons.view.documentEducations.new');
    };
  }

  DocumentEducationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEducation',
    'edus'
  ];

  DocumentEducationsSearchCtrl.$resolve = {
    edus: [
      '$stateParams',
      'PersonDocumentEducation',
      function ($stateParams, PersonDocumentEducation) {
        return PersonDocumentEducation.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentEducationsSearchCtrl', DocumentEducationsSearchCtrl);
}(angular));
