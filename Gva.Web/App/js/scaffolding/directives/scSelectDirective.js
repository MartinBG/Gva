//Usage: <sc-select ng-model="<model_name>"/></sc-select>
(function (angular) {
  'use strict';

  function scSelectDirective() {
    return {
      restrict: 'E',
      replace: true,
      require: ['?ngModel', 'uiSelect2'],
      templateUrl: 'scaffolding/templates/scSelectTemplate.html',
      link: function (scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }

      }
    };
  }

  angular.module('scaffolding').directive('scSelect', scSelectDirective);
}(angular));