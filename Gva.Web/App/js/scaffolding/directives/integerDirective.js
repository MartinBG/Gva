﻿// Usage: <sc-int ng-model="<model_name>"></sc-int>
(function (angular, _) {
  'use strict';

  function IntegerDirective() {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      require: '?ngModel',
      templateUrl: 'scaffolding/templates/integerTemplate.html',
      link: function ($scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }

        var validate = function (value) {
          if (!_.isNumber(value) && !_.isString(value)) {
            return null;
          }

          var integerNum = parseInt(value, 10);
          integerNum = isNaN(integerNum) ? null : integerNum;

          return integerNum;
        };

        element.on('change', function () {
          ngModel.$formatters.forEach(function (formatter) {
            ngModel.$viewValue = formatter(ngModel.$viewValue);
          });

          ngModel.$render();
        });

        ngModel.$parsers.push(validate);
        ngModel.$formatters.push(function (value) {
          var validatedNum = validate(value);

          return validatedNum ? validatedNum.toString() : '';
        });
      }
    };
  }

  angular.module('scaffolding').directive('scInt', IntegerDirective);
}(angular, _));