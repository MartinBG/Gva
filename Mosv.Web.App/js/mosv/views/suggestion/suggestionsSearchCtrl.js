/*global angular, _*/
(function (angular, _) {
  'use strict';

  function SuggestionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    suggestions
  ) {

    $scope.filters = {
      incomingNumber: null,
      incomingLot: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.suggestions = suggestions;

    $scope.search = function () {
      $state.go('root.suggestions.search', {
        incomingNumber: $scope.filters.incomingNumber,
        incomingLot: $scope.filters.incomingLot,
        applicant: $scope.filters.applicant,
        incomingDateFrom: $scope.filters.incomingDateFrom,
        incomingDateТо: $scope.filters.incomingDate
      });
    };

    $scope.newSuggestion = function () {
      return $state.go('root.suggestions.new');
    };

    $scope.viewSuggestion = function (suggestion) {
      return $state.go('root.suggestions.edit', { id: suggestion.id });
    };
  }

  SuggestionsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'suggestions'
  ];

  SuggestionsSearchCtrl.$resolve = {
    suggestions: [
      '$stateParams',
      'Suggestions',
      function ($stateParams, Suggestions) {
        return Suggestions.query($stateParams).$promise;
      }
    ]
  };

  angular.module('mosv').controller('SuggestionsSearchCtrl', SuggestionsSearchCtrl);
}(angular, _));
