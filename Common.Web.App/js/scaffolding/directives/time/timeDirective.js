// Usage: <sc-date ng-model="<model_name>" />

/*global angular*/
(function (angular) {
  'use strict';

  function TimeDirective() {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      require: 'ngModel',
      scope: {
      },
      templateUrl: 'js/scaffolding/directives/time/timeDirective.html',
      link: function (scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }

        var infinityMode = attrs.mode === 'infinity';

        attrs.$observe('readonly', function (value) {
          scope.isReadonly = !!value;
        });

        ngModel.$render = function () {
          if (ngModel.$viewValue) {
            var minutes = ngModel.$viewValue / 60000;

            scope.hours = Math.floor(minutes / 60);
            scope.minutes = minutes % 60;
          }
        };

        function calculateMilliseconds() {
          var hours = scope.hours ? scope.hours : 0,
            minutes = scope.minutes ? scope.minutes : 0,
            milliseconds = (60 * hours + minutes) * 60000;
          if (scope.hours === undefined && scope.minutes === undefined) {
            ngModel.$setViewValue(undefined);
          } else {
            ngModel.$setViewValue(milliseconds);
          }
        }

        var initializingMinutes = true;
        scope.$watch('minutes', function (value) {
          if (initializingMinutes) {
            initializingMinutes = false;
          } else {
            scope.minutes = value > 59 ? undefined : value;

            calculateMilliseconds();
          }
        });

        var initializingHours = true;
        scope.$watch('hours', function (value) {
          if (initializingHours) {
            initializingHours = false;
          } else {
            if (!infinityMode) {
              scope.hours = value;
            }

            calculateMilliseconds();
          }
        });
      }
    };
  }

  angular.module('scaffolding').directive('scTime', TimeDirective);
}(angular));
