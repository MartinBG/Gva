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
