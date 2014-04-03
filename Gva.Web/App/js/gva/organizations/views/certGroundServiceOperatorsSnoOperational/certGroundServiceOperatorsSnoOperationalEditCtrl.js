/*global angular*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsSnoOperationalEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperatorsSnoOperational,
    certificate
    ) {

    $scope.certificate = certificate;

    $scope.save = function () {
      return OrganizationCertGroundServiceOperatorsSnoOperational
        .save({ id: $stateParams.id, ind: $stateParams.ind },
        $scope.certificate)
        .$promise
        .then(function () {
          return $state
            .go('root.organizations.view.groundServiceOperatorsSnoOperational.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.groundServiceOperatorsSnoOperational.search');
    };
  }

  CertGroundServiceOperatorsSnoOperationalEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationCertGroundServiceOperatorsSnoOperational',
    'certificate'
  ];

  CertGroundServiceOperatorsSnoOperationalEditCtrl.$resolve = {
    certificate: [
      '$stateParams',
      'OrganizationCertGroundServiceOperatorsSnoOperational',
      function ($stateParams, OrganizationCertGroundServiceOperatorsSnoOperational) {
        return OrganizationCertGroundServiceOperatorsSnoOperational.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('CertGroundServiceOperatorsSnoOperationalEditCtrl',
    CertGroundServiceOperatorsSnoOperationalEditCtrl);
}(angular));