/*global angular*/
(function (angular) {
  'use strict';

  function DocApplicationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentApplication,
    personDocumentApplication
  ) {

    $scope.personDocumentApplication = personDocumentApplication;

    $scope.save = function () {
      $scope.personDocumentApplicationForm.$validate()
         .then(function () {
            if ($scope.personDocumentApplicationForm.$valid) {
              return PersonDocumentApplication
              .save({ id: $stateParams.id }, $scope.personDocumentApplication).$promise
              .then(function () {
                return $state.go('root.persons.view.documentApplications.search');
              });
            }
          });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentApplications.search');
    };
  }

  DocApplicationsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentApplication',
    'personDocumentApplication'
  ];
  DocApplicationsNewCtrl.$resolve = {
    personDocumentApplication: function () {
      return {
        part: {},
        files: {
          hideApplications: true,
          files: []
        }
      };
    }
  };

  angular.module('gva').controller('DocApplicationsNewCtrl', DocApplicationsNewCtrl);
}(angular));
