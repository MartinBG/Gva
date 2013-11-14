// Usage: <sc-text ng-model="<model_name>"></sc-text>

/*global angular*/
(function (angular) {
  'use strict';

  function TextDirective() {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'scaffolding/directives/text/textDirective.html'
    };
  }

  angular.module('scaffolding').directive('scText', TextDirective);
}(angular));