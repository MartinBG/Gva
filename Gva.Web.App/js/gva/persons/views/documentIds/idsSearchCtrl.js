/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function DocumentIdsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    docIds
  ) {
    $scope.documentIds = docIds;

    $scope.isInvalidDocument = function(item){
      return item.valid.code === 'N';
    };

    $scope.isExpiringDocument = function(item) {
      var today = moment(new Date()),
          difference = moment(item.documentDateValidTo).diff(today, 'days');

      return 0 <= difference && difference <= 30;
    };

    $scope.isExpiredDocument = function(item) {
      return moment(new Date()).isAfter(item.documentDateValidTo);
    };
  }

  DocumentIdsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'docIds'
  ];

  DocumentIdsSearchCtrl.$resolve = {
    docIds: [
      '$stateParams',
      'PersonDocumentIds',
      function ($stateParams, PersonDocumentIds) {
        return PersonDocumentIds.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentIdsSearchCtrl', DocumentIdsSearchCtrl);
}(angular, moment));
