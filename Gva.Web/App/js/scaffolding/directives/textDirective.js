// Usage: <sc-text ng-model="<model_name>" />
(function (angular) {
  'use strict';

  function TextDirective() {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'scaffolding/templates/textTemplate.html'
    };
  }

  angular.module('scaffolding').directive('scText', TextDirective);
}(angular));