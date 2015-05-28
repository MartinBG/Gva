/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AircraftCertRadioCtrl(
    $scope,
    Nomenclatures,
    Aircrafts,
    scModal,
    scMessage,
    scFormParams
    ) {
    $scope.lotId = scFormParams.lotId;
    $scope.partIndex = scFormParams.partIndex;

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

    var getNomenclatureData = function (alias, id) {
      return Nomenclatures.get({
        alias: alias,
        id: id
      }).$promise;
    };

    $scope.newOwnerOper = function () {
      if ($scope.model.ownerOperIsOrg) {
        var modalNewOrganization = scModal.open('newOrganization');

        modalNewOrganization.result.then(function (organizationId) {
          getNomenclatureData('organizations', organizationId)
            .then(function (organization) {
              $scope.model.ownerOper = organization;
            });
        });

        return modalNewOrganization.opened;
      } else {
        var modalNewOPerson = scModal.open('newPerson');

        modalNewOPerson.result.then(function (personId) {
          getNomenclatureData('persons', personId)
            .then(function (person) {
              $scope.model.ownerOper = person;
            });
        });

        return modalNewOPerson.opened;
      }
    };

    $scope.addNewEntry = function () {
      var modalInstance = scModal.open('manageRadioEntry');

      modalInstance.result.then(function (newEntry) {
        $scope.model.entries.push(newEntry);
      });

      return modalInstance.opened;
    };

    $scope.removeEntry = function (entry) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            _.remove($scope.model.entries,
              function(equipment) {
                return equipment === entry;
              });
          }
        });
    };

    $scope.editEntry = function (entry) {
      var modalInstance = scModal.open('manageRadioEntry', {entry: _.cloneDeep(entry)});
      var index =_.findIndex($scope.model.entries, entry);
      modalInstance.result.then(function (entry) {
        $scope.model.entries[index] = entry;
      });
      return modalInstance.opened;
    };

    $scope.isUniqueFormNumber = function () {
       if ($scope.model.aslNumber) {
        return Aircrafts.isUniqueFormNumber({
          formName: 'radio',
          formNumber: $scope.model.aslNumber,
          lotId: $scope.lotId,
          partIndex: $scope.partIndex
        })
        .$promise
        .then(function (result) {
          return result.isUnique;
        });
      } else {
        return true;
       }
    };
  }

  AircraftCertRadioCtrl.$inject = [
    '$scope',
    'Nomenclatures',
    'Aircrafts',
    'scModal',
    'scMessage',
    'scFormParams'
  ];

  angular.module('gva').controller('AircraftCertRadioCtrl', AircraftCertRadioCtrl);
}(angular, _));
