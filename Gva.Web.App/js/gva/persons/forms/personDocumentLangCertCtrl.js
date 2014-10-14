/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentLangCertCtrl($scope, scFormParams) {
    $scope.personLin = scFormParams.personLin;
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
  }

  PersonDocumentLangCertCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentLangCertCtrl', PersonDocumentLangCertCtrl);
}(angular));
