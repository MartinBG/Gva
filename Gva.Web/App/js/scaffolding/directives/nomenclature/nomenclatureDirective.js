// Usage:
// <sc-nomenclature alias="" mode="id|object" load="true|false" params="" multiple ng-model="">
// </sc-nomenclature>

/*global angular, _, $, Select2*/
(function (angular, _, $, Select2) {
  'use strict';

  function NomenclatureDirective($filter, $parse, Nomenclature, scNomenclatureConfig) {
    function preLink(scope, iElement, iAttrs, ngModel) {
      var idProp = scNomenclatureConfig.idProp,
          nameProp = scNomenclatureConfig.nameProp,
          alias = scope.alias(),
          query = { alias: alias },
          load = scope.load() || false,
          loadedValuesPromise,
          queryFunc,
          initSelectionFunc,
          paramsFunc,
          createQuery;

      if (!alias) {
        throw new Error('sc-nomenclature alias not specified!');
      }

      if (iAttrs.ngDisabled) {
        scope.ngDisabled = $parse(iAttrs.ngDisabled);
      }

      if (iAttrs.params) {
        paramsFunc = $parse(iAttrs.params);
        createQuery = function (params) {
          return _.assign({}, query, params, paramsFunc(scope.$parent));
        };

        scope.$parent.$watch(function () {
          return paramsFunc(scope.$parent);
        }, function (newVal, oldVal) {
          //skip initialization
          if (newVal !== oldVal) {
            ngModel.$setViewValue(undefined);
            ngModel.$render();
          }
        },
        true);
      } else {
        createQuery = function (params) {
          return _.assign({}, query, params);
        };
      }

      if (iAttrs.mode === 'id') {
        ngModel.$parsers.push(function (viewValue) {
          if (viewValue === null || viewValue === undefined) {
            return viewValue;
          } else {
            return viewValue[idProp];
          }
        });

        initSelectionFunc = function (element, callback) {
          var id = element.val();

          Nomenclature
            .get(createQuery({ id: id })).$promise
            .then(function (result) {
              callback(result);
            });
        };
      }

      if (load) {
        loadedValuesPromise = Nomenclature.query(createQuery()).$promise;

        queryFunc = function (query) {
          loadedValuesPromise.then(function (result) {
            var filter = {};
            filter[nameProp] = query.term;
            query.callback({
              results: $filter('filter')(result, filter)
            });
          });
        };
      } else {
        queryFunc = function (query) {
          Nomenclature
            .query(createQuery({ term: query.term })).$promise
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
        initSelection: initSelectionFunc,
        formatResult: function (result, container, query, escapeMarkup) {
          var markup = [];
          Select2.util.markMatch(result[nameProp], query.term, markup, escapeMarkup);
          return markup.join('');
        },
        formatSelection: function (data) {
          return data ? Select2.util.escapeMarkup(data[nameProp]) : undefined;
        },
        id: function (obj) {
          return obj[idProp];
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

  NomenclatureDirective.$inject = ['$filter', '$parse', 'Nomenclature', 'scNomenclatureConfig'];

  angular.module('scaffolding')
    .constant('scNomenclatureConfig', {
      idProp: 'nomTypeValueId',
      nameProp: 'name'
    })
    .directive('scNomenclature', NomenclatureDirective);
}(angular, _, $, Select2));
