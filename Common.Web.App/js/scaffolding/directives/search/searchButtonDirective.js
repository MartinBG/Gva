// Usage: <sc-search-button action="" class="" text="" icon=""></sc-search-button>

/*globals angular, _, $ */
(function (angular,  _, $) {
  'use strict';

  function SearchButtonDirective($parse, l10n) {
    function ButtonCompile (tElement, tAttrs) {
      var btnClasses = tElement.attr('class');
      tElement.removeAttr('class');

      if (tAttrs.action === 'add') {
        tElement.children('button, sc-button').remove();
      } else {
        tElement.children('div.btn-group').remove();
      }

      tElement.find('button, sc-button').each(function (index, button) {
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
      } else if ($scope.action === 'clear') {
        $scope.clear = function () {
          _.forEach(scSearch.selectedFilters, function (value, key) {
            scSearch.selectedFilters[key] = null;
          });
          return scSearch.defaultAction($scope.$parent);
        };
      } else {
        $scope.text = l10n.get(attrs.text);

        parsedAction = $parse(attrs.action);
        $scope.btnAction = function () {
          return parsedAction($scope.$parent);
        };
      }
    }

    return {
      restrict: 'E',
      require: '^scSearch',
      replace: true,
      scope: {
        action: '@',
        icon: '@'
      },
      templateUrl: 'js/scaffolding/directives/search/searchButtonDirective.html',
      compile: ButtonCompile
    };
  }

  SearchButtonDirective.$inject = ['$parse', 'l10n'];

  angular.module('scaffolding').directive('scSearchButton', SearchButtonDirective);
}(angular, _, $));
