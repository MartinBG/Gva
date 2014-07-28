/*global angular,_*/
(function (angular) {
  'use strict';

  function CertAirportOperatorsEditCtrl(
    $scope,
    $state,
    $stateParams,
    CertAirportOperators,
    certificate,
    scMessage
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
      return $scope.certAirportOperatorForm.$validate()
        .then(function () {
          if ($scope.certAirportOperatorForm.$valid) {
            return CertAirportOperators
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.certificate)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.certAirportOperators.search');
              });
          }
        });
    };

    $scope.deleteCertAirportOperator = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return CertAirportOperators.remove({ id: $stateParams.id, ind: certificate.partIndex })
            .$promise.then(function () {
              return $state.go('root.organizations.view.certAirportOperators.search');
            });
        }
      });
    };
  }

  CertAirportOperatorsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'CertAirportOperators',
    'certificate',
    'scMessage'
  ];

  CertAirportOperatorsEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'CertAirportOperators',
      function ($stateParams, CertAirportOperators) {
        return CertAirportOperators.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirportOperatorsEditCtrl', CertAirportOperatorsEditCtrl);
}(angular));
