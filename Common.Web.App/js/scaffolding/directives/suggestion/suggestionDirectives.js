// Usage: <sc-text[area]-suggestion ng-model="<model_name>" alias="<suggestions alias>">
//        </sc-text[area]-suggestion>

/*global angular, _*/
(function (angular, _) {
  'use strict';

  function createSuggestionDirective(name, templateUrl) {

    function SuggestionDirective($http) {
      var substringMatcher = function (alias) {
        return function findMatches(term, cb) {
          return $http({
            method: 'GET',
            url: 'api/suggestions/notes',
            params: { alias: alias, term: term }
          })
          .then(function (result) {
            return cb(result.data);
          });
        };
      };
      return {
        priority: 110,
        restrict: 'E',
        replace: true,
        require: '?ngModel',
        scope: {
          alias: '&'
        },
        templateUrl: templateUrl,
        link: function (scope, element, attrs, ngModel) {
          if (!ngModel) {
            return;
          }
          var input = element.children('input,textarea'),
            alias = scope.alias();

          input.attr('rows', attrs.rows);

          input.typeahead({
            hint: false,
            highlight: true
          },
          {
            name: 'values',
            displayKey: _.identity,
            source: substringMatcher(alias)
          })
          .on('typeahead:selected', function (ev, suggestion) {
            scope.$apply(function () {
              ngModel.$setViewValue(suggestion);
            });
          });

          ngModel.$render = function () {
            input.typeahead('val', ngModel.$viewValue);
          };

          input.change(function() {
            scope.$apply(function () {
              ngModel.$setViewValue(input.typeahead('val'));
            });
          });

          attrs.$observe('readonly', function (value) {
            scope.isReadonly = !!value;
          });

          scope.openTypeahead = function () {
            if (scope.isReadonly) {
              return;
            }
            input.typeahead('open');
            input.focus();
          };

          element.bind('$destroy', function () {
            input.typeahead('destroy');
          });
        }
      };
    }
    SuggestionDirective.$inject = ['$http'];

    angular.module('scaffolding').directive(name, SuggestionDirective);
  }

  createSuggestionDirective(
    'scTextSuggestion',
    'js/scaffolding/directives/suggestion/textSuggestionDirective.html'
  );
  createSuggestionDirective(
    'scTextareaSuggestion',
    'js/scaffolding/directives/suggestion/textareaSuggestionDirective.html'
  );
}(angular, _));
