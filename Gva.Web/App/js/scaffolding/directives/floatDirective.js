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
            var float = this.value.replace(/[^0-9.]+/g, '');
            element[0].value = float;

            scope.$apply(function () {
              ngModel.$setViewValue(parseFloat(float));
            });
          });
        }
      };
    });
}(angular));