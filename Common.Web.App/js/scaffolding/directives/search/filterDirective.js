// Usage: <sc-filter type="" name="" removable="" class="" options="" label=""></sc-filter>

/*global angular*/
(function (angular) {
  'use strict';

  function FilterDirective(l10n, $parse) {
    function FilterCompile(tElement, tAttrs) {
      var type = tAttrs.type,
          options = tAttrs.options,
          dirHtml;

      if (!tElement.attr('class')) {
        tElement.addClass('col-sm-3');
      }

      if (type === 'date' ||
          type === 'int' ||
          type === 'float' ||
          type === 'text' ||
          type === 'select') {
        var optionsHtml;

        optionsHtml = options ? ' sc-' + type + '="' + options + '"' : '';
        dirHtml = '<sc-' + type + ' ng-model="model"' + optionsHtml + '></sc-' + type + '>';

        tElement.append(dirHtml);
      } else if (type === 'nomenclature') {
        var alias = tAttrs.alias,
          load = tAttrs.load || 'true',
          mode = tAttrs.mode,
          nomObj = tAttrs.nomObj,
          multiple = tAttrs.multiple,
          modeHtml = mode ? ' mode="' + mode + '"' : '',
          nomObjHtml = nomObj ? ' nom-obj="' + nomObj + '"' : '',
          formatOptions = tAttrs.formatOptions ?
            ' format-options="' + tAttrs.formatOptions + '"' : '',
          params = tAttrs.params ? ' params="' + tAttrs.params + '"' : '',
          multipleHtml = multiple ? ' multiple="' + multiple + '"' : '';

        dirHtml = '<sc-nomenclature ng-model="model" alias="\'' + alias +
          '\'" load="' + load + '" ' + modeHtml + nomObjHtml + multipleHtml +
          formatOptions + params + '>' +
          '</sc-nomenclature>';

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

      $scope.removable = $parse(attrs.removable)($scope);

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
      require: '^scSearch',
      replace: true,
      scope: true,
      templateUrl: 'js/scaffolding/directives/search/filterDirective.html',
      compile: FilterCompile
    };
  }

  FilterDirective.$inject = ['l10n', '$parse'];

  angular.module('scaffolding').directive('scFilter', FilterDirective);
}(angular));