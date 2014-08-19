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
