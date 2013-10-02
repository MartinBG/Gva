(function (angular) {
  'use strict';

  angular.module('scaffolding')
    .directive('input', function () {
      return {
        restrict: 'E',
        require: '?ngModel',
        link: function (scope, element, attrs, ngModel) {
          if (!ngModel || attrs.type !== 'integer') {
            return;
          }

          element.unbind('input');
          element.bind('input', function () {
            var integer = this.value.replace(/[^0-9]+/g, '');
            element[0].value = integer;

            scope.$apply(function () {
              ngModel.$setViewValue(parseInt(integer, 10));
            });
          });
        }
      };
    });
}(angular));