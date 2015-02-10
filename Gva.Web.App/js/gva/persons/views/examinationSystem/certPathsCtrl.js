/*global angular*/
(function (angular) {
  'use strict';

  function CertPathsCtrl(
    $scope,
    certPaths
  ) {
    $scope.certPaths = certPaths;
  }

  CertPathsCtrl.$inject = [
    '$scope',
    'certPaths'
  ];

  CertPathsCtrl.$resolve = {
    certPaths: [
      'ExaminationSystem',
      function (ExaminationSystem) {
        return ExaminationSystem.getCertPaths().$promise;
      }
    ]
  };

  angular.module('gva').controller('CertPathsCtrl', CertPathsCtrl);
}(angular));