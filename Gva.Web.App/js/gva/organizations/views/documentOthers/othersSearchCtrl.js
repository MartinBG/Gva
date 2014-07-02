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

    $scope.editDocumentOther = function (documentOther) {
      return $state.go('root.organizations.view.documentOthers.edit',
        {
          id: $stateParams.id,
          ind: documentOther.partIndex
        });
    };

    $scope.newDocumentOther = function () {
      return $state.go('root.organizations.view.documentOthers.new');
    };
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
