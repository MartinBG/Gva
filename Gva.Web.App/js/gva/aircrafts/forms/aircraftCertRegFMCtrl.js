/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertRegFMCtrl(
    $scope,
    Nomenclatures,
    scModal,
    gvaConstants
    ) {
    $scope.regMarkPattern = gvaConstants.regMarkPattern;

    $scope.newOwner = function () {
      if ($scope.model.ownerIsOrg) {
        var modalNewOrganization = scModal.open('newOrganization');

        modalNewOrganization.result.then(function (organizationId) {
          Nomenclatures.get({
            alias: 'organizations',
            id: organizationId
          })
          .$promise
          .then(function (organization) {
            $scope.model.ownerOrganization = organization;
          });
        });

        return modalNewOrganization.opened;
      } else {
        var modalNewOPerson = scModal.open('newPerson');

        modalNewOPerson.result.then(function (personId) {
          Nomenclatures.get({
            alias: 'persons',
            id: personId
          })
          .$promise
          .then(function (person) {
            $scope.model.ownerPerson = person;
          });
        });

        return modalNewOPerson.opened;
      }
    };
   
  }

  AircraftCertRegFMCtrl.$inject = [
    '$scope',
    'Nomenclatures',
    'scModal',
    'gvaConstants'
  ];

  angular.module('gva').controller('AircraftCertRegFMCtrl', AircraftCertRegFMCtrl);
}(angular));
