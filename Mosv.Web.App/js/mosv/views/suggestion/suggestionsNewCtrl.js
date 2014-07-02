/*global angular*/
(function (angular) {
  'use strict';

  function SuggestionsNewCtrl($scope, $state, Suggestions, suggestion) {
    $scope.suggestion = suggestion;

    $scope.save = function () {
      return $scope.newSuggestionForm.$validate()
      .then(function () {
        if ($scope.newSuggestionForm.$valid) {
          return Suggestions.save($scope.suggestion).$promise
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

  SuggestionsNewCtrl.$inject = ['$scope', '$state', 'Suggestions', 'suggestion'];

  SuggestionsNewCtrl.$resolve = {
    suggestion: function () {
      return {};
    }
  };

  angular.module('mosv').controller('SuggestionsNewCtrl', SuggestionsNewCtrl);
}(angular));