/*global angular*/
(function (angular) {
  'use strict';

  function LicenceEditionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicences,
    PersonLicenceEditions,
    licence,
    newLicenceEdition
  ) {
    $scope.newLicenceEdition = newLicenceEdition;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.appId = $stateParams.appId;
    $scope.licence = licence;
    var licenceTypeCode = $scope.licence.part.licenceType.code;
    $scope.isFcl = licenceTypeCode.indexOf('FCL') >= 0 ||
        licenceTypeCode === 'BG CCA';

    $scope.save = function () {
      return $scope.newLicenceEditionForm.$validate().then(function () {
        if ($scope.newLicenceEditionForm.$valid) {
          return PersonLicenceEditions
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.newLicenceEdition).$promise
            .then(function (edition) {
              return $state.go(
                'root.persons.view.licences.view.editions.edit.ratings',
                { index: edition.partIndex });
            });
        }
      });
    };

    $scope.cancel = function () {
      return PersonLicences.lastEditionIndex($stateParams).$promise.then(function (index) {
        return $state.go(
          'root.persons.view.licences.view.editions.edit.ratings',
          { index: index.lastIndex });
      });
    };
  }

  LicenceEditionsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonLicences',
    'PersonLicenceEditions',
    'licence',
    'newLicenceEdition'
  ];

  LicenceEditionsNewCtrl.$resolve = {
    newLicenceEdition: [
      '$stateParams',
      'PersonLicenceEditions',
      'licence',
      function ($stateParams, PersonLicenceEditions, licence) {
        return PersonLicenceEditions.newLicenceEdition({
          id: $stateParams.id,
          licencePartIndex: $stateParams.ind,
          appId: $stateParams.appId,
          caseTypeId: licence['case'].caseType.nomValueId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('LicenceEditionsNewCtrl', LicenceEditionsNewCtrl);
}(angular));
