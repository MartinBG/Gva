// Usage: <sc-int ng-model="<model_name>"></sc-int>
/*global angular*/
(function (angular) {
  'use strict';

  function IntDirective() {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      require: '?ngModel',
      templateUrl: 'scaffolding/directives/int/intDirective.html',
      link: function ($scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }

        ngModel.$parsers.push(function (strValue) {
          var num = parseInt(strValue, 10);
          return isNaN(num) ? undefined : num;
        });

        ngModel.$formatters.push(function (numValue) {
          return numValue === undefined || numValue === null ?
            undefined :
            numValue;
        });
        
        element.on('blur', function() {
          var formatters = ngModel.$formatters,
              idx = formatters.length,
              value = ngModel.$modelValue;

          while(idx--) {
            value = formatters[idx](value);
          }

          if (ngModel.$viewValue !== value) {
            ngModel.$viewValue = value;
            ngModel.$render();
          }
        });
      }
    };
  }

  angular.module('scaffolding').directive('scInt', IntDirective);
}(angular));