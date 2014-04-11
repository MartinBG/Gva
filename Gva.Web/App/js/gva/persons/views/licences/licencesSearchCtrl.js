/*global angular*/
(function (angular) {
  'use strict';

  function LicencesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicence,
    licences
  ) {
    $scope.licences = licences;

    $scope.viewLicence = function (licence) {
      return $state.go('root.persons.view.licences.edit', {
        id: $stateParams.id,
        ind: licence.partIndex
      });
    };

    $scope.newLicence = function () {
      return $state.go('root.persons.view.licences.new');
    };
  }

  LicencesSearchCtrl.$inject =
    ['$scope', '$state', '$stateParams', 'PersonLicence', 'licences'];

  LicencesSearchCtrl.$resolve = {
    licences: [
      '$stateParams',
      'PersonLicence',
      function ($stateParams, PersonLicence) {
        return PersonLicence.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('LicencesSearchCtrl', LicencesSearchCtrl);
}(angular));
