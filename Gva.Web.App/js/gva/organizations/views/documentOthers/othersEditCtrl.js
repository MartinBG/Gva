/*global angular,_*/
(function (angular) {
  'use strict';

  function OrganizationDocOthersEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationDocumentOthers,
    organizationDocumentOther,
    scMessage
  ) {
    var originalDocument = _.cloneDeep(organizationDocumentOther);

    $scope.organizationDocumentOther = organizationDocumentOther;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationDocumentOther = _.cloneDeep(originalDocument);
    };

    $scope.save = function () {
      return $scope.editDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.editDocumentOtherForm.$valid) {
            return OrganizationDocumentOthers
              .save({ id: $stateParams.id, ind: $stateParams.ind },
              $scope.organizationDocumentOther)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.documentOthers.search');
              });
          }
        });
    };

    $scope.deleteDocumentOther = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return OrganizationDocumentOthers.remove({
            id: $stateParams.id,
            ind: organizationDocumentOther.partIndex
          }).$promise.then(function () {
            return $state.go('root.organizations.view.documentOthers.search');
          });
        }
      });
    };
  }

  OrganizationDocOthersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationDocumentOthers',
    'organizationDocumentOther',
    'scMessage'
  ];

  OrganizationDocOthersEditCtrl.$resolve = {
    organizationDocumentOther: [
      '$stateParams',
      'OrganizationDocumentOthers',
      function ($stateParams, OrganizationDocumentOthers) {
        return OrganizationDocumentOthers.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('OrganizationDocOthersEditCtrl', OrganizationDocOthersEditCtrl);
}(angular));
