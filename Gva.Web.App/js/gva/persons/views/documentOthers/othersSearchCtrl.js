/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function DocumentOthersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    documentOthers
  ) {
    $scope.documentOthers = documentOthers;

    $scope.isInvalidDocument = function(item){
      return item.valid && item.valid.code === 'N';
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

  DocumentOthersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'documentOthers'
  ];

  DocumentOthersSearchCtrl.$resolve = {
    documentOthers: [
      '$stateParams',
      'PersonDocumentOthers',
      function ($stateParams, PersonDocumentOthers) {
        return PersonDocumentOthers.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentOthersSearchCtrl', DocumentOthersSearchCtrl);
}(angular, moment));
