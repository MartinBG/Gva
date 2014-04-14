/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationDocOthersNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationDocumentOther,
    organizationDocumentOther,
    selectedPublisher
  ) {
    $scope.save = function () {
      return $scope.newDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.newDocumentOtherForm.$valid) {
            return OrganizationDocumentOther
              .save({ id: $stateParams.id }, $scope.organizationDocumentOther).$promise
              .then(function () {
                return $state.go('root.organizations.view.documentOthers.search');
              });
          }
        });
    };

    $scope.organizationDocumentOther = organizationDocumentOther;
    $scope.organizationDocumentOther.part.documentPublisher = selectedPublisher.pop() ||
      organizationDocumentOther.part.documentPublisher;

    $scope.choosePublisher = function () {
      return $state.go('root.organizations.view.documentOthers.new.choosePublisher');
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.documentOthers.search');
    };
  }

  OrganizationDocOthersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationDocumentOther',
    'organizationDocumentOther',
    'selectedPublisher'
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
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('OrganizationDocOthersNewCtrl', OrganizationDocOthersNewCtrl);
}(angular));
