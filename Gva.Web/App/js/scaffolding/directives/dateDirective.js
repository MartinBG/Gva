// Usage: <sc-date ng-model="<model_name>" />
(function (angular) {
  'use strict';

  function DateDirective ($filter) {
    return {
      restrict: 'E',
      replace: true,
      require: '?ngModel',
      templateUrl: 'scaffolding/templates/dateTemplate.html',
      link: function (scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }

        element.datetimepicker({
          language: 'bg',
          pickTime: false,
          weekStart: 1
        });

        ngModel.$render = function () {
          if (ngModel.$viewValue) {
            var date = new Date(ngModel.$viewValue),
                day = date.getDate(),
                month = date.getMonth() + 1,
                year = date.getFullYear();

            element.datetimepicker('setValue', day + '.' + month + '.' + year);
          }
        };

        element.datetimepicker().on('changeDate', function (ev) {
          scope.$apply(function () {
            var year = ev.date.getFullYear(),
                month = ev.date.getMonth(),
                day = ev.date.getDate(),
                date = new Date(year, month, day, 0, 0, 0, 0),
                modelDate = $filter('date')(date, 'yyyy-MM-ddTHH:mm:ss');

            ngModel.$setViewValue(modelDate);
          });
        });

        scope.$on('$destroy', function () {
          element.datetimepicker('destroy');
        });
      }
    };
  }

  DateDirective.$inject = ['$filter'];

  angular.module('scaffolding').directive('scDate', DateDirective);
}(angular));