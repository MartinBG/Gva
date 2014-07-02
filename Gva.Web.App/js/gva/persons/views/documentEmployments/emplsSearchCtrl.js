/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEmploymentsSearchCtrl(
    $scope,
    $state,
    $stateParams,
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

    $scope.newDocumentEmployment = function () {
      return $state.go('root.persons.view.employments.new');
    };
  }

  DocumentEmploymentsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'empls'
  ];
  DocumentEmploymentsSearchCtrl.$resolve = {
    empls: [
      '$stateParams',
      'PersonDocumentEmployments',
      function ($stateParams, PersonDocumentEmployments) {
        return PersonDocumentEmployments.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentEmploymentsSearchCtrl', DocumentEmploymentsSearchCtrl);
}(angular));
