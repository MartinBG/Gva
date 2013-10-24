// Usage: <sc-button action="" class="" text="" icon=""></sc-button>
(function (angular) {
  'use strict';

  function ButtonDirective($parse) {
    function ButtonCompile (tElement, tAttrs) {
      if (!tAttrs.classes) {
        tAttrs.classes = 'btn-sm btn-default';
      }

      return ButtonLink;
    }

    function ButtonLink($scope, element, attrs, scSearch) {
      if (!scSearch) {
        return;
      }

      $scope.addFilter = undefined;
      $scope.nonSelectedFilters = undefined;
      $scope.btnAction = undefined;

      if ($scope.action === 'add') {
        $scope.nonSelectedFilters = scSearch.nonSelectedFilters;
        $scope.addFilter = function (filter) {
          scSearch.selectedFilters[filter.name] = null;
        };
      }
      else {
        $scope.btnAction = $parse(attrs.action)($scope.$parent);
      }
    }

    return {
      restrict: 'E',
      require: '?^scSearch',
      replace: true,
      scope: {
        action: '@',
        classes: '@',
        text: '@',
        icon: '@'
      },
      templateUrl: 'scaffolding/templates/buttonTemplate.html',
      compile: ButtonCompile
    };
  }

  ButtonDirective.$inject = ['$parse'];

  angular.module('scaffolding').directive('scButton', ButtonDirective);
}(angular));