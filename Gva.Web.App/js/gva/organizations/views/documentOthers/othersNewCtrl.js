/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationDocOthersNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationDocumentOthers,
    organizationDocumentOther
  ) {
    $scope.organizationDocumentOther = organizationDocumentOther;
    $scope.lotId = $stateParams.id;
    $scope.appId = $stateParams.appId;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.newDocumentOtherForm.$valid) {
            return OrganizationDocumentOthers
              .save({ id: $stateParams.id }, $scope.organizationDocumentOther).$promise
              .then(function () {
                return $state.go('root.organizations.view.documentOthers.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.documentOthers.search');
    };
  }

  OrganizationDocOthersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationDocumentOthers',
    'organizationDocumentOther'
  ];

  OrganizationDocOthersNewCtrl.$resolve = {
    organizationDocumentOther: [
      '$stateParams',
      'OrganizationDocumentOthers',
      function ($stateParams, OrganizationDocumentOthers) {
        return OrganizationDocumentOthers.newDocument({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('OrganizationDocOthersNewCtrl', OrganizationDocOthersNewCtrl);
}(angular));
