﻿/*global angular*/
(function (angular) {
  'use strict';

  function CertAirOperatorsNewCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirOperators,
    certificate
  ) {
    $scope.certificate = certificate;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.certAirOperatorForm.$validate()
        .then(function () {
          if ($scope.certAirOperatorForm.$valid) {
            return CertAirOperators
              .save({ id: $stateParams.id }, $scope.certificate)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.certAirOperators.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.certAirOperators.search');
    };
  }

  CertAirOperatorsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirOperators',
    'certificate'
  ];

  CertAirOperatorsNewCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'CertAirOperators',
      function ($stateParams, CertAirOperators) {
        return CertAirOperators.newCertAirOperator({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirOperatorsNewCtrl', CertAirOperatorsNewCtrl);
}(angular));
