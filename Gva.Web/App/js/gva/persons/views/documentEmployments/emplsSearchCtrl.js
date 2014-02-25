/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEmploymentsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentEmployment,
    empls
  ) {
    $scope.employments = empls;

    $scope.editDocumentEmployment = function (employment) {
      return $state.go('root.persons.view.employments.edit',
        {
          id: $stateParams.id,
          ind: employment.partIndex
        });
    };

    $scope.deleteDocumentEmployment = function (employment) {
      return PersonDocumentEmployment.remove({ id: $stateParams.id, ind: employment.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newDocumentEmployment = function () {
      return $state.go('root.persons.view.employments.new');
    };
  }

  DocumentEmploymentsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEmployment',
    'empls'
  ];
  DocumentEmploymentsSearchCtrl.$resolve = {
    empls: [
      '$stateParams',
      'PersonDocumentEmployment',
      function ($stateParams, PersonDocumentEmployment) {
        return PersonDocumentEmployment.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentEmploymentsSearchCtrl', DocumentEmploymentsSearchCtrl);
}(angular));
