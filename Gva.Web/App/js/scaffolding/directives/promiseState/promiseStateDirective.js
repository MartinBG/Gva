// Usage: <sc-promise-state promise="personForm.lin.$error.unique.$promise" />

/*global angular*/
(function (angular) {
  'use strict';

  function PromiseStateDirective() {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      scope: {
        promise: '='
      },
      templateUrl: 'scaffolding/directives/promiseState/promiseStateDirective.html',
      link: function (scope) {
        scope.state = null;
        scope.title = '';

        scope.$watch('promise', function () {
          if (scope.promise) {
            scope.state = 'loading';
            scope.title = '';

            scope.promise
              .then(function (result) {
                scope.state = 'resolved';
                scope.title = result;
              }, function (reason) {
                scope.state = 'rejected';
                scope.title = reason;
              });
          } else {
            scope.state = null;
            scope.title = '';
          }
        });
      }
    };
  }

  PromiseStateDirective.$inject = [];

  angular.module('scaffolding').directive('scPromiseState', PromiseStateDirective);
}(angular));