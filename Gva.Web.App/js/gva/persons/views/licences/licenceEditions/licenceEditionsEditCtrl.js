/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicenceEditions,
    licence,
    currentLicenceEdition,
    licenceEditions,
    scMessage,
    scModal
  ) {
    var originalLicenceEdition = _.cloneDeep(currentLicenceEdition);
    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.licenceEditions = licenceEditions;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.appId = $stateParams.appId;
    $scope.lastEditionIndex = _.last(licenceEditions).partIndex;
    $scope.licence = licence;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.print = function (edition) {
      var params = {
        lotId: $stateParams.id,
        index: $scope.licence.partIndex,
        editionIndex: edition.partIndex,
        isLastEdition: $scope.lastEditionIndex === edition.partIndex
      };

      var modalInstance = scModal.open('printLicence', params);

      modalInstance.result.then(function (savedLicence) {
        licence = savedLicence;
      });

      return modalInstance.opened;
    };

    $scope.viewEditionDoc = function (editionPartIndex) {
      var params = {
        edition: $scope.currentLicenceEdition,
        personId: $stateParams.id,
        licenceInd: $stateParams.ind,
        editionInd:  editionPartIndex,
        caseTypeId: $stateParams.caseTypeId
      };

      var modalInstance = scModal.open('licenceEditionDoc', params);

      modalInstance.result.then(function () {
        $state.reload();
      });

      return modalInstance.opened;
    };

    $scope.viewApplication = function (appId, lotId, partIndex) {
      return $state.go('root.applications.edit.data', {
        id: appId,
        set: 'person',
        lotId: lotId,
        ind: partIndex
      });
    };

    $scope.deleteCurrentEdition = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return PersonLicenceEditions
            .remove({
              id: $stateParams.id,
              ind: $stateParams.ind,
              index: $stateParams.index
            })
            .$promise.then(function () {
              $scope.licenceEditions = _.remove($scope.licenceEditions, function (le) {
                return le.partIndex !== currentLicenceEdition.partIndex;
              });

              if ($scope.licenceEditions.length === 0) {
                return $state.go('root.persons.view.licences.search');
              }
              else {
                return $state.go(
                  'root.persons.view.licences.view.editions.edit',
                  { index: _.last($scope.licenceEditions).partIndex });
              }
            });
        }
      });
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.currentLicenceEdition = _.cloneDeep(originalLicenceEdition);
    };
  }

  LicenceEditionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonLicenceEditions',
    'licence',
    'currentLicenceEdition',
    'licenceEditions',
    'scMessage',
    'scModal'
  ];

  LicenceEditionsEditCtrl.$resolve = {
    currentLicenceEdition: [
      '$stateParams',
      'PersonLicenceEditions',
      function ($stateParams, PersonLicenceEditions) {
        return PersonLicenceEditions.get({
          id: $stateParams.id,
          ind: $stateParams.ind,
          index: $stateParams.index
        }).$promise;
      }
    ],
    licenceEditions: [
      '$stateParams',
      'PersonLicenceEditions',
      function ($stateParams, PersonLicenceEditions) {
        return PersonLicenceEditions.query({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('LicenceEditionsEditCtrl', LicenceEditionsEditCtrl);
}(angular, _));
