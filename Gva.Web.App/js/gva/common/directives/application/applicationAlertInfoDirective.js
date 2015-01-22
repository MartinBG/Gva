// Usage: <gva-application-alert-info application="" lot-id="" set=""></gva-application-alert-info>

/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationAlertInfoDirective($state, $filter)
  {
    return {
      restrict: 'E',
      replace: true,
      scope: {
        lotId: '@',
        set: '@',
        application: '='
      },
      templateUrl: 'js/gva/common/directives/application/applicationAlertInfoDirective.html',
      link: function ($scope) {
        $scope.applicationName = $scope.application.applicationCode + ' ';
        if ($scope.application.oldDocumentNumber) {
          $scope.applicationName += $scope.application.oldDocumentNumber + '/' +
            $filter('date')($scope.application.documentDate, 'mediumDate');
        } else {
          $scope.applicationName += $scope.application.documentNumber;
        }
      },
      controller: ['$scope', function ($scope) {
        $scope.exitApplication = function () {
          delete $state.params.appId;
          $state.transitionTo($state.current, $state.params, { reload: true });
        };
        
        $scope.viewApplication = function (applicationId, partIndex) {
          return $state.go('root.applications.edit.data', {
            id: applicationId,
            set: $scope.set,
            lotId: $scope.lotId,
            ind: partIndex
          });
        };
      }]
    };
  }

  ApplicationAlertInfoDirective.$inject = ['$state', '$filter'];

  angular.module('gva').directive('gvaApplicationAlertInfo', ApplicationAlertInfoDirective);
}(angular));
