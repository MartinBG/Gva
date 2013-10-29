// Usage: <sc-filter type="" name="" removable="" class="" options="" label=""></sc-filter>
(function (angular) {
  'use strict';

  function FilterDirective(l10n) {
    function FilterCompile (tElement, tAttrs) {
      var type = tAttrs.type,
          options = tAttrs.options;

      if (!tElement.attr('class')) {
        tElement.addClass('col-sm-3');
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

      var name = attrs.name,
          label = attrs.label,
          selectedFilters = scSearch.selectedFilters;

      $scope.model = null;
      $scope.label = l10n.get(label);

      $scope.show = function () {
        return selectedFilters.hasOwnProperty(name);
      };

      $scope.$watch('model', function (newVal, oldVal) {
        if (newVal !== oldVal && selectedFilters.hasOwnProperty(name)) {
          selectedFilters[name] = newVal;
        }
      });

      $scope.removeFilter = function () {
        delete selectedFilters[name];
      };

      scSearch.registerFilter(name, $scope);
    }

    FilterDirective.$inject = ['l10n'];

    return {
      restrict: 'E',
      require: '?^scSearch',
      replace: true,
      scope: {
        removable: '&'
      },
      templateUrl: 'scaffolding/templates/filterTemplate.html',
      compile: FilterCompile
    };
  }

  angular.module('scaffolding').directive('scFilter', FilterDirective);
}(angular));