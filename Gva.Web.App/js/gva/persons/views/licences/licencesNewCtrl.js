/*global angular*/
(function (angular) {
  'use strict';

  function LicencesNewCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    PersonLicences,
    PersonLicenceEditions,
    Nomenclatures,
    newLicence
  ) {
    $scope.newLicence = newLicence;
    $scope.newEdition = null;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.appId = $stateParams.appId;
    $scope.$watch('newLicence.part.licenceTypeId', function () {
      if ($scope.newLicence.part.licenceTypeId) {
        Nomenclatures.get({
          alias: 'licenceTypes',
          id: $scope.newLicence.part.licenceTypeId 
        })
          .$promise
          .then(function (licenceType) {
            $scope.isFcl = licenceType.code.indexOf('FCL') >= 0 || 
              licenceType.code === 'BG CCA';
          });
      }
    });

    $scope.$watch('newLicence.case.caseType', function () {
      if ($scope.newLicence['case'] && $scope.newLicence['case'].caseType) {
        PersonLicenceEditions.newLicenceEdition({
          id: $scope.lotId,
          appId: $scope.appId,
          caseTypeId: newLicence['case'].caseType.nomValueId
        }).$promise.then(function (edition) {
          $scope.newEdition = edition;
        });
      }
    });

    $scope.save = function () {
      return $scope.newLicenceForm.$validate().then(function () {
        if ($scope.newLicenceForm.$valid) {
          return PersonLicences.save({ id: $stateParams.id }, {
            licence: $scope.newLicence,
            edition: $scope.newEdition
          }).$promise
            .then(function () {
              return $state.go('root.persons.view.licences.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.licences.search');
    };
  }

  LicencesNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$filter',
    'PersonLicences',
    'PersonLicenceEditions',
    'Nomenclatures',
    'newLicence'
  ];

  LicencesNewCtrl.$resolve = {
    newLicence: [
      '$stateParams',
      'PersonLicences',
      function ($stateParams, PersonLicences) {
        return PersonLicences.newLicence({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('LicencesNewCtrl', LicencesNewCtrl);
}(angular));
