// Usage: <sc-button action="" class="" text="" icon=""></sc-button>
/*globals angular, $ */
(function (angular, $) {
  'use strict';

  function ButtonDirective($parse, l10n) {
    function ButtonCompile (tElement, tAttrs) {
      var btnClasses = tElement.attr('class');

      if (tAttrs.action === 'add') {
        tElement.children('button').remove();
      } else {
        tElement.children('div.btn-group').remove();
      }

      tElement.find('button').each(function (index, button) {
        $(button).attr('class', btnClasses || 'btn-sm btn-default');
      });

      tElement.attr('class', 'btn-div');

      return ButtonLink;
    }

    function ButtonLink($scope, element, attrs, scSearch) {
      if (!scSearch) {
        return;
      }

      $scope.addFilter = undefined;
      $scope.dropdownFilters = undefined;
      $scope.btnAction = undefined;
      $scope.text = undefined;

      if ($scope.action === 'add') {
        $scope.dropdownFilters = scSearch.dropdownFilters;
        $scope.addFilter = function (filterName) {
          scSearch.selectedFilters[filterName] = null;
        };
      }
      else {
        $scope.text = l10n.get(attrs.text);
        $scope.btnAction = $parse(attrs.action)($scope.$parent);
      }
    }

    return {
      restrict: 'E',
      require: '?^scSearch',
      replace: true,
      scope: {
        action: '@',
        icon: '@'
      },
      templateUrl: 'scaffolding/directives/search/buttonDirective.html',
      compile: ButtonCompile
    };
  }

  ButtonDirective.$inject = ['$parse', 'l10n'];

  angular.module('scaffolding').directive('scButton', ButtonDirective);
}(angular, $));
