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

        element.on('change', function (ev) {
          var value = ev.target.value.replace(groupSep, '').replace(',', '.'),
              floatNum = parseFloat(value),
              floatText;

          floatNum = isNaN(floatNum) ? undefined : floatNum;
          floatText = $filter('number')(floatNum, 2);

          element.val(floatText);
          scope.$apply(function () {
            ngModel.$setViewValue(floatNum);
          });
        });
      }
    };
  }

  FloatDirective.$inject = ['$filter', '$locale'];

  angular.module('scaffolding').directive('scFloat', FloatDirective);
}(angular));