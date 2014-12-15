/*global angular*/
(function (angular) {
  'use strict';

  function ReportsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    $compile,
    reports
  ) {
    $scope.reports = reports;

  }

  ReportsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$compile',
    'reports'
  ];

  ReportsSearchCtrl.$resolve = {
    reports: [
      '$stateParams',
      'PersonReports',
      function ($stateParams, PersonReports) {
        return PersonReports.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('ReportsSearchCtrl', ReportsSearchCtrl);
}(angular));
