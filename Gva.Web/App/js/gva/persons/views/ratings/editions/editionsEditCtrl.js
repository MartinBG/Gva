/*global angular*/
(function (angular) {
  'use strict';

  function EditionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonEdition,
    edition
  ) {
    $scope.item = edition;

    $scope.save = function () {
      return $scope.editRatingEditionForm.$validate()
        .then(function () {
          if ($scope.editRatingEditionForm.$valid) {
            return PersonEdition
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind,
                childInd: $stateParams.childInd
              }, $scope.item)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.ratings.editions.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.ratings.editions.search');
    };
  }

  EditionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonEdition',
    'edition'
  ];

  EditionsEditCtrl.$resolve = {
    edition: [
      '$stateParams',
      'PersonEdition',
      function ($stateParams, PersonEdition) {
        return PersonEdition.get({
          id: $stateParams.id,
          ind: $stateParams.ind,
          childInd: $stateParams.childInd
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EditionsEditCtrl', EditionsEditCtrl);
}(angular));