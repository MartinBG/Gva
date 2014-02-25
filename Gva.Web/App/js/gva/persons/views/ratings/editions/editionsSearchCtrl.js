/*global angular*/
(function (angular) {
  'use strict';

  function EditionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonEdition,
    PersonEditionView,
    editions
  ) {
    $scope.editions = editions;

    $scope.editEdition = function (item) {
      return $state.go('root.persons.view.editions.edit', {
        id: $stateParams.id,
        ind: $stateParams.ind,
        childInd: item.ratingEdition.partIndex
      });
    };

    $scope.deleteEdition = function (item) {
      return PersonEdition
        .remove({
          id: $stateParams.id,
          ind: $stateParams.ind,
          childInd: item.ratingEdition.partIndex
        }).$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };
    $scope.newEdition = function () {
      return $state.go('root.persons.view.editions.new');
    };
  }

  EditionsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonEdition',
    'PersonEditionView',
    'editions'
  ];
  EditionsSearchCtrl.$resolve = {
    editions: [
      '$stateParams',
      'PersonEditionView',
      function ($stateParams, PersonEditionView) {
        return PersonEditionView.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('EditionsSearchCtrl', EditionsSearchCtrl);
}(angular));