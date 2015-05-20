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
    'ui.sortable',
    'common',
    'scaffolding',
    'ems'
    //'mosv'
  ])

  .config([
    '$urlRouterProvider',
    '$locationProvider',
    function (
      $urlRouterProvider,
      $locationProvider
    ) {
      $locationProvider.html5Mode(false);
      $urlRouterProvider.otherwise('/docs?filter=current');
    }
  ])

  .config(['$provide', function ($provide) {
    $provide.decorator('$exceptionHandler', [
      '$delegate',
      '$injector',
      function ($delegate, $injector) {
        var $rootScope,
            l10n;
        return function (exception, cause) {
          $delegate(exception, cause);

          try {
            $rootScope = $rootScope || $injector.get('$rootScope');
            l10n = l10n || $injector.get('l10n');
            $rootScope.$broadcast('alert', cause || l10n.get('app.unknownErrorMessage'), 'danger');
          } catch (e) {
            //swallow all exception so that we don't end up in an infinite loop
          }
        };
      }
    ]);
  }])

  .config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.headers.get = {
      'cache-control': 'no-cache, no-store, must-revalidate',
      'Pragma': 'no-cache',
      'Expires': '0'
    };
  }])

  .run(['$rootScope', '$modal', function ($rootScope, $modal) {
    var authenticating;

    $rootScope.$on('authRequired', function () {
      if (authenticating) {
        return;
      }

      authenticating = true;
      $modal.open({
        templateUrl: 'js/common/login/loginModal.html',
        controller: 'LoginModalCtrl',
        backdrop: 'static',
        keyboard: false
      }).result.then(function () {
        authenticating = false;
      });
    });
  }]);
}(angular));
