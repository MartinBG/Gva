/*global angular,_*/
(function (angular) {
  'use strict';

  function StatusesEditCtrl(
    $scope,
    $stateParams,
    $state,
    PersonStatuses,
    status,
    scMessage
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
            return PersonStatuses
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.status)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.statuses.search');
              });
          }
        });
    };

    $scope.deleteStatus = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return PersonStatuses
            .remove({ id: $stateParams.id, ind: status.partIndex }).$promise
            .then(function () {
              return $state.go('root.persons.view.statuses.search');
            });
        }
      });
    };
  }

  StatusesEditCtrl.$inject = [
    '$scope',
    '$stateParams',
    '$state',
    'PersonStatuses',
    'status',
    'scMessage'
  ];

  StatusesEditCtrl.$resolve = {
    status: [
      '$stateParams',
      'PersonStatuses',
      function ($stateParams, PersonStatuses) {
        return PersonStatuses.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('StatusesEditCtrl', StatusesEditCtrl);
}(angular));
