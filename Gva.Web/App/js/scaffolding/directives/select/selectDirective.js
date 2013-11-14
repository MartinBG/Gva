//Usage: <sc-select sc-select="{ select2 options object }" ng-model="model"></sc-select>
(function (angular) {
  'use strict';

  function SelectDirective() {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'scaffolding/directives/select/selectDirective.html',
      compile: function (tElement, tAttrs) {
        tAttrs.$set('uiSelect2', tAttrs.scSelect);
      }
    };
  }

  angular.module('scaffolding').directive('scSelect', SelectDirective);
}(angular));