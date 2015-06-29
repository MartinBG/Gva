// Usage: <gva-application-alert-info application=""></gva-application-alert-info>

/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationAlertInfoDirective($state, $filter, l10n)
  {
    return {
      restrict: 'E',
      replace: true,
      scope: {
        application: '='
      },
      templateUrl: 'js/gva/common/directives/application/applicationAlertInfoDirective.html',
      link: function ($scope) {
        $scope.viewApplicationText =
          l10n.get('common.applicationAlertInfoDirective.viewApplication') +
          $scope.application.applicationName;
      },
      controller: ['$scope', function ($scope) {
        $scope.exitApplication = function () {
          delete $state.params.appId;
          $state.transitionTo($state.current, $state.params, { reload: true });
        };
      }]
    };
  }

  ApplicationAlertInfoDirective.$inject = ['$state', '$filter', 'l10n'];

  angular.module('gva').directive('gvaApplicationAlertInfo', ApplicationAlertInfoDirective);
}(angular));
