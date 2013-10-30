// Usage: <sc-float ng-model="<model_name>"></sc-float>
(function (angular) {
  'use strict';

  function FloatDirective ($filter, $locale) {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      require: '?ngModel',
      templateUrl: 'scaffolding/templates/floatTemplate.html',
      link: function (scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }

        var groupSep = $locale.NUMBER_FORMATS.GROUP_SEP;

        var validate = function (value) {
          var floatNum = parseFloat(value.toString().replace(',', '.'));
          floatNum = isNaN(floatNum) ? null : floatNum.toFixed(2);

          return floatNum;
        };

        element.on('change', function () {
          ngModel.$formatters.forEach(function (formatter) {
            ngModel.$viewValue = formatter(ngModel.$viewValue);
          });

          ngModel.$render();
        });

        ngModel.$parsers.push(validate);
        ngModel.$formatters.push(function (value) {
          var validatedNum = validate(value) || undefined;

          return $filter('number')(validatedNum, 2).replace(groupSep, '');
        });
      }
    };
  }

  FloatDirective.$inject = ['$filter', '$locale'];

  angular.module('scaffolding').directive('scFloat', FloatDirective);
}(angular));