// Usage: <input sc-float type="text" class="form-control input-sm" ng-model="<model_name>" />
(function (angular) {
  'use strict';

  angular.module('scaffolding')
    .directive('scFloat', function () {
      return {
        restrict: 'A',
        require: '?ngModel',
        link: function (scope, element, attrs, ngModel) {
          if (!ngModel) {
            return;
          }

          var floatRegExpr = new RegExp('^-?[0-9]+(.|,)?[0-9]*$');

          element.on('change', function (ev) {
            var value = ev.target.value,
                floatNum;

            if (floatRegExpr.test(value)) {
              floatNum = parseFloat(value.replace(',', '.'));

              if (/(\.|\,)$/g.test(value)) {
                value += '00';
              }
            }
            else {
              floatNum = null;
              value = null;
            }

            element.val(value);
            scope.$apply(function () {
              ngModel.$setViewValue(floatNum);
            });
          });
        }
      };
    });
}(angular));