/*global angular*/
(function (angular) {
  'use strict';

  function EditionsNewCtrl($scope, $state, $stateParams, PersonEdition) {
    $scope.save = function () {
      $scope.newRatingEditionForm.$validate()
       .then(function () {
        if ($scope.newRatingEditionForm.$valid) {
          return PersonEdition
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.model).$promise
            .then(function () {
              return $state.go('root.persons.view.editions.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.editions.search');
    };
  }

  EditionsNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonEdition'];

  angular.module('gva').controller('EditionsNewCtrl', EditionsNewCtrl);
}(angular));