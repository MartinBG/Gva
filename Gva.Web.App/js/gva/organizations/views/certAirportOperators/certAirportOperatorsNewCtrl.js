/*global angular*/
(function (angular) {
  'use strict';

  function CertAirportOperatorsNewCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirportOperators,
    certificate
  ) {
    $scope.certificate = certificate;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.certAirportOperatorForm.$validate()
        .then(function () {
          if ($scope.certAirportOperatorForm.$valid) {
            return CertAirportOperators
              .save({ id: $stateParams.id }, $scope.certificate)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.certAirportOperators.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.certAirportOperators.search');
    };
  }

  CertAirportOperatorsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirportOperators',
    'certificate'
  ];

  CertAirportOperatorsNewCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'CertAirportOperators',
      function ($stateParams, CertAirportOperators) {
        return CertAirportOperators.newCertAirportOperator({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirportOperatorsNewCtrl', CertAirportOperatorsNewCtrl);
}(angular));
