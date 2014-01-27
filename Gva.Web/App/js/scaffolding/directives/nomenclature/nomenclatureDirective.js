// Usage:
// <sc-nomenclature alias="" load="true|false" params="" multiple ng-model="">
// </sc-nomenclature>

/*global angular, _, $, Select2*/
(function (angular, _, $, Select2) {
  'use strict';

  function NomenclatureDirective($filter, $parse, Nomenclature) {
    function preLink(scope, iElement, iAttrs, ngModel) {
      var alias = scope.alias(),
          load = scope.load() || false,
          loadedValuesPromise,
          queryFunc,
          paramsFunc,
          params,
          qry;

      if (!alias) {
        throw new Error('sc-nomenclature alias not specified!');
      }

      if (iAttrs.ngDisabled) {
        scope.ngDisabled = $parse(iAttrs.ngDisabled);
      }

      if (iAttrs.params) {
        paramsFunc = $parse(iAttrs.params);

        scope.$watch(function () {
          return paramsFunc(scope.$parent);
        }, function (newVal, oldVal) {
          //skip initialization
          if (newVal !== oldVal) {
            ngModel.$setViewValue(undefined);
            ngModel.$render();
          }
        },
        true);
      }

      if (load) {
        qry = { alias: alias };

        if (paramsFunc) {
          params = paramsFunc(scope.$parent);
          _.assign(qry, params);
        }

        loadedValuesPromise = Nomenclature.query(qry).$promise;

        queryFunc = function (query) {
          loadedValuesPromise.then(function (result) {
            query.callback({
              results: $filter('filter')(result, { name: query.term })
            });
          });
        };
      } else {
        queryFunc = function (query) {
          var params,
            qry = { alias: alias, term: query.term };

          if (paramsFunc) {
            params = paramsFunc(scope.$parent);
            _.assign(qry, params);
          }

          Nomenclature
            .query(qry).$promise
            .then(function (result) {
              query.callback({ results: result });
            });
        };
      }

      scope.select2Options = {
        multiple: iAttrs.multiple,
        allowClear: true,
        placeholder: ' ', //required for allowClear to work
        query: queryFunc,
        formatResult: function (result, container, query, escapeMarkup) {
          var markup = [];
          Select2.util.markMatch(result.name, query.term, markup, escapeMarkup);
          return markup.join('');
        },
        formatSelection: function (data) {
          return data ? Select2.util.escapeMarkup(data.name) : undefined;
        },
        id: function (obj) {
          return obj.nomTypeValueId;
        }
      };
    }

    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'scaffolding/directives/nomenclature/nomenclatureDirective.html',
      scope: {
        alias: '&',
        load: '&'
      },
      require: '?ngModel',
      link: { pre: preLink }
    };
  }

  NomenclatureDirective.$inject = ['$filter', '$parse', 'Nomenclature'];

  angular.module('scaffolding').directive('scNomenclature', NomenclatureDirective);
}(angular, _, $, Select2));
