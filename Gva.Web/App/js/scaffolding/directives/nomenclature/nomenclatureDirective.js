﻿// Usage:
// <sc-nomenclature alias="" load="true|false" multiple ng-model="">
// </sc-nomenclature>

/*global angular, $, Select2*/
(function (angular, $, Select2) {
  'use strict';

  function NomenclatureDirective($filter, Nomenclature) {
    function preLink(scope, iAttrs) {
      var alias = scope.alias(),
          load = scope.load() || false,
          loadedValuesPromise,
          queryFunc;

      if (!alias) {
        throw new Error('sc-nomenclature alias not specified!');
      }

      if (load) {
        loadedValuesPromise = Nomenclature.query({alias: alias}).$promise;

        queryFunc = function (query) {
          loadedValuesPromise.then(function (result) {
            query.callback({
              results: $filter('filter')(result, {name: query.term})
            });
          });
        };
      } else {
        queryFunc = function (query) {
          Nomenclature
            .query({alias: alias, term: query.term}).$promise
            .then(function (result) {
              query.callback({results: result});
            });
        };
      }

      scope.select2Options = {
        multiple: iAttrs.multiple,
        allowClear: true,
        placeholder: ' ', //required for allowClear to work
        query: queryFunc,
        formatResult: function(result, container, query, escapeMarkup) {
          var markup=[];
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
      link: { pre: preLink }
    };
  }

  NomenclatureDirective.$inject = ['$filter', 'scaffolding.Nomenclature'];

  angular.module('scaffolding').directive('scNomenclature', NomenclatureDirective);
}(angular, $, Select2));
