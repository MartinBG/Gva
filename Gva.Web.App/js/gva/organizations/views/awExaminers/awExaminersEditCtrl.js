/*global angular,_*/
(function (angular) {
  'use strict';

  function AwExaminersEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAwExaminers,
    organizationAwExaminer,
    scMessage
  ) {
    var originalAwExaminer = _.cloneDeep(organizationAwExaminer);

    $scope.organizationAwExaminer = organizationAwExaminer;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationAwExaminer = _.cloneDeep(originalAwExaminer);
    };

    $scope.save = function () {
      return $scope.editAwExaminer.$validate()
        .then(function () {
          if ($scope.editAwExaminer.$valid) {
            return OrganizationAwExaminers
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind
              }, $scope.organizationAwExaminer)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.awExaminers.search');
              });
          }
        });
    };

    $scope.deleteStaffChecker = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return OrganizationAwExaminers.remove({
              id: $stateParams.id,
              ind: organizationAwExaminer.partIndex
            }).$promise.then(function () {
              return $state.go('root.organizations.view.awExaminers.search');
            });
        }
      });
    };
  }

  AwExaminersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationAwExaminers',
    'organizationAwExaminer',
    'scMessage'
  ];

  AwExaminersEditCtrl.$resolve = {
    organizationAwExaminer: [
      '$stateParams',
      'OrganizationAwExaminers',
      function ($stateParams, OrganizationAwExaminers) {
        return OrganizationAwExaminers.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AwExaminersEditCtrl', AwExaminersEditCtrl);
}(angular));
