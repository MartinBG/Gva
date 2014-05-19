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
      templateUrl: 'js/scaffolding/directives/promiseState/promiseStateDirective.html',
      link: function (scope) {
        scope.state = null;
        scope.title = '';

        var cancellationToken;
        scope.$watch('promise', function () {
          if (cancellationToken) {
            cancellationToken.isCancelled = true;
          }

          if (scope.promise) {
            scope.state = 'loading';
            scope.title = '';

            cancellationToken = { isCancelled: false };

            (function () {
              var c = cancellationToken;

              scope.promise
                .then(function (result) {
                  if (c.isCancelled) {
                    return;
                  }

                  scope.state = 'resolved';
                  scope.title = result;
                }, function (reason) {
                  if (c.isCancelled) {
                    return;
                  }

                  scope.state = 'rejected';
                  scope.title = reason;
                });
            })();
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