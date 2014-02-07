﻿// Usage: <sc-float ng-model="<model_name>"></sc-float>
/*global angular*/
(function (angular) {
  'use strict';

  function createNumberDirective (name, parser, formatter, inject) {
    function NumberDirective () {
      var injected = Array.prototype.slice.apply(arguments);
      return {
        priority: 110,
        restrict: 'E',
        replace: true,
        require: '?ngModel',
        templateUrl: 'scaffolding/directives/number/numberDirectives.html',
        link: function (scope, element, attrs, ngModel) {
          if (!ngModel) {
            return;
          }

          ngModel.$parsers.push(parser.apply(this, injected));

          ngModel.$formatters.push(formatter.apply(this, injected));

          if (attrs.min) {
            var minValidator = function(value) {
              var min = parseFloat(attrs.min);
              if (!ngModel.$isEmpty(value) && value < min) {
                ngModel.$setValidity('min', false);
              } else {
                ngModel.$setValidity('min', true);
              }

              return value;
            };

            ngModel.$parsers.push(minValidator);
            ngModel.$formatters.push(minValidator);
          }

          if (attrs.max) {
            var maxValidator = function(value) {
              var max = parseFloat(attrs.max);
              if (!ngModel.$isEmpty(value) && value > max) {
                ngModel.$setValidity('max', false);
              } else {
                ngModel.$setValidity('max', true);
              }

              return value;
            };

            ngModel.$parsers.push(maxValidator);
            ngModel.$formatters.push(maxValidator);
          }

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

    NumberDirective.$inject = inject;

    angular.module('scaffolding').directive(name, NumberDirective);
  }

  createNumberDirective(
    'scFloat',
    function () {
      return function (strValue) {
        var num = parseFloat((strValue || '').replace(',', '.'));
        return isNaN(num) ? undefined : Math.round((num + 0.00001) * 100) / 100;
      };
    },
    function ($filter, $locale) {
      return function (numValue) {
        return numValue === undefined || numValue === null ?
          undefined :
          $filter('number')(numValue).replace($locale.NUMBER_FORMATS.GROUP_SEP, '');
      };
    },
    ['$filter', '$locale']);

  createNumberDirective(
    'scInt',
    function () {
      return function (strValue) {
        var num = parseInt(strValue, 10);
        return isNaN(num) ? undefined : num;
      };
    },
    function () {
      return function (numValue) {
        return numValue === undefined || numValue === null ?
          undefined :
          numValue;
      };
    });

}(angular));