// Usage: <input sc-integer type="text" class="form-control input-sm" ng-model="<model_name>" />
(function (angular) {
  'use strict';

  angular.module('scaffolding')
    .directive('scInteger', function () {
      return {
        restrict: 'A',
        require: '?ngModel',
        link: function (scope, element, attrs, ngModel) {
          if (!ngModel) {
            return;
          }

          var intRegExpr = new RegExp('/[0-9]+/');

          element.on('change', function (ev) {
            var value = ev.target.value,
                integerNum;

            if (intRegExpr.test(value)) {
              integerNum = parseInt(value, 10);
            }
            else {
              integerNum = null;
              element.val(null);
            }

            scope.$apply(function () {
              ngModel.$setViewValue(integerNum);
            });
          });
        }
      };
    });
}(angular));