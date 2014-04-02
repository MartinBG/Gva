// Usage:
// <sc-nomenclature alias="" mode="id|object" params="" multiple ng-model="">
// </sc-nomenclature>

/*global angular, _, $, Select2*/
(function (angular, _, $, Select2) {
  'use strict';

  function NomenclatureDirective(
    $filter,
    $parse,
    $exceptionHandler,
    Nomenclature,
    scNomenclatureConfig
  ) {
    function preLink(scope, iElement, iAttrs, ngModel) {
      var idProp = scNomenclatureConfig.idProp,
          nameProp = scNomenclatureConfig.nameProp,
          alias = scope.alias(),
          isMultiple = angular.isDefined(iAttrs.multiple),
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
          return _.assign({}, { alias: alias }, params, paramsFunc(scope.$parent));
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
          return _.assign({}, { alias: alias }, params);
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
          } else if (_.isArray(viewValue)) {
            return _.map(viewValue, function (item) {
              return item[idProp];
            });
          } else {
            return viewValue[idProp];
          }
        });

        initSelectionFunc = function (element, callback) {
          var val = element.select2('val'),
              resultPromise;

          if (isMultiple) {
            resultPromise = Nomenclature.query(createQuery({ ids: val })).$promise;
          } else {
            resultPromise = Nomenclature.get(createQuery({ id: val })).$promise;
          }

          resultPromise
            .then(function (result) {
              if (nomObjFunc && nomObjFunc.assign) {
                nomObjFunc.assign(scope.$parent, result);
              }

              callback(result);
            }, function (error) {
              $exceptionHandler(error);
            });
        };
      }

      scope.select2Options = {
        multiple: isMultiple,
        allowClear: true,
        placeholder: ' ', //required for allowClear to work
        query: function (query) {
          var pageSize = scNomenclatureConfig.pageSize,
              page = query.page - 1;
          Nomenclature
            .query(createQuery({ term: query.term, offset: page * pageSize, limit: pageSize }))
            .$promise
            .then(function (result) {
              query.callback({ results: result, more: result.length === pageSize });
            }, function (error) {
              $exceptionHandler(error);
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

  NomenclatureDirective.$inject = [
    '$filter',
    '$parse',
    '$exceptionHandler',
    'Nomenclature',
    'scNomenclatureConfig'
  ];

  angular.module('scaffolding')
    .constant('scNomenclatureConfig', {
      idProp: 'nomValueId',
      nameProp: 'name',
      pageSize: 20
    })
    .directive('scNomenclature', NomenclatureDirective);
}(angular, _, $, Select2));
