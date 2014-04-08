/*global angular,_*/
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
    var originalDocument = _.cloneDeep(organizationDocumentOther);

    $scope.organizationDocumentOther = organizationDocumentOther;
    $scope.organizationDocumentOther.part.documentPublisher = selectedPublisher.pop() ||
      organizationDocumentOther.part.documentPublisher;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationDocumentOther.part = _.cloneDeep(originalDocument.part);
      $scope.$broadcast('cancel', originalDocument);
    };

    $scope.save = function () {
      return $scope.editDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.editDocumentOtherForm.$valid) {
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

    $scope.deleteDocumentOther = function () {
      return OrganizationDocumentOther.remove({
        id: $stateParams.id,
        ind: organizationDocumentOther.partIndex
      }).$promise.then(function () {
        return $state.go('root.organizations.view.documentOthers.search');
      });
    };

    $scope.choosePublisher = function () {
      return $state.go('root.organizations.view.documentOthers.edit.choosePublisher');
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