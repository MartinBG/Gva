/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEducationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
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
    'edus'
  ];

  DocumentEducationsSearchCtrl.$resolve = {
    edus: [
      '$stateParams',
      'PersonDocumentEducations',
      function ($stateParams, PersonDocumentEducations) {
        return PersonDocumentEducations.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentEducationsSearchCtrl', DocumentEducationsSearchCtrl);
}(angular));
