// Usage: <sc-int ng-model="<model_name>" />
(function (angular) {
  'use strict';

  function IntegerDirective($filter) {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      require: '?ngModel',
      templateUrl: 'scaffolding/templates/integerTemplate.html',
      link: function (scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }

        element.on('change', function (ev) {
          var integerNum = parseInt(ev.target.value, 10),
              integerText;

          integerNum = isNaN(integerNum) ? undefined : integerNum;
          integerText = $filter('number')(integerNum, 0);

          element.val(integerText);
          scope.$apply(function () {
            ngModel.$setViewValue(integerText);
          });
        });
      }
    };
  }

  IntegerDirective.$inject = ['$filter'];

  angular.module('scaffolding').directive('scInt', IntegerDirective);
}(angular));