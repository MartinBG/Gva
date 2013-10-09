//Usage: <sc-select sc-select="{ select2 options object }" ng-model="model"></sc-select>
(function (angular) {
  'use strict';

  function scSelectDirective() {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'scaffolding/templates/selectTemplate.html',
      compile: function () {
          return {
              pre: function (scope, tElement, tAttrs) {
                  tAttrs.$set('uiSelect2', tAttrs.scSelect);
                }
            };
        }
    };
  }

  angular.module('scaffolding').directive('scSelect', scSelectDirective);
}(angular));