/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertSmodCtrl($scope, Nomenclatures, scModal) {
    var getNomenclatureData = function (alias, id) {
      return Nomenclatures.get({
        alias: alias,
        id: id
      }).$promise;
    };

    $scope.newApplicant = function () {
      if ($scope.model.applicantIsOrg) {
        var modalNewOrganization = scModal.open('newOrganization');

        modalNewOrganization.result.then(function (organizationId) {
          getNomenclatureData('organizations', organizationId)
            .then(function (organization) {
              $scope.model.applicantOrganization = organization;
            });
        });

        return modalNewOrganization.opened;
      } else {
        var modalNewOPerson = scModal.open('newPerson');

        modalNewOPerson.result.then(function (personId) {
          getNomenclatureData('persons', personId)
            .then(function (person) {
              $scope.model.applicantPerson = person;
            });
        });

        return modalNewOPerson.opened;
      }
    };
  }

  AircraftCertSmodCtrl.$inject = ['$scope', 'Nomenclatures', 'scModal'];

  angular.module('gva').controller('AircraftCertSmodCtrl', AircraftCertSmodCtrl);
}(angular));
