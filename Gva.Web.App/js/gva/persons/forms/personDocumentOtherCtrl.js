/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentOtherCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
  }

  PersonDocumentOtherCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentOtherCtrl', PersonDocumentOtherCtrl);
}(angular));
