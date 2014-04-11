/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentEducationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentEducation,
    edu
  ) {
    var originalEdu = _.cloneDeep(edu);

    $scope.personDocumentEducation = edu;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentEducation = _.cloneDeep(originalEdu);
    };

    $scope.save = function () {
      return $scope.editDocumentEducationForm.$validate()
        .then(function () {
          if ($scope.editDocumentEducationForm.$valid) {
            return PersonDocumentEducation
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind
              }, $scope.personDocumentEducation)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentEducations.search');
              });
          }
        });
    };

    $scope.deleteEdu = function () {
      return PersonDocumentEducation.remove({
        id: $stateParams.id,
        ind: edu.partIndex
      })
        .$promise.then(function () {
          return $state.go('root.persons.view.documentEducations.search');
        });
    };
  }

  DocumentEducationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEducation',
    'edu'
  ];

  DocumentEducationsEditCtrl.$resolve = {
    edu: [
      '$stateParams',
      'PersonDocumentEducation',
      function ($stateParams, PersonDocumentEducation) {
        return PersonDocumentEducation.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentEducationsEditCtrl', DocumentEducationsEditCtrl);
}(angular));
