/*global angular*/
(function (angular) {
  'use strict';

  function EditionsEditCtrl($scope, $state, $stateParams, PersonEdition) {
    PersonEdition
      .get({ id: $stateParams.id, ind: $stateParams.ind, childInd: $stateParams.childInd }).$promise
      .then(function (item) {
        $scope.item = item;
      });

    $scope.save = function () {
      $scope.editRatingEditionForm.$validate()
       .then(function () {
        if ($scope.editRatingEditionForm.$valid) {
          return PersonEdition
             .save({
              id: $stateParams.id,
              ind: $stateParams.ind,
              childInd: $stateParams.childInd
            }, $scope.item).$promise
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

  EditionsEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonEdition'];

  angular.module('gva').controller('EditionsEditCtrl', EditionsEditCtrl);
}(angular));