// Usage: <gva-case-type init="" case-type="" model=""></gva-case-type>

/*global angular, Select2, _*/
(function (angular, Select2, _) {
  'use strict';

  function CaseTypeDirective($stateParams, $parse, Nomenclatures, GvaParts, gvaCaseTypeConfig) {
    function preLink(scope, element, attrs) {
      var isMultiple = angular.isDefined(attrs.multiple),
          caseTypeId = $parse(attrs.caseType)(scope.$parent),
          init = $parse(attrs.init)(scope.$parent),
          lotId = $parse(attrs.lotId)(scope.$parent) || attrs.lotId || $stateParams.id,
          caseTypePromise = Nomenclatures.get({ alias: 'caseTypes', id: caseTypeId }).$promise;

      if (isMultiple) {
        scope.caseType = _.pluck(scope.model, gvaCaseTypeConfig.selectObj);
      }
      else if (!isMultiple && scope.model) {
        scope.caseType = scope.model[gvaCaseTypeConfig.selectObj];
      }

      if (init && caseTypeId) {
        caseTypePromise.then(function (caseType) {
          if (isMultiple) {
            scope.caseType.push(caseType);
          }
          else {
            scope.caseType = caseType;
          }
        });
      }

      scope.caseTypeOpt = {
        multiple: isMultiple,
        allowClear: true,
        placeholder: ' ',
        id: function (caseType) {
          return caseType.nomValueId;
        },
        formatResult: function (result, container, query, escapeMarkup) {
          var markup = [];
          Select2.util.markMatch(result.name, query.term, markup, escapeMarkup);
          return markup.join('');
        },
        formatSelection: function (caseType) {
          return caseType ? Select2.util.escapeMarkup(caseType.name) : undefined;
        },
        query: function (query) {
          if (caseTypeId) {
            caseTypePromise.then(function (caseType) {
              query.callback({
                results: [caseType]
              });
            });
          }
          else {
            Nomenclatures.query({
              alias: 'caseTypes',
              lotId: lotId,
              term: query.term
            }).$promise.then(function (result) {
              query.callback({
                results: result
              });
            });
          }
        }
      };
    }

    function postLink(scope, element, attrs) {
      var isMultiple = angular.isDefined(attrs.multiple),
          lotId = $parse(attrs.lotId)(scope.$parent) || attrs.lotId || $stateParams.id;

      scope.$watch('caseType', function (newValue, oldValue) {
        if (newValue === oldValue || !newValue && !oldValue) {
          return;
        }

        if (isMultiple && newValue.length < scope.model.length) {
          var index = _.findIndex(scope.model, function (file) {
            return !file.isDeleted && !_.contains(newValue, file.caseType);
          });

          removeFile(index);
        }
        else if (!isMultiple && !newValue) {
          removeFile();
        }
        else if (isMultiple && newValue.length > oldValue.length) {
          var caseType = _.find(newValue, function (ct1) {
            return !_.some(oldValue, function (ct2) {
              return ct1.alias === ct2.alias;
            });
          });

          addNewFile(caseType);
        }
        else if (!isMultiple && newValue) {
          addNewFile(newValue);
        }
      }, true);

      var removeFile = function (index) {
        if (isMultiple && scope.model[index].isAdded) {
          scope.model.splice(index, 1);
        }
        else if (!isMultiple && scope.model.isAdded) {
          scope.model = null;
        }
        else if (isMultiple) {
          scope.model[index].isDeleted = true;
        }
        else {
          scope.model.isDeleted = true;
        }
      };

      var addNewFile = function (caseType) {
        var newFile = {
          caseType: caseType,
          file: null,
          bookPageNumber: null,
          pageCount: null,
          isAdded: true,
          isDeleted: false
        };

        GvaParts.getNextBPN({
          lotId: lotId,
          caseTypeId: caseType.nomValueId
        }).$promise.then(function (data) {
          newFile.bookPageNumber = data.nextBPN.toString();
        });

        if (isMultiple) {
          scope.model.push(newFile);
        }
        if (scope.model) {
          scope.model.isDeleted = false;
          scope.model.caseType = caseType;
        }
        else {
          scope.model = newFile;
        }
      };
    }

    return {
      restrict: 'E',
      replace: true,
      template: '<input type="hidden" class="input-sm form-control" ng-model="caseType"' +
                    'ui-select2="caseTypeOpt" />',
      link: { pre: preLink, post: postLink },
      scope: {
        model: '='
      }
    };
  }

  CaseTypeDirective.$inject = [
    '$stateParams',
    '$parse',
    'Nomenclatures',
    'GvaParts',
    'gvaCaseTypeConfig'
  ];

  angular.module('gva')
    .constant('gvaCaseTypeConfig', {
      selectObj: 'caseType'
    })
    .directive('gvaCaseType', CaseTypeDirective);
}(angular, Select2, _));
