/*global angular*/
(function (angular) {
  'use strict';

  function DocApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    personDocumentApplications
  ) {
    $scope.personDocumentApplications = personDocumentApplications;
  }

  DocApplicationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'personDocumentApplications'
  ];

  DocApplicationsSearchCtrl.$resolve = {
    personDocumentApplications: [
      '$stateParams',
      'PersonDocumentApplications',
      function ($stateParams, PersonDocumentApplications) {
        return PersonDocumentApplications.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocApplicationsSearchCtrl', DocApplicationsSearchCtrl);
}(angular));
