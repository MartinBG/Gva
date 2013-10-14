// Uage: <sc-search selected-filters=""></sc-search>
(function (angular) {
  'use strict';

  function SearchDirective() {
    function SearchController ($scope) {
      var filters = $scope.filters = {},
          nonSelectedFilters = [],
          selectedFilters = this.selectedFilters = $scope.selectedFilters;

      this.registerFilter = function (modelName, filterScope) {
        filters[modelName] = filterScope;
      };

      this.addFilter = function (filter) {
        var index = nonSelectedFilters.indexOf(filter);
        nonSelectedFilters.splice(index, 1);

        selectedFilters[filter.model] = undefined;
        filters[filter.model].model = undefined;
      };

      this.removeFilter = function (filterName, modelName) {
        if (filterName) {
          nonSelectedFilters.push({
            name: filterName,
            model: modelName
          });
        }

        delete selectedFilters[modelName];
      };

      this.getNonSelectedFilters = function () {
        for (var modelName in filters) {
          if (!(modelName in $scope.selectedFilters) && filters[modelName].filterName) {
            nonSelectedFilters.push({
              name: filters[modelName].filterName,
              model: modelName
            });
          }
        }

        return nonSelectedFilters;
      };
    }

    function SearchLink($scope) {
      $scope.$watch('selectedFilters', function (newObj, oldObj) {
        if (newObj === oldObj) {
          return;
        }

        for (var propt in newObj) {
          if (newObj[propt] !== oldObj[propt]) {
            $scope.filters[propt].model = newObj[propt];
          }
        }
      }, true);
    }

    return {
      restrict: 'E',
      transclude: true,
      replace: true,
      templateUrl: 'scaffolding/templates/searchTemplate.html',
      scope: {
        selectedFilters: '='
      },
      controller: SearchController,
      link: SearchLink
    };
  }

  angular.module('scaffolding').directive('scSearch', SearchDirective);
}(angular));