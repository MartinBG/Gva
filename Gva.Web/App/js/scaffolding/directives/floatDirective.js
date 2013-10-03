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

          element.on('change', function (ev) {
            var floatNum = ev.target.value.replace(/[^0-9.]+/g, '');
                element.val(floatNum);

            scope.$apply(function () {
              ngModel.$setViewValue(parseFloat(floatNum));
            });
          });
        }
      };
    });
}(angular));