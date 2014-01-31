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

        goToPreviousState();
      });
    };

    $scope.cancel = function () {
      goToPreviousState();
    };

    function goToPreviousState() {
      var previousState;
      if ($state.$current.parent.self.name === 'applications/new') {
        previousState = 'applications/new/doc';
      }
      else if ($state.$current.parent.self.name === 'applications/link') {
        previousState = 'applications/link/common';
      }

      return $state.go(previousState);
    }
  }

  PersonNewCtrl.$inject = ['$scope', '$state', 'Person'];

  angular.module('gva').controller('PersonNewCtrl', PersonNewCtrl);
}(angular));
