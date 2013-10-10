// Usage: <sc-filter type="" model-name="" removable="" classes="" options="" filter-name="">
//        </sc-filter>
(function (angular) {
  'use strict';

  function FilterDirective() {
    return {
      restrict: 'E',
      require: '^scSearch',
      replace: true,
      scope: {
        type: '@',
        modelName: '@',
        removable: '@',
        classes: '@',
        options: '=',
        filterName: '@'
      },
      templateUrl: 'scaffolding/templates/filterTemplate.html',
      link: function (scope, element, attrs, scSearch) {
        if (!scSearch) {
          return;
        }

        var modelName = attrs.modelName;
        scope.model = scSearch.selectedFilters[modelName];
        scope.show = function () {
          return modelName in scSearch.selectedFilters;
        };

        if (scope.model !== undefined) {
          scope.$watch('model', function (value) {
            scSearch.selectedFilters[modelName] = value;
          });
        }

        scope.removeFilter = function () {
          if (scope.filterName) {
            scSearch.nonSelectedFilters.push({
              name: scope.filterName,
              model: modelName
            });
          }

          delete scSearch.selectedFilters[modelName];
        };

        scSearch.addFilter(modelName, scope);
      },
      compile: function (tElement, tAttrs) {
        var type = tAttrs.type,
            options = tAttrs.options;

        if (!tAttrs.classes) {
          tAttrs.classes = 'col-sm-3';
        }

        if (type === 'date' ||
            type === 'int' ||
            type === 'float' ||
            type === 'text' ||
            type === 'select') {
          var dirHtml, optionsHtml;

          optionsHtml = options ? ' sc-' + type + '="options"' : '';
          dirHtml = '<sc-' + type + ' ng-model="model"' + optionsHtml + '></sc-' + type + '>';

          tElement.append(dirHtml);
        }

        return this.link;
      }
    };
  }

  angular.module('scaffolding').directive('scFilter', FilterDirective);
}(angular));