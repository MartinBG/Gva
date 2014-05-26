// Usage: <sc-password ng-model="<model_name>"></sc-text>

/*global angular*/
(function (angular) {
  'use strict';

  function PasswordDirective() {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'js/scaffolding/directives/password/passwordDirective.html'
    };
  }

  angular.module('scaffolding').directive('scPassword', PasswordDirective);
}(angular));