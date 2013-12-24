/*global angular, _*/
(function (angular, _) {
  'use strict';

  function StatusesSearchCtrl($scope, $stateParams, $state, PersonStatus) {
    PersonStatus.query({ id: $stateParams.id }).$promise
      .then(function (statuses) {
        $scope.statuses = _(statuses)
          .forEach(function (s) {
            s.part.isActive = new Date(s.part.documentDateValidTo) > new Date();
          })
          .value();
      });

    $scope.editStatus = function (status) {
      return $state.go('persons.statuses.edit', { id: $stateParams.id, ind: status.partIndex });
    };

    $scope.deleteStatus = function (status) {
      return PersonStatus
        .remove({ id: $stateParams.id, ind: status.partIndex }).$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newStatus = function () {
      return $state.go('persons.statuses.new');
    };
  }

  StatusesSearchCtrl.$inject = ['$scope', '$stateParams', '$state', 'PersonStatus'];

  angular.module('gva').controller('StatusesSearchCtrl', StatusesSearchCtrl);
}(angular, _));
