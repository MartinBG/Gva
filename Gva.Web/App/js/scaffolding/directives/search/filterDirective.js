// Usage: <sc-filter type="" name="" removable="" class="" options="" label=""></sc-filter>

/*global angular*/
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

      $scope.removeFilter = function () {
        delete selectedFilters[name];
      };

      scSearch.registerFilter(name, $scope);
    }

    return {
      restrict: 'E',
      require: '?^scSearch',
      replace: true,
      scope: {
        removable: '&'
      },
      templateUrl: 'scaffolding/directives/search/filterDirective.html',
      compile: FilterCompile
    };
  }

  FilterDirective.$inject = ['l10n'];

  angular.module('scaffolding').directive('scFilter', FilterDirective);
}(angular));