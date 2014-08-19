/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationDocOthersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    documentOthers
  ) {
    $scope.documentOthers = documentOthers;
  }

  OrganizationDocOthersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'documentOthers'
  ];

  OrganizationDocOthersSearchCtrl.$resolve = {
    documentOthers: [
      '$stateParams',
      'OrganizationDocumentOthers',
      function ($stateParams, OrganizationDocumentOthers) {
        return OrganizationDocumentOthers.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationDocOthersSearchCtrl', OrganizationDocOthersSearchCtrl);
}(angular));
