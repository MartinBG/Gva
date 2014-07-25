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

    $scope.organizationDocumentOther = organizationDocumentOther;

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
      'application',
      function (application) {
        if (application) {
          return {
            part: {},
            files: [{ isAdded: true, applications: [application] }]
          };
        }
        else {
          return {
            part: {},
            files: []
          };
        }
      }
    ]
  };

  angular.module('gva').controller('OrganizationDocOthersNewCtrl', OrganizationDocOthersNewCtrl);
}(angular));
