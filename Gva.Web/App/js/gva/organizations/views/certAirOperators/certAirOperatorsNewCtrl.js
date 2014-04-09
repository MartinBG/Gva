/*global angular*/
(function (angular) {
  'use strict';

  function CertAirOperatorsNewCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirOperator,
    certificate
  ) {
    $scope.certificate = certificate;

    $scope.save = function () {
      return $scope.certAirOperatorForm.$validate()
        .then(function () {
          if ($scope.certAirOperatorForm.$valid) {
            return CertAirOperator
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
    'CertAirOperator',
    'certificate'
  ];

  CertAirOperatorsNewCtrl.$resolve = {
    certificate: function () {
      return {
        part: {
          includedDocuments: []
        }
      };
    }
  };

  angular.module('gva').controller('CertAirOperatorsNewCtrl', CertAirOperatorsNewCtrl);
}(angular));
