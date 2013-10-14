// Uage: <sc-search selected-filters="" btn-classes="" btn-actions=""></sc-search>
(function (angular) {
  'use strict';

  function SearchDirective() {
    function SearchController ($scope) {
      var filters = $scope.filters = {},
          nonSelectedFilters = [],
          selectedFilters = this.selectedFilters = $scope.selectedFilters,
          btnActions = $scope.btnActions;

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

      this.getBtnAction = function (actionName) {
        return btnActions[actionName];
      };
    }

    function SearchLink($scope, element) {
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

      var rowBlock = element.find('div.row');
      var buttonBlock = element.find('div.btnBlock');
      var transcludedBlock = element.find('div[ng-transclude]');
      var transcludedElements = transcludedBlock.children();
      angular.forEach(transcludedElements, function (elem) {
        if (angular.element(elem).hasClass('btnDiv')) {
          buttonBlock.append(elem);
        } else {
          rowBlock.append(elem);
        }
      });
      transcludedBlock.remove();
    }

    function SearchCompile(tElement, tAttrs) {
      if (!tAttrs.btnClasses) {
        tAttrs.btnClasses = 'col-sm-3';
      }

      return SearchLink;
    }

    return {
      restrict: 'E',
      transclude: true,
      replace: true,
      templateUrl: 'scaffolding/templates/searchTemplate.html',
      scope: {
        selectedFilters: '=',
        btnClasses: '@',
        btnActions: '='
      },
      controller: SearchController,
      compile: SearchCompile
    };
  }

  angular.module('scaffolding').directive('scSearch', SearchDirective);
}(angular));