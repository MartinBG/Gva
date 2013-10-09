// Usage: <sc-float ng-model="<model_name>" />
(function (angular) {
  'use strict';

  function FloatDirective ($filter) {
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

        element.on('change', function (ev) {
          var value = ev.target.value.replace(',', '.'),
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

  FloatDirective.$inject = ['$filter'];

  angular.module('scaffolding').directive('scFloat', FloatDirective);
}(angular));