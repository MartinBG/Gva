/*global angular*/
(function (angular) {
  'use strict';

  function AircraftDocumentDebtFMCtrl($scope, Nomenclatures, scModal, scFormParams) {
    $scope.lotId = scFormParams.lotId;
    $scope.newApplicant = function () {
      var modalNewOrganization = scModal.open('newOrganization');

      modalNewOrganization.result.then(function (organizationId) {
        Nomenclatures.get({
          alias: 'organizations',
          id: organizationId
        })
          .$promise
          .then(function (organization) {
            $scope.model.aircraftApplicant = organization;
          });
      });

      return modalNewOrganization.opened;
    };
  }

  AircraftDocumentDebtFMCtrl.$inject = ['$scope', 'Nomenclatures', 'scModal', 'scFormParams'];

  angular.module('gva').controller('AircraftDocumentDebtFMCtrl', AircraftDocumentDebtFMCtrl);
}(angular));
