/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentEducationCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
  }

  PersonDocumentEducationCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentEducationCtrl', PersonDocumentEducationCtrl);
}(angular));
