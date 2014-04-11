/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentEmploymentsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentEmployment,
    employment
  ) {
    var originalEmpl = _.cloneDeep(employment);

    $scope.personDocumentEmployment = employment;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentEmployment = _.cloneDeep(originalEmpl);
    };

    $scope.save = function () {
      return $scope.editDocumentEmploymentForm.$validate()
        .then(function () {
          if ($scope.editDocumentEmploymentForm.$valid) {
            return PersonDocumentEmployment
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind
              }, $scope.personDocumentEmployment)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.employments.search');
              });
          }
        });
    };

    $scope.deleteEmployment = function () {
      return PersonDocumentEmployment.remove({ id: $stateParams.id, ind: employment.partIndex })
        .$promise.then(function () {
          return $state.go('root.persons.view.employments.search');
        });
    };
  }

  DocumentEmploymentsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEmployment',
    'employment'
  ];

  DocumentEmploymentsEditCtrl.$resolve = {
    employment: [
      '$stateParams',
      'PersonDocumentEmployment',
      function ($stateParams, PersonDocumentEmployment) {
        return PersonDocumentEmployment.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentEmploymentsEditCtrl', DocumentEmploymentsEditCtrl);
}(angular));
