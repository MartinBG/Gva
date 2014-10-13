/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentCheckCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
  }

  PersonDocumentCheckCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentCheckCtrl', PersonDocumentCheckCtrl);
}(angular));
