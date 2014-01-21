/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEducationsSearchCtrl($scope, $state, $stateParams, PersonDocumentEducation) {
    PersonDocumentEducation.query($stateParams).$promise.then(function (documentEducations) {
      $scope.documentEducations = documentEducations;
    });

    $scope.editDocumentEducation = function (documentEducation) {
      return $state.go('persons.documentEducations.edit',
        {
          id: $stateParams.id,
          ind: documentEducation.partIndex
        });
    };

    $scope.deleteDocumentEducation = function (documentEducation) {
      return PersonDocumentEducation.remove({
        id: $stateParams.id,
        ind: documentEducation.partIndex
      })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newDocumentEducation = function () {
      return $state.go('persons.documentEducations.new');
    };
  }

  DocumentEducationsSearchCtrl.$inject = ['$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEducation'
  ];

  angular.module('gva').controller('DocumentEducationsSearchCtrl', DocumentEducationsSearchCtrl);
}(angular));
