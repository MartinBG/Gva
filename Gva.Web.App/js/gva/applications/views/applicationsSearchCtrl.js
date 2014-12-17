/*global angular, moment, _*/
(function (angular, moment, _) {
  'use strict';

  function ApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    applications
    ) {
    $scope.applications = applications;

    $scope.filters = {
      set: null,
      fromDate: null,
      toDate: null,
      regUri: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.search = function () {
      return $state.go('root.applications.search', {
        set: $stateParams.set,
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        personLin: $scope.filters.personLin,
        aircraftIcao: $scope.filters.aircraftIcao,
        organizationUin: $scope.filters.organizationUin,
        stage: $scope.filters.stage
      }, { reload: true });
    };

    $scope.viewApplication = function (application) {
      return $state.go('root.applications.edit.data', 
        {
          id: application.applicationId,
          set: $stateParams.set,
          lotId: application.lotId,
          ind: application.appPartIndex
        });
    };

    $scope.newApp = function () {
      return $state.go('root.applications.new', $stateParams);
    };

    $scope.isExpiringApplication = function (item) {
      if (item.gvaApplicationStage && item.gvaApplicationStage.stageTermDate) {
        var today = moment(new Date()),
          difference = moment(item.gvaApplicationStage.stageTermDate).diff(today, 'days');

        return 0 <= difference && difference <= 7;
      }

      return false;
    };

    $scope.isExpiredApplication = function (item) {
      if (item.gvaApplicationStage && item.gvaApplicationStage.stageTermDate) {
        return moment(new Date()).isAfter(item.gvaApplicationStage.stageTermDate);
      }

      return false;
    };
  }

  ApplicationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'applications'
  ];

  ApplicationsSearchCtrl.$resolve = {
    applications: [
      '$stateParams',
      'Applications',
      function ResolveApps($stateParams, Applications) {
        return Applications.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApplicationsSearchCtrl', ApplicationsSearchCtrl);
}(angular, moment, _));
