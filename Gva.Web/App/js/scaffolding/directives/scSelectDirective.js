//Usage: <sc-select ng-model="<model_name>" ui-select2="{ select2 options object }"/></sc-select>
(function (angular) {
  'use strict';

  function scSelectDirective() {
    return {
      restrict: 'E',
      replace: true,
      require: ['?ngModel', 'uiSelect2'],
      templateUrl: 'scaffolding/templates/scSelectTemplate.html'
    };
  }

  angular.module('scaffolding').directive('scSelect', scSelectDirective);
}(angular));