/*global angular*/

(function (angular) {
  'use strict';

  function AppDocumentCtrl($scope, scFormParams) {
    $scope.caseReadonly = scFormParams.readonly;
  }

  AppDocumentCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('AppDocumentCtrl', AppDocumentCtrl);
}(angular));
