// Usage: <sc-filter type="" model-name="" removable="" classes="" options="" filter-name="">
//        </sc-filter>
(function (angular) {
  'use strict';

  function FilterDirective() {
    function FilterCompile (tElement, tAttrs) {
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

        optionsHtml = options ? ' sc-' + type + '="' + options + '"' : '';
        dirHtml = '<sc-' + type + ' ng-model="model"' + optionsHtml + '></sc-' + type + '>';

        tElement.append(dirHtml);
      }

      return FilterLink;
    }

    function FilterLink($scope, element, attrs, scSearch) {
      if (!scSearch) {
        return;
      }

      var modelName = attrs.modelName;
      $scope.model = scSearch.selectedFilters[modelName];
      $scope.show = function () {
        return modelName in scSearch.selectedFilters;
      };

      $scope.$watch('model', function (newVal, oldVal) {
        if (newVal !== oldVal) {
          scSearch.selectedFilters[modelName] = newVal;
        }
      }, true);

      $scope.removeFilter = function () {
        scSearch.removeFilter($scope.filterName, modelName);
      };

      scSearch.registerFilter(modelName, $scope);
    }

    return {
      restrict: 'E',
      require: '?^scSearch',
      replace: true,
      scope: {
        type: '@',
        modelName: '@',
        removable: '@',
        classes: '@',
        options: '@',
        filterName: '@'
      },
      templateUrl: 'scaffolding/templates/filterTemplate.html',
      compile: FilterCompile
    };
  }

  angular.module('scaffolding').directive('scFilter', FilterDirective);
}(angular));