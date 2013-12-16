// Usage: <sc-textarea ng-model="<model_name>"></sc-text>

/*global angular*/
(function (angular) {
  'use strict';

  function TextareaDirective() {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'scaffolding/directives/textarea/textareaDirective.html'
    };
  }

  angular.module('scaffolding').directive('scTextarea', TextareaDirective);
}(angular));