/*global angular,_*/
(function (angular) {
  'use strict';

  function PersonDataEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonData,
    personData
  ) {
    var originalPersonData = _.cloneDeep(personData);

    $scope.personData = personData;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personData = _.cloneDeep(originalPersonData);
    };

    $scope.save = function () {
      return $scope.editPersonForm.$validate()
        .then(function () {
          if ($scope.editPersonForm.$valid) {
            return PersonData
              .save({ id: $stateParams.id }, $scope.personData)
              .$promise
              .then(function () {
                return $state.transitionTo('root.persons.view', $stateParams, { reload: true });
              });
          }
        });
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
