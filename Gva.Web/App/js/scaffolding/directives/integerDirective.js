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

          element.on('change', function (ev) {
            var intNum = ev.target.value.replace(/[^0-9]+/g, '');
            element.val(intNum);

            scope.$apply(function () {
              ngModel.$setViewValue(parseInt(intNum, 10));
            });
          });
        }
      };
    });
}(angular));