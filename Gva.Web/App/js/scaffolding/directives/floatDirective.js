// Usage: <sc-float ng-model="<model_name>" />
(function (angular) {
  'use strict';

  function FloatDirective ($locale) {
    return {
      restrict: 'E',
      replace: true,
      require: '?ngModel',
      templateUrl: 'scaffolding/templates/floatTemplate.html',
      link: function (scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }

        var decimalSep = $locale.NUMBER_FORMATS.DECIMAL_SEP;

        element.on('change', function (ev) {
          var value = ev.target.value.replace(',', '.'),
              floatNum = parseFloat(value);

          if (floatNum) {
            value = floatNum.toString().replace('.', decimalSep);
            element.val(value);
          }
          else {
            element.val(null);
            floatNum = null;
          }

          scope.$apply(function () {
            ngModel.$setViewValue(floatNum);
          });
        });
      }
    };
  }

  FloatDirective.$inject = ['$locale'];

  angular.module('scaffolding').directive('scFloat', FloatDirective);
}(angular));