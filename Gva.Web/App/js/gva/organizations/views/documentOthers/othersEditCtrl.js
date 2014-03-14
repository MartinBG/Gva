/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationDocOthersEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationDocumentOther,
    organizationDocumentOther,
    selectedPublisher
  ) {

    $scope.organizationDocumentOther = organizationDocumentOther;
    $scope.organizationDocumentOther.part.documentPublisher = selectedPublisher.pop() ||
      organizationDocumentOther.part.documentPublisher;

    $scope.save = function () {
      $scope.organizationDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.organizationDocumentOtherForm.$valid) {
            return OrganizationDocumentOther
              .save({ id: $stateParams.id, ind: $stateParams.ind },
              $scope.organizationDocumentOther)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.documentOthers.search');
              });
          }
        });
    };

    $scope.choosePublisher = function () {
      return $state.go('root.organizations.view.documentOthers.edit.choosePublisher');
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.documentOthers.search');
    };
  }

  OrganizationDocOthersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationDocumentOther',
    'organizationDocumentOther',
    'selectedPublisher'
  ];

  OrganizationDocOthersEditCtrl.$resolve = {
    organizationDocumentOther: [
      '$stateParams',
      'OrganizationDocumentOther',
      function ($stateParams, OrganizationDocumentOther) {
        return OrganizationDocumentOther.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('OrganizationDocOthersEditCtrl', OrganizationDocOthersEditCtrl);
}(angular));