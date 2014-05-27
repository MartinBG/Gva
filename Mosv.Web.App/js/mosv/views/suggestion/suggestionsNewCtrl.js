/*global angular*/
(function (angular) {
  'use strict';

  function SuggestionsNewCtrl($scope, $state, Suggestion, suggestion) {
    $scope.suggestion = suggestion;

    $scope.save = function () {
      return $scope.newSuggestionForm.$validate()
      .then(function () {
        if ($scope.newSuggestionForm.$valid) {
          return Suggestion.save($scope.suggestion).$promise
            .then(function () {
              return $state.go('root.suggestions.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.suggestions.search');
    };
  }

  SuggestionsNewCtrl.$inject = ['$scope', '$state', 'Suggestion', 'suggestion'];

  SuggestionsNewCtrl.$resolve = {
    suggestion: function () {
      return {};
    }
  };

  angular.module('mosv').controller('SuggestionsNewCtrl', SuggestionsNewCtrl);
}(angular));