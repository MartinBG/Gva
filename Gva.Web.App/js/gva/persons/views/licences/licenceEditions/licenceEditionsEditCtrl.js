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
    scMessage
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

    $scope.save = function () {
      return $scope.editLicenceEditionForm.$validate().then(function () {
        if ($scope.editLicenceEditionForm.$valid) {
          return PersonLicenceEditions
            .save({
              id: $stateParams.id,
              ind: $stateParams.ind,
              index: $stateParams.index,
              caseTypeId: $scope.caseTypeId
            }, $scope.currentLicenceEdition)
            .$promise
            .then(function (edition) {
              $scope.editMode = null;
              var editionIndex = _.findIndex($scope.licenceEditions, function (ed) {
                return ed.partIndex === edition.partIndex;
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
