/*global angular,_*/
(function (angular) {
  'use strict';

  function CertAirOperatorsEditCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirOperator,
    certificate
  ) {
    var originalCertificate = _.cloneDeep(certificate);

    $scope.certificate = certificate;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.certificate = _.cloneDeep(originalCertificate);
    };

    $scope.save = function () {
      return $scope.certAirOperatorForm.$validate()
        .then(function () {
          if ($scope.certAirOperatorForm.$valid) {
            return CertAirOperator
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.certificate)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.certAirOperators.search');
              });
          }
        });
    };

    $scope.deleteCertAirOperator = function () {
      return CertAirOperator.remove({ id: $stateParams.id, ind: certificate.partIndex })
        .$promise.then(function () {
          return $state.go('root.organizations.view.certAirOperators.search');
        });
    };
  }

  CertAirOperatorsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirOperator',
    'certificate'
  ];

  CertAirOperatorsEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'CertAirOperator',
      function ($stateParams, CertAirOperator) {
        return CertAirOperator.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirOperatorsEditCtrl', CertAirOperatorsEditCtrl);
}(angular));
