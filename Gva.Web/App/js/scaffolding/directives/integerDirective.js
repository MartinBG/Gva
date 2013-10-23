// Usage: <sc-int ng-model="<model_name>"></sc-int>
(function (angular) {
  'use strict';

  function IntegerDirective($filter, $locale) {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      require: '?ngModel',
      templateUrl: 'scaffolding/templates/integerTemplate.html',
      link: function (scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }

        var groupSep = $locale.NUMBER_FORMATS.GROUP_SEP;

        element.on('change', function (ev) {
          var value = ev.target.value.replace(groupSep, ''),
              integerNum = parseInt(value, 10),
              integerText;

          integerNum = isNaN(integerNum) ? undefined : integerNum;
          integerText = $filter('number')(integerNum, 0);

          element.val(integerText);
          scope.$apply(function () {
            ngModel.$setViewValue(integerNum);
          });
        });
      }
    };
  }

  IntegerDirective.$inject = ['$filter', '$locale'];

  angular.module('scaffolding').directive('scInt', IntegerDirective);
}(angular));