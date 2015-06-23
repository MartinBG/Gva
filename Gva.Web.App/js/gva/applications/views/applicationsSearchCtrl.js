/*global angular, moment, _*/
(function (angular, moment, _) {
  'use strict';

  function ApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    Applications,
    apps
    ) {
    $scope.apps = apps;
    $scope.applicationsCount = apps.applicationsCount;
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
        stageId: $scope.filters.stageId,
        inspectorId: $scope.filters.inspectorId,
        applicationTypeId: $scope.filters.applicationTypeId
      }, { reload: true });
    };

    $scope.getApplications = function (page, pageSize) {
      var params = {set: $stateParams.set};

      _.assign(params, $scope.filters);
      _.assign(params, {
        offset: (page - 1) * pageSize,
        limit: pageSize
      });

      return Applications.get(params).$promise;
    };

    $scope.viewApplication = function (application) {
      return $state.go('root.applications.edit.data', 
        {
          id: application.applicationId
        });
    };

    $scope.newApp = function () {
      return $state.go('root.applications.new', $stateParams);
    };

    $scope.isExpiringApplication = function (item) {
      if (item.stageTermDate) {
        var today = moment(new Date()),
          difference = moment(item.stageTermDate).diff(today, 'days');

        return 0 <= difference && difference <= 7;
      }

      return false;
    };

    $scope.isExpiredApplication = function (item) {
      if (item.stageTermDate) {
        return moment(new Date()).isAfter(item.stageTermDate);
      }

      return false;
    };
  }

  ApplicationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Applications',
    'apps'
  ];

  ApplicationsSearchCtrl.$resolve = {
    apps: [
      '$stateParams',
      'Applications',
      function ResolveApps($stateParams, Applications) {
        return Applications.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApplicationsSearchCtrl', ApplicationsSearchCtrl);
}(angular, moment, _));
