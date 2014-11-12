/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function DocumentChecksSearchCtrl(
    $scope,
    $state,
    $stateParams,
    checks
  ) {
    $scope.checks = checks;

    $scope.isInvalidDocument = function(item){
      return item.part.valid.code === 'N';
    };

    $scope.isExpiringDocument = function(item) {
      var today = moment(new Date()),
          difference = moment(item.part.documentDateValidTo).diff(today, 'days');

      return 0 <= difference && difference <= 30;
    };

    $scope.isExpiredDocument = function(item) {
      return moment(new Date()).isAfter(item.part.documentDateValidTo);
    };
  }

  DocumentChecksSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'checks'
  ];

  DocumentChecksSearchCtrl.$resolve = {
    checks: [
      '$stateParams',
      'PersonDocumentChecks',
      function ($stateParams, PersonDocumentChecks) {
        return PersonDocumentChecks.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentChecksSearchCtrl', DocumentChecksSearchCtrl);
}(angular, moment));
