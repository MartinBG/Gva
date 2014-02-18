// Usage: <sc-search-button action="" class="" text="" icon=""></sc-search-button>

/*globals angular, $ */
(function (angular, $) {
  'use strict';

  function SearchButtonDirective($parse, l10n) {
    function ButtonCompile (tElement, tAttrs) {
      var btnClasses = tElement.attr('class');

      if (tAttrs.action === 'add') {
        tElement.children('button').remove();
      } else {
        tElement.children('div.btn-group').remove();
      }

      tElement.find('button').each(function (index, button) {
        $(button).attr('class', btnClasses || 'btn btn-sm btn-default');
      });

      tElement.attr('class', 'btn-div');

      return ButtonLink;
    }

    function ButtonLink($scope, element, attrs, scSearch) {
      var parsedAction;

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

        parsedAction = $parse(attrs.action);
        $scope.btnAction = function () {
          parsedAction($scope.$parent);
        };
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
      templateUrl: 'scaffolding/directives/search/searchButtonDirective.html',
      compile: ButtonCompile
    };
  }

  SearchButtonDirective.$inject = ['$parse', 'l10n'];

  angular.module('scaffolding').directive('scSearchButton', SearchButtonDirective);
}(angular, $));
