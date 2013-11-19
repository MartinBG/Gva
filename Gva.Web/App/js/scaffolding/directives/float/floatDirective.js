// Usage: <sc-float ng-model="<model_name>"></sc-float>
/*global angular*/
(function (angular) {
  'use strict';

  function FloatDirective ($filter, $locale) {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      require: '?ngModel',
      templateUrl: 'scaffolding/directives/float/floatDirective.html',
      link: function (scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }

        var groupSep = $locale.NUMBER_FORMATS.GROUP_SEP;

        ngModel.$parsers.push(function (strValue) {
          var num = parseFloat(strValue.replace(',', '.'));
          return isNaN(num) ? undefined : Math.round((num + 0.00001) * 100) / 100;
        });

        ngModel.$formatters.push(function (numValue) {
          return numValue === undefined || numValue === null ?
            undefined :
            $filter('number')(numValue).replace(groupSep, '');
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

  FloatDirective.$inject = ['$filter', '$locale'];

  angular.module('scaffolding').directive('scFloat', FloatDirective);
}(angular));