/*global angular*/
(function (angular) {
  'use strict';

  function PersonDataCtrl($scope, $stateParams, Person, Nomenclature, PersonNextLin) {
    Nomenclature.query({alias: 'linTypes'})
      .$promise.then(function(linTypes){
        $scope.linTypes = linTypes;
      });

    $scope.setLin = function(item){
      $scope.model.linType = item.name;
      PersonNextLin.get({
        linType: item.code
      }).$promise
        .then(function(result){
          $scope.model.lin = result.nextLin;
        });
    };

    $scope.isUniqueLin = function (value) {
      if (!value) {
        return true;
      }

      return Person.query({ lin: value, exact: true })
        .$promise
        .then(function (persons) {
          return persons.length === 0 ||
            (persons.length === 1 && persons[0].id === $stateParams.id);
        });
    };

    $scope.requireCaseTypes = function () {
      return $scope.model.caseTypes.length > 0;
    };
  }

  PersonDataCtrl.$inject = ['$scope', '$stateParams', 'Person', 'Nomenclature', 'PersonNextLin'];

  angular.module('gva').controller('PersonDataCtrl', PersonDataCtrl);
}(angular));
