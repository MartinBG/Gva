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


    $scope.editApplication = function (application) {
      return $state.go('root.persons.view.documentApplications.edit', {
        id: $stateParams.id,
        ind: application.partIndex
      });
    };

    $scope.newApplication = function () {
      return $state.go('root.persons.view.documentApplications.new');
    };
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
