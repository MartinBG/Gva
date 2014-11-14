//Usage: <sc-select sc-select="{ select2 options object }" ng-model="model"></sc-select>

/*global angular*/
(function (angular) {
  'use strict';

  function SelectDirective($parse) {
    function preLink (scope, iElement, iAttrs) {
      scope.select2Options = $parse(iAttrs.scSelect)(scope.$parent);
    }

    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'js/scaffolding/directives/select/selectDirective.html',
      require: '?ngModel',
      link: { pre: preLink }
    };
  }
  SelectDirective.$inject = ['$parse'];

  angular.module('scaffolding').directive('scSelect', SelectDirective);
}(angular));