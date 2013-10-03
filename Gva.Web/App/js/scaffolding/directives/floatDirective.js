(function (angular) {
  'use strict';

  angular.module('scaffolding')
    .directive('input', function () {
      return {
        restrict: 'E',
        require: '?ngModel',
        link: function (scope, element, attrs, ngModel) {
          if (!ngModel || attrs.type !== 'float') {
            return;
          }

          element.unbind('input');
          element.bind('input', function () {
            var floatNum = this.value.replace(/[^0-9.]+/g, '');
            element[0].value = floatNum;

            scope.$apply(function () {
              ngModel.$setViewValue(parseFloat(floatNum));
            });
          });
        }
      };
    });
}(angular));