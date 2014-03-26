/*global angular*/
(function (angular) {
  'use strict';
  angular.module('app', [
    'ng',
    'ui.router',
    'ui.select2',
    'l10n',
    'l10n-tools',
    'auth',
    'boot',
    'common',
    'scaffolding',
    'gva',
    'ems'
  ]).config([
    '$urlRouterProvider',
    '$locationProvider',
    function (
      $urlRouterProvider,
      $locationProvider
    ) {
      $locationProvider.html5Mode(false);
      $urlRouterProvider.otherwise('/persons');
    }
  ]).run(['$rootScope', '$modal', function ($rootScope, $modal) {
    var authenticating;
    $rootScope.$on('authRequired', function (event, authService) {
      if (authenticating) {
        return;
      }

      authenticating = true;
      $modal.open({
        templateUrl: 'common/login/loginModal.html',
        controller: 'LoginModalCtrl',
        backdrop: 'static',
        keyboard: false
      }).result.then(function () {
        authenticating = false;
        authService.retryFailedRequests();
      });
    });
  }]);
}(angular));
