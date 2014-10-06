/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocIdCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.isNewPerson = scFormParams.isNewPerson;
  }

  PersonDocIdCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonDocIdCtrl', PersonDocIdCtrl);
}(angular));
