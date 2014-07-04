//Usage: <gva-part-select ng-model="model" lot-id="lotId" part-path="path"></gva-part-select>

/*global angular, Select2*/
(function (angular, Select2) {
  'use strict';

  function PartSelectDirective($state, $stateParams, $parse, GvaParts, gvaPartPageSize) {
    function preLink(scope, iElement, iAttrs) {
      var isMultiple = iAttrs.multiple !== undefined && iAttrs.multiple === 'true';

      scope.appSelectOpt = {
        allowClear: true,
        placeholder: ' ',
        multiple: isMultiple,
        id: function (part) {
          return part.partIndex;
        },
        formatResult: function (result, container, query, escapeMarkup) {
          var markup = [];
          Select2.util.markMatch(result.description, query.term, markup, escapeMarkup);
          return markup.join('');
        },
        formatSelection: function (part) {
          return part ? Select2.util.escapeMarkup(part.description) : undefined;
        },
        query: function (query) {
          var pageSize = gvaPartPageSize,
              page = query.page - 1;

          GvaParts.query({
            lotId: $parse(iAttrs.lotId)(scope) || $stateParams.id,
            partPath: $parse(iAttrs.partPath)(scope) || iAttrs.partPath,
            term: query.term,
            offset: page * pageSize,
            limit: pageSize
          }).$promise.then(function (result) {
            query.callback({
              results: result,
              more: result.length === pageSize
            });
          });
        }
      };
    }

    return {
      restrict: 'E',
      replace: true,
      template: '<input type="hidden" class="input-sm form-control" ui-select2="appSelectOpt" />',
      link: { pre: preLink }
    };
  }

  PartSelectDirective.$inject = [
    '$state',
    '$stateParams',
    '$parse',
    'GvaParts',
    'gvaPartPageSize'
  ];

  angular.module('gva')
    .constant('gvaPartPageSize', 20)
    .directive('gvaPartSelect', PartSelectDirective);
}(angular, Select2));
