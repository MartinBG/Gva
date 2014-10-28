/*global angular*/
(function (angular) {
  'use strict';

  function AwExaminersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizationAwExaminers
  ) {
    $scope.organizationAwExaminers = organizationAwExaminers;
  }

  AwExaminersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'organizationAwExaminers'
  ];

  AwExaminersSearchCtrl.$resolve = {
    organizationAwExaminers: [
      '$stateParams',
      'OrganizationAwExaminers',
      function ($stateParams, OrganizationAwExaminers) {
        return OrganizationAwExaminers.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('AwExaminersSearchCtrl', AwExaminersSearchCtrl);
}(angular));
