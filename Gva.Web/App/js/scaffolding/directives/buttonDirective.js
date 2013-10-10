// Usage: <sc-button action-name="" classes="" text="" icon=""></sc-button>
(function (angular) {
  'use strict';

  function ButtonDirective() {
    return {
      restrict: 'E',
      require: '^scSearch',
      replace: true,
      scope: {
        actionName: '@',
        classes: '@',
        text: '@',
        icon: '@'
      },
      templateUrl: 'scaffolding/templates/buttonTemplate.html',
      link: function (scope, element, attrs, scSearch) {
        if (!scSearch) {
          return;
        }

        scope.btnClasses = scSearch.btnClasses;
        scope.action = undefined;
        scope.addFilter = undefined;
        scope.nonSelectedFilters = [];

        if (scope.actionName === 'add') {
          for (var modelName in scSearch.filters) {
            if (!(modelName in scSearch.selectedFilters) &&
                scSearch.filters[modelName].filterName) {
              scope.nonSelectedFilters.push({
                name: scSearch.filters[modelName].filterName,
                model: modelName
              });
            }
          }
          scSearch.nonSelectedFilters = scope.nonSelectedFilters;

          scope.addFilter = function (filter) {
            var index = scope.nonSelectedFilters.indexOf(filter);
            scope.nonSelectedFilters.splice(index, 1);

            scSearch.selectedFilters[filter.model] = undefined;
          };
        }
        else {
          scope.action = scSearch.btnActions[scope.actionName];
        }
      },
      compile: function (tElement, tAttrs) {
        if (!tAttrs.classes) {
          tAttrs.classes = 'btn-sm btn-default';
        }

        return this.link;
      }
    };
  }

  angular.module('scaffolding').directive('scButton', ButtonDirective);
}(angular));