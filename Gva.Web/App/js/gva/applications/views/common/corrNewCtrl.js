/*global angular*/
(function (angular) {
  'use strict';

  function CorrNewCtrl(
    $scope,
    $state,
    $stateParams,
    Corr,
    selectedCorrs
    ) {
    if (!selectedCorrs.onCorrSelect) {
      return $state.go('^');
    }

    $scope.corr = {
      isActive: true
    };

    if ($state.payload) {
      $scope.corr.correspondentGroupId = $state.payload.correspondentGroupId;
      $scope.corr.correspondentTypeId = $state.payload.correspondentTypeId;
      $scope.corr.correspondentType = $state.payload.correspondentType;
      if ($state.payload.correspondentType.alias === 'BulgarianCitizen') {
        $scope.corr.bgCitizenFirstName = $state.payload.bgCitizenFirstName;
        $scope.corr.bgCitizenLastName = $state.payload.bgCitizenLastName;
        $scope.corr.bgCitizenUIN = $state.payload.bgCitizenUIN;
        $scope.corr.email = $state.payload.email;
      }
      else if ($state.payload.correspondentType.alias === 'LegalEntity') {
        $scope.corr.legalEntityName = $state.payload.legalEntityName;
        $scope.corr.legalEntityBulstat = $state.payload.legalEntityBulstat;
      }
    }

    $scope.save = function save() {
      return $scope.newCorrForm.$validate()
        .then(function () {
          if ($scope.newCorrForm.$valid) {
            return Corr.save($scope.corr).$promise.then(function (corr) {
              selectedCorrs.onCorrSelect(corr.correspondentId);
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
    'Corr',
    'selectedCorrs'
  ];

  angular.module('gva').controller('CorrNewCtrl', CorrNewCtrl);
}(angular));
