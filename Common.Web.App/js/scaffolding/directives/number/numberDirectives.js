// Usage: <sc-float ng-model="<model_name>"></sc-float>
/*global angular*/
(function (angular) {
  'use strict';

  function createNumberDirective(name, parserFactory, formatterFactory, inject) {
    function NumberDirective() {
      var injected = Array.prototype.slice.apply(arguments);
      return {
        priority: 110,
        restrict: 'E',
        replace: true,
        require: '?ngModel',
        templateUrl: 'js/scaffolding/directives/number/numberDirectives.html',
        link: function (scope, element, attrs, ngModel) {
          if (!ngModel) {
            return;
          }

          ngModel.$parsers.push(parserFactory.apply(this, [attrs].concat(injected)));

          ngModel.$formatters.push(formatterFactory.apply(this, [attrs].concat(injected)));

          if (attrs.min) {
            var minValidator = function (value) {
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
            var maxValidator = function (value) {
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

          element.on('blur', function onBlurFn() {
            var formatters = ngModel.$formatters,
                idx = formatters.length,
                value = ngModel.$modelValue;

            while (idx--) {
              value = formatters[idx](value);
            }

            if (ngModel.$viewValue !== value) {
              ngModel.$viewValue = value;
              ngModel.$render();
            }
          });

          element.bind('$destroy', function () {
            element.unbind('blur');
          });
        }
      };
    }

    NumberDirective.$inject = inject;

    angular.module('scaffolding').directive(name, NumberDirective);
  }

  function padInt(value, padding) {
    value = value.toString();
    return value.length < padding ? padInt('0' + value, padding) : value;
  }

  function isInRange(value, type) {
    if (type === 'int') {
      return value > -2147483648 && value < 2147483647;
    }
    else if (type === 'float') {
      return value > -1.7976931348623157E+308 && value < 1.7976931348623157E+308;
    }
    return false;
  }

  createNumberDirective(
    'scFloat',
    function () {
      return function (strValue) {
        var num = parseFloat((strValue || '').replace(',', '.'));
        return isNaN(num) ?
          undefined :
          isInRange(num, 'float') ?
            Math.round((num + 0.00001) * 100) / 100 :
            undefined;
      };
    },
    function (attrs, $filter, $locale) {
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
        return isNaN(num) ?
          undefined :
          isInRange(num, 'int') ?
            num :
            undefined;
      };
    },
    function (attrs) {
      return function (numValue) {
        numValue = numValue === undefined || numValue === null ?
          undefined :
          numValue;
        if (attrs.padding && numValue) {
          numValue = padInt(numValue, attrs.padding);
        }
          return numValue;
      };
    });

}(angular));
