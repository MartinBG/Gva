/*global angular*/
(function (angular) {
  'use strict';

  function PersonDataEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonData,
    personData
  ) {
    $scope.personData = personData;

    $scope.save = function () {
      $scope.personDataForm.$validate()
      .then(function () {
        if ($scope.personDataForm.$valid) {
          return PersonData
          .save({ id: $stateParams.id }, $scope.personData)
          .$promise
          .then(function () {
            return $state.transitionTo('root.persons.view', $stateParams, { reload: true });
          });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view');
    };
  }

  PersonDataEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonData',
    'personData'
  ];

  PersonDataEditCtrl.$resolve = {
    personData: [
      '$stateParams',
      'PersonData',
      function ($stateParams, PersonData) {
        return PersonData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonDataEditCtrl', PersonDataEditCtrl);
}(angular));
