/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationDocOthersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationDocumentOther,
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
    'OrganizationDocumentOther',
    'documentOthers'
  ];

  OrganizationDocOthersSearchCtrl.$resolve = {
    documentOthers: [
      '$stateParams',
      'OrganizationDocumentOther',
      function ($stateParams, OrganizationDocumentOther) {
        return OrganizationDocumentOther.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationDocOthersSearchCtrl', OrganizationDocOthersSearchCtrl);
}(angular));
