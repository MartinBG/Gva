/*global angular*/
(function (angular) {
  'use strict';

  function PersonNewCtrl($scope, $state, Person) {

    $scope.save = function () {
      return Person.save($scope.newPerson).$promise
      .then(function (result) {
        $scope.$parent.person = {
          nomTypeValueId: result.lotId,
          name: result.personData.part.firstName + ' ' + result.personData.part.lastName,
          content: result
        };

        return $state.go('applications/new/doc');
      });
    };

    $scope.cancel = function () {
      return $state.go('applications/new/doc');
    };
  }

  PersonNewCtrl.$inject = ['$scope', '$state', 'Person'];

  angular.module('gva').controller('PersonNewCtrl', PersonNewCtrl);
}(angular));
