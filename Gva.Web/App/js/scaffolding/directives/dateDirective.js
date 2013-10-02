(function (angular) {
  'use strict';

  angular.module('scaffolding')
    .directive('scDate', ['$filter', function ($filter) {
      return {
        restrict: 'A',
        replace: true,
        require: '?ngModel',
        template: '<div class="date input-group input-group-sm">' +
                    '<input data-format="dd.MM.yyyy" type="text" class="form-control" />' +
                    '<span class="input-group-addon">' +
                      '<span class="glyphicon glyphicon-calendar"></span>' +
                    '</span>' +
                  '</div>',
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
              var date = $filter('date')(ev.date, 'yyyy-MM-dd HH:mm:ss');

              ngModel.$setViewValue(date);
            });
          });
        }
      };
    }]);
}(angular));