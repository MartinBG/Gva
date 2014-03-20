/*global angular*/
(function (angular) {
  'use strict';

  function CertGroundServiceOperatorsSnoOperationalEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationCertGroundServiceOperatorsSnoOperational,
    organizationCertGroundServiceOperatorSnoOperational) {

    $scope.certGroundServiceOperatorSnoOperational =
      organizationCertGroundServiceOperatorSnoOperational;

    $scope.save = function () {
      return OrganizationCertGroundServiceOperatorsSnoOperational
        .save({ id: $stateParams.id, ind: $stateParams.ind },
        $scope.certGroundServiceOperatorSnoOperational)
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
    'organizationCertGroundServiceOperatorSnoOperational'
  ];

  CertGroundServiceOperatorsSnoOperationalEditCtrl.$resolve = {
    organizationCertGroundServiceOperatorSnoOperational: [
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