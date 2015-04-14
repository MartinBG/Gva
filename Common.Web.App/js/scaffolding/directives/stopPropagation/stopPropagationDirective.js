// Usage: <button ng-click="someHandler() sc-stop-propagation="click"> </button>
// Usage: <button ng-click="someHandler() sc-stop-propagation="dblclick"> </button>
// Usage: <button ng-click="someHandler() sc-stop-propagation> </button> default will stop click event

/*global angular, $*/
(function (angular, $) {
  'use strict';

  function StopPropagationDirective() {
    return {
      restrict: 'A',
      link: function (scope, element, attr) {
        if (attr.scStopPropagation) {
          $(element).bind(attr.scStopPropagation, function (e) {
            e.stopPropagation();
          });
        }
        else {
          $(element).click(function (e) {
            e.stopPropagation();
          });
        }
      }
    };
  }

  StopPropagationDirective.$inject = [];

  angular.module('scaffolding').directive('scStopPropagation', StopPropagationDirective);
}(angular, $));
