/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CorrNewCtrl(
    $scope,
    $state,
    $stateParams,
    Corrs,
    corrGroups,
    corrTypes,
    selectedCorrs,
    corr
    ) {
    $scope.corr = corr;

    if ($state.payload) {
      if ($stateParams.filter === 'Person') {
        $scope.corr.correspondentTypeId = _(corrTypes)
          .filter({ alias: 'BulgarianCitizen' })
          .first()
          .nomValueId;
        $scope.corr.correspondentType = _(corrTypes).filter({ alias: 'BulgarianCitizen' }).first();
        $scope.corr.bgCitizenFirstName = $state.payload.bgCitizenFirstName;
        $scope.corr.bgCitizenLastName = $state.payload.bgCitizenLastName;
        $scope.corr.bgCitizenUIN = $state.payload.bgCitizenUIN;
        $scope.corr.email = $state.payload.email;
      }
      else if ($stateParams.filter === 'Organization') {
        $scope.corr.legalEntityName = $state.payload.legalEntityName;
        $scope.corr.legalEntityBulstat = $state.payload.legalEntityBulstat;
      }
    }

    $scope.save = function save() {
      return $scope.newCorrForm.$validate()
        .then(function () {
          if ($scope.newCorrForm.$valid) {
            return Corrs.save($scope.corr).$promise.then(function (corr) {
              selectedCorrs.current.push(corr.correspondentId);
              return $state.go('^');
            });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

  }

  CorrNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Corrs',
    'corrGroups',
    'corrTypes',
    'selectedCorrs',
    'corr'
  ];

  CorrNewCtrl.$resolve = {
    corr: [
      '$stateParams',
      'Corrs',
      function resolveCorr($stateParams, Corrs) {
        return Corrs.getNew().$promise;
      }
    ],
    corrGroups: [
      'Nomenclatures',
      function resolveCorr(Nomenclatures) {
        return Nomenclatures.query({ alias: 'correspondentGroup' }).$promise;
      }
    ],
    corrTypes: [
      'Nomenclatures',
      function resolveCorr(Nomenclatures) {
        return Nomenclatures.query({ alias: 'correspondentType' }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CorrNewCtrl', CorrNewCtrl);
}(angular, _));
