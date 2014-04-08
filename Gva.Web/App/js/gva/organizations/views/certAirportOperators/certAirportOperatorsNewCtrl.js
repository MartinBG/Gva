/*global angular*/
(function (angular) {
  'use strict';

  function CertAirportOperatorsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationDocumentOther,
    CertAirportOperator,
    certificate
    ) {
    $scope.certificate = certificate;

    $scope.save = function () {
      return $scope.certAirportOperatorForm.$validate()
        .then(function () {
          if ($scope.certAirportOperatorForm.$valid) {
            return CertAirportOperator
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
    'OrganizationDocumentOther',
    'CertAirportOperator',
    'certificate'
  ];

  CertAirportOperatorsNewCtrl.$resolve = {
    certificate: function () {
      return {
        part: {
          includedDocuments: []
        }
      };
    }
  };

  angular.module('gva').controller('CertAirportOperatorsNewCtrl', CertAirportOperatorsNewCtrl);
}(angular));
