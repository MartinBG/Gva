/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    PersonLicenceEditions,
    currentLicenceEdition,
    licenceEditions,
    scMessage
  ) {
    var originalLicenceEdition = _.cloneDeep(currentLicenceEdition);
    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.licenceEditions = licenceEditions;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.appId = $stateParams.appId;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editLicenceEditionForm.$validate().then(function () {
        if ($scope.editLicenceEditionForm.$valid) {
          return PersonLicenceEditions
            .save({
              id: $stateParams.id,
              ind: $stateParams.ind,
              index: $stateParams.index
            }, $scope.currentLicenceEdition, $scope.caseTypeId)
            .$promise
            .then(function (edition) {
              $scope.editMode = null;
              var editionIndex = null;
              _.find($scope.licenceEditions, function (ed, index) {
                if (ed.partIndex === edition.partIndex) {
                  editionIndex = index;
                }
              });
              originalLicenceEdition = _.cloneDeep($scope.currentLicenceEdition);
              $scope.licenceEditions[editionIndex] = _.cloneDeep(edition);
            });
        }
      });
    };

    $scope.deleteCurrentEdition = function () {

      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          $scope.licenceEditions = _.remove($scope.licenceEditions, function (le) {
            return le.partIndex !== currentLicenceEdition.partIndex;
          });
          return PersonLicenceEditions
            .remove({
              id: $stateParams.id,
              ind: $stateParams.ind,
              index: $stateParams.index
            })
            .$promise.then(function () {
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
    '$filter',
    'PersonLicenceEditions',
    'currentLicenceEdition',
    'licenceEditions',
    'scMessage'
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
