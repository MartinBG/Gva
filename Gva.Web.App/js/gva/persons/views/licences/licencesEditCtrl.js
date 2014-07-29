/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicencesEditCtrl(
    $scope,
    $state,
    $stateParams,
    namedModal,
    PersonLicences,
    licence,
    scMessage
  ) {
    var originalLicence = _.cloneDeep(licence);
    $scope.licence = licence;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.$watch('licence.part.editions | last', function (lastEdition) {
      $scope.currentEdition = lastEdition;
      $scope.lastEdition = lastEdition;
    });

    $scope.selectEdition = function (item) {
      $scope.currentEdition = item;
    };

    $scope.newEdition = function () {
      $scope.licence.part.editions.push({});

      $scope.editMode = 'edit';
    };

    $scope.editLastEdition = function () {
      $scope.editMode = 'edit';
    };

    $scope.deleteLastEdition = function () {
      $scope.licence.part.editions.pop();

      if ($scope.licence.part.editions.length === 0) {
        return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            return PersonLicences
              .remove({ id: $stateParams.id, ind: $stateParams.ind })
              .$promise.then(function () {
                return $state.go('root.persons.view.licences.search');
              });
          }
        });
      }
      else {
        return PersonLicences.save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.licence)
          .$promise;
      }
    };

    $scope.save = function () {
      return $scope.editLicenceForm.$validate()
        .then(function () {
          if ($scope.editLicenceForm.$valid) {
            $scope.editMode = 'saving';
            return PersonLicences
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.licence).$promise
              .then(function () {
                $scope.editMode = null;
                originalLicence = _.cloneDeep($scope.licence);
              }, function () {
                $scope.editMode = 'edit';
              });
          }
        });
    };

    $scope.cancel = function () {
      $scope.licence = _.cloneDeep(originalLicence);
      $scope.editMode = null;
    };

    $scope.viewStatuses = function () {
      var params = {
        licence: $scope.licence,
        personId: $stateParams.id,
        licenceInd: $stateParams.ind
      };

      var modalInstance = namedModal.open('licenceStatuses', params);

      modalInstance.result.then(function () {
        $state.transitionTo($state.current, $stateParams, { reload: true });
      });

      return modalInstance.opened;
    };
  }

  LicencesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'namedModal',
    'PersonLicences',
    'licence',
    'scMessage'
  ];

  LicencesEditCtrl.$resolve = {
    licence: [
      '$stateParams',
      'PersonLicences',
      function ($stateParams, PersonLicences) {
        return PersonLicences.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('LicencesEditCtrl', LicencesEditCtrl);
}(angular, _));
