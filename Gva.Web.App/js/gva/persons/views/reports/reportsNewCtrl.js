/*global angular*/
(function (angular) {
  'use strict';

  function ReportsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonReports,
    report,
    person
  ) {
    $scope.report = report;
    $scope.person = person;

    $scope.save = function () {
      return $scope.newReportForm.$validate()
        .then(function () {
          if ($scope.newReportForm.$valid) {
            return PersonReports
              .save({ id: $stateParams.id }, $scope.report)
              .$promise
              .then(function (result) {
                return $state.transitionTo(
                  'root.persons.view.reports.edit', { 
                    id: $stateParams.id, 
                    ind: result.partIndex
                  },{reload: true});
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.reports.search');
    };
  }

  ReportsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonReports',
    'report',
    'person'
  ];

  ReportsNewCtrl.$resolve = {
    report: [
      '$stateParams',
      'PersonReports',
      function ($stateParams, PersonReports) {
        return PersonReports.newReport({
          id: $stateParams.id
        });
      }
    ],
    person: [
      '$stateParams',
      'Persons',
      function ($stateParams, Persons) {
        return Persons.get({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ReportsNewCtrl', ReportsNewCtrl);
}(angular));
