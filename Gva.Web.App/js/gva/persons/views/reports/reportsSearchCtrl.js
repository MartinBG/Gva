/*global angular*/
(function (angular) {
  'use strict';

  function ReportsSearchCtrl(
    $scope,
    reports
  ) {
    $scope.reports = reports;
  }

  ReportsSearchCtrl.$inject = [
    '$scope',
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
