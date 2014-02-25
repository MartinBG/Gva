// Usage: <sc-date ng-model="<model_name>" />

/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function DateDirective(scDateConfig) {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      require: '?ngModel',
      templateUrl: 'scaffolding/directives/date/dateDirective.html',
      link: function (scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }
        var input = element.children('input');

        element.datetimepicker({
          language: 'bg',
          pickTime: false,
          weekStart: 1
        });

        ngModel.$render = function () {
          if (ngModel.$viewValue) {
            var date = moment(ngModel.$viewValue).format('DD.MM.YYYY');
            element.datetimepicker('setValue', new Date(date));
            input.val(date);
          }
        };

        function changeDateOnSelect(ev) {
          scope.$apply(function () {
            var modelDate,
              dateShort;
            if (ev.localDate) {
              modelDate = moment(ev.localDate).startOf('day').format('YYYY-MM-DDTHH:mm:ss');
              dateShort = moment(ev.localDate).startOf('day').format('DD.MM.YYYY');
            }
            ngModel.$setViewValue(modelDate);
            input.val(dateShort);
          });
        }

        function changeDateOnInput(ev) {
          ev.preventDefault();
          ev.stopPropagation();

          scope.$apply(function () {
            var date = moment(ev.target.value, scDateConfig.dateFormats)
              .format('YYYY-MM-DDTHH:mm:ss'),
              dateShort = moment(ev.target.value, scDateConfig.dateFormats)
              .format('DD.MM.YYYY');

            if (!date || date === 'Invalid date') {
              input.val(null);
              ngModel.$setViewValue(undefined);
            } else {
              ngModel.$setViewValue(date);
              element.datetimepicker('setValue', new Date(date));
              input.val(dateShort);
            }
          });
        }

        element.datetimepicker().on('changeDate', changeDateOnSelect);
        input.on('change', changeDateOnInput);

        element.bind('$destroy', function () {
          input.off('change', changeDateOnInput);
          element.datetimepicker().off('changeDate', changeDateOnSelect);
          element.datetimepicker('destroy');
        });
      }
    };
  }
  DateDirective.$inject = ['scDateConfig'];

  angular.module('scaffolding')
    .constant('scDateConfig', {
      dateFormats: ['DD.MM.YYYY', 'DD_MM_YYYY', 'DD-MM-YYYY', 'DD/MM/YYYY', 'DD\\MM\\YYYY']
    })
    .directive('scDate', DateDirective);
}(angular, moment));