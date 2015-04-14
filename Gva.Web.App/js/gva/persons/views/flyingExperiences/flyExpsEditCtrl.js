/*global angular, _*/
(function (angular, _) {
  'use strict';

  function FlyingExperiencesEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonFlyingExperiences,
    personFlyingExperience,
    scMessage
  ) {
    var originalFlyingExp = _.cloneDeep(personFlyingExperience);

    $scope.personFlyingExperience = personFlyingExperience;
    $scope.editMode = null;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.lotId = $stateParams.id;
    $scope.partIndex = $stateParams.ind;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personFlyingExperience = _.cloneDeep(originalFlyingExp);
    };

    $scope.save = function () {
      return $scope.personFlyingExperienceForm.$validate()
        .then(function () {
          if ($scope.personFlyingExperienceForm.$valid) {
            return PersonFlyingExperiences
              .save({ id: $scope.lotId, ind: $stateParams.ind }, $scope.personFlyingExperience)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.flyingExperiences.search');
              });
          }
        });
    };

    $scope.deleteFlyingExp = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return PersonFlyingExperiences.remove({
            id: $stateParams.id,
            ind: $stateParams.ind
          }).$promise.then(function () {
            return $state.go('root.persons.view.flyingExperiences.search');
          });
        }
      });
    };
  }

  FlyingExperiencesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonFlyingExperiences',
    'personFlyingExperience',
    'scMessage'
  ];

  FlyingExperiencesEditCtrl.$resolve = {
    personFlyingExperience: [
      '$stateParams',
      'PersonFlyingExperiences',
      function ($stateParams, PersonFlyingExperiences) {
        return PersonFlyingExperiences.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('FlyingExperiencesEditCtrl', FlyingExperiencesEditCtrl);
}(angular, _));
