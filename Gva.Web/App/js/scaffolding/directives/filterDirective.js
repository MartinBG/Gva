// Usage: <sc-filter type="" name="" removable="" classes="" options="" label=""></sc-filter>
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

      var name = attrs.name;
      $scope.model = scSearch.selectedFilters[name] || null;
      $scope.label = $scope.$root.l10n.get(attrs.label);

      $scope.show = function () {
        return scSearch.selectedFilters.hasOwnProperty(name) &&
          scSearch.selectedFilters[name] !== undefined;
      };

      $scope.$watch('model', function (newVal, oldVal) {
        if (newVal !== oldVal) {
          scSearch.selectedFilters[name] = newVal;
        }
      });

      $scope.removeFilter = function () {
        delete scSearch.selectedFilters[name];
      };

      scSearch.registerFilter(name, $scope);
    }

    return {
      restrict: 'E',
      require: '?^scSearch',
      replace: true,
      scope: {
        removable: '@',
        classes: '@'
      },
      templateUrl: 'scaffolding/templates/filterTemplate.html',
      compile: FilterCompile
    };
  }

  angular.module('scaffolding').directive('scFilter', FilterDirective);
}(angular));