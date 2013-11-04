// Usage: <sc-date ng-model="<model_name>" />
(function (angular) {
  'use strict';

  function DateDirective ($filter) {
    return {
      priority: 110,
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
            var modelDate = null;

            if (ev.date) {
              var year = ev.date.getFullYear(),
                  month = ev.date.getMonth(),
                  day = ev.date.getDate(),
                  date = new Date(year, month, day, 0, 0, 0, 0);

              modelDate = $filter('date')(date, 'yyyy-MM-ddTHH:mm:ss');
            }

            ngModel.$setViewValue(modelDate);
          });
        });

        element.children('input').on('change', function (ev) {
          var dateArr = ev.target.value.split('.'),
              day,
              month,
              year,
              date;

          if (dateArr.length !== 3) {
            element.children('input').val(null);
            return;
          }

          day = parseInt(dateArr[0], 10);
          month = parseInt(dateArr[1], 10);
          year = parseInt(dateArr[2], 10);

          if (isNaN(day) || isNaN(month) || isNaN(year)) {
            element.children('input').val(null);
            return;
          }

          date = new Date(year, month - 1, day, 0, 0, 0, 0);

          if (date.getFullYear() !== year ||
              date.getMonth() + 1 !== month ||
              date.getDate() !== day) {
            element.children('input').val(null);
            return;
          }
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