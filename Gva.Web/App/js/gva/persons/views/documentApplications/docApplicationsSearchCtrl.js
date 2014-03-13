/*global angular*/
(function (angular) {
  'use strict';

  function DocApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentApplication,
    personDocumentApplications
  ) {
    $scope.personDocumentApplications = personDocumentApplications;


    $scope.editApplication = function (application) {
      return $state.go('root.persons.view.documentApplications.edit', {
        id: $stateParams.id,
        ind: application.partIndex
      });
    };

    $scope.deleteApplication = function (application) {
      return PersonDocumentApplication.remove({ id: $stateParams.id, ind: application.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'PersonDocumentApplication',
    'personDocumentApplications'
  ];

  DocApplicationsSearchCtrl.$resolve = {
    personDocumentApplications: [
      '$stateParams',
      'PersonDocumentApplication',
      function ($stateParams, PersonDocumentApplication) {
        return PersonDocumentApplication.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocApplicationsSearchCtrl', DocApplicationsSearchCtrl);
}(angular));
