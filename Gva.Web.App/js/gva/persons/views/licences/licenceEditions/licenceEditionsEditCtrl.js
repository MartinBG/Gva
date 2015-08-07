/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicenceEditions,
    Nomenclatures,
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
    $scope.licenceTypeCode = licence.licenceType.code;
    $scope.isFcl = $scope.licenceTypeCode.indexOf('FCL') >= 0 || 
      $scope.licenceTypeCode === 'BG CCA' ||
      $scope.licenceTypeCode === 'NPPM';
    $scope.$watch('newLicence.part.licenceTypeId', function () {
      if ($scope.newLicence && $scope.newLicence.part.licenceTypeId) {
        Nomenclatures.get({
          alias: 'licenceTypes',
          id: $scope.newLicence.part.licenceTypeId 
        })
          .$promise
          .then(function (licenceType) {
            $scope.licenceTypeCode = licenceType.code;
            $scope.isFcl = $scope.licenceTypeCode.indexOf('FCL') >= 0 || 
              $scope.licenceTypeCode === 'BG CCA' ||
              $scope.licenceTypeCode === 'NPPM';
          });
      }
    });

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.print = function (edition) {
      var params = {
        lotId: $stateParams.id,
        index: $scope.licence.partIndex,
        editionIndex: edition.partIndex,
        isLastEdition: $scope.lastEditionIndex === edition.partIndex,
        isFclOrPart66: $scope.isFcl ||
              $scope.licence.licenceType.code.indexOf('Part-66') >= 0,
        hasNoNumber: edition.hasNoNumber
      };

      var modalInstance = scModal.open('printLicence', params);

      modalInstance.result.then(function () {
        return $state.go('root.persons.view.licences.view.editions.edit.ratings',
          $stateParams,
          { reload: true });
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
        return $state.go('root.persons.view.licences.view.editions.edit.ratings',
          $stateParams,
          { reload: true });
      });

      return modalInstance.opened;
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
    'Nomenclatures',
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
