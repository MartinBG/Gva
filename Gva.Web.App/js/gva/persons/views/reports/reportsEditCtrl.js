/*global angular,_*/
(function (angular) {
  'use strict';

  function ReportsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonReports,
    report,
    person,
    scMessage
  ) {
    var originalReport = _.cloneDeep(report);

    $scope.report = report;
    $scope.editMode = null;
    $scope.person = person;
    $scope.lotId = $stateParams.id;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.report = _.cloneDeep(originalReport);
    };

    $scope.save = function () {
      return $scope.editReportForm.$validate()
        .then(function () {
          if ($scope.editReportForm.$valid) {
            return PersonReports
              .save({ id: $scope.lotId, ind: $stateParams.ind }, $scope.report)
              .$promise
              .then(function () {
                return $state.transitionTo(
                  'root.persons.view.reports.edit',
                  $stateParams,
                  {reload: true});
              });
          }
        });
    };

    $scope.deleteReport = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return PersonReports.remove({ id: $scope.lotId, ind: $stateParams.ind })
          .$promise.then(function () {
            return $state.go('root.persons.view.reports.search');
          });
        }
      });
    };
  }

  ReportsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonReports',
    'report',
    'person',
    'scMessage'
  ];

  ReportsEditCtrl.$resolve = {
    report: [
      '$stateParams',
      'PersonReports',
      function ($stateParams, PersonReports) {
        return PersonReports.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
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

  angular.module('gva').controller('ReportsEditCtrl', ReportsEditCtrl);
}(angular));
