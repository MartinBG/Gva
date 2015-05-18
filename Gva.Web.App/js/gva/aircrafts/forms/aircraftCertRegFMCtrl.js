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

    var getNomenclatureData = function (alias, id) {
      return Nomenclatures.get({
        alias: alias,
        id: id
      }).$promise;
    };

    $scope['new'] = function (fieldName) {
      var isOrg = fieldName !== 'lessor' ? 
        $scope.model[fieldName + 'IsOrg'] : $scope.model.lessorType === 'organization';
      if (isOrg) {
        var modalNewOrganization = scModal.open('newOrganization');

        modalNewOrganization.result.then(function (organizationId) {
          getNomenclatureData('organizations', organizationId)
            .then(function (organization) {
              $scope.model[fieldName + 'Organization'] = organization;
            });
        });

        return modalNewOrganization.opened;
      } else {
        var modalNewOPerson = scModal.open('newPerson');

        modalNewOPerson.result.then(function (personId) {
          getNomenclatureData('persons', personId)
            .then(function (person) {
              $scope.model[fieldName + 'Person'] = person;
            });
        });

        return modalNewOPerson.opened;
      }
    };

    $scope.$watch('model.inspector', function (inspectorModel) {
      if (!inspectorModel) {
        return;
      }

      if (inspectorModel.inspector) {
        $scope.inspectorType = 'inspector';
      } else if (inspectorModel.other) {
        $scope.inspectorType = 'other';
      }
    });
  }

  AircraftCertRegFMCtrl.$inject = [
    '$scope',
    'Nomenclatures',
    'scModal',
    'gvaConstants'
  ];

  angular.module('gva').controller('AircraftCertRegFMCtrl', AircraftCertRegFMCtrl);
}(angular));
