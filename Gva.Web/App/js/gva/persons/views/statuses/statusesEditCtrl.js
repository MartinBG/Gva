/*global angular,_*/
(function (angular) {
  'use strict';

  function StatusesEditCtrl(
    $scope,
    $stateParams,
    $state,
    PersonStatus,
    status
  ) {
    var originalStatus = _.cloneDeep(status);

    $scope.status = status;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.status = _.cloneDeep(originalStatus);
    };

    $scope.save = function () {
      return $scope.personStatusForm.$validate()
        .then(function () {
          if ($scope.personStatusForm.$valid) {
            return PersonStatus
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.status)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.statuses.search');
              });
          }
        });
    };

    $scope.deleteStatus = function () {
      return PersonStatus
        .remove({ id: $stateParams.id, ind: status.partIndex }).$promise
        .then(function () {
          return $state.go('root.persons.view.statuses.search');
        });
    };
  }

  StatusesEditCtrl.$inject = [
    '$scope',
    '$stateParams',
    '$state',
    'PersonStatus',
    'status'
  ];

  StatusesEditCtrl.$resolve = {
    status: [
      '$stateParams',
      'PersonStatus',
      function ($stateParams, PersonStatus) {
        return PersonStatus.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('StatusesEditCtrl', StatusesEditCtrl);
}(angular));
