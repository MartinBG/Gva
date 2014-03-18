// Usage:
// <sc-nomenclature alias="" mode="id|object" params="" multiple ng-model="">
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
          initSelectionFunc,
          nomObjFunc,
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
        if (iAttrs.nomObj) {
          nomObjFunc = $parse(iAttrs.nomObj);
        }

        ngModel.$parsers.push(function (viewValue) {
          if (nomObjFunc && nomObjFunc.assign) {
            nomObjFunc.assign(scope.$parent, viewValue);
          }

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
              if (nomObjFunc && nomObjFunc.assign) {
                nomObjFunc.assign(scope.$parent, result);
              }

              callback(result);
            });
        };
      }

      scope.select2Options = {
        multiple: 'multiple' in iAttrs,
        allowClear: true,
        placeholder: ' ', //required for allowClear to work
        query: function (query) {
          Nomenclature
            .query(createQuery({ term: query.term })).$promise
            .then(function (result) {
              query.callback({ results: result });
            });
        },
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
        alias: '&'
      },
      require: '?ngModel',
      link: { pre: preLink }
    };
  }

  NomenclatureDirective.$inject = ['$filter', '$parse', 'Nomenclature', 'scNomenclatureConfig'];

  angular.module('scaffolding')
    .constant('scNomenclatureConfig', {
      idProp: 'nomValueId',
      nameProp: 'name'
    })
    .directive('scNomenclature', NomenclatureDirective);
}(angular, _, $, Select2));
