/*global angular,_*/
(function (angular, _) {
  'use strict';

  function RootCtrl($scope, $timeout, $http, orgCaseTypes) {
    $http({
      method: 'GET',
      url: 'api/user/currentData'
    }).then(function (result) {
      $scope.userFullname = result.data.userFullname;
      $scope.permissions = result.data.permissions;
    });

    $scope.$watch('permissions', function (val) {
      if (val) {
        $scope.showAdmin = val.indexOf('sys#admin') !== -1;
      }
    });

    $scope.alerts = [];
    $scope.removeAlert = function (alert) {
      var index = $scope.alerts.indexOf(alert);
      //check if it has already been removed by the user or a timeout
      if (index >= 0) {
        $scope.alerts.splice(index, 1);
      }
    };

    $scope.$on('alert', function (event, msg, type) {
      try {
        var alert = { message: msg, type: type };
        $scope.alerts.push(alert);

        //remove the alert after 60 seconds
        $timeout(function () {
          $scope.removeAlert(alert);
        }, 60 * 1000);
      } catch (e) {
        //swallow all exception so that we don't end up in an infinite loop
      }
    });

    $scope.getOrgCaseType = function (alias) {
      var caseType = _.find(orgCaseTypes, function (caseType) {
        return caseType.alias === alias;
      });

      return caseType.nomValueId;
    };
  }

  RootCtrl.$inject = ['$scope', '$timeout', '$http', 'orgCaseTypes'];

  RootCtrl.$resolve = {
    orgCaseTypes: [
      '$stateParams',
      'Nomenclatures',
      function ($stateParams, Nomenclatures) {
        return Nomenclatures.query({ alias: 'organizationCaseTypes' }).$promise;
      }
    ]
  };

  angular.module('common').controller('RootCtrl', RootCtrl);
}(angular, _));
