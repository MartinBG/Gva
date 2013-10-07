// Usage: <sc-int ng-model="<model_name>" />
(function (angular) {
  'use strict';

  function IntegerDirective() {
    return {
      restrict: 'E',
      replace: true,
      require: '?ngModel',
      templateUrl: 'scaffolding/templates/integerTemplate.html',
      link: function (scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }

        element.on('change', function (ev) {
          var value = ev.target.value,
              integerNum = parseInt(value, 10);

          if (integerNum) {
            value = integerNum.toString();
            element.val(value);
          }
          else {
            element.val(null);
            integerNum = null;
          }

          scope.$apply(function () {
            ngModel.$setViewValue(integerNum);
          });
        });
      }
    };
  }

  angular.module('scaffolding').directive('scInt', IntegerDirective);
}(angular));