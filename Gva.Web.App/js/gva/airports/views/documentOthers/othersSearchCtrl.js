/*global angular*/
(function (angular) {
  'use strict';

  function AirportOthersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    documentOthers
  ) {
    $scope.documentOthers = documentOthers;
  }

  AirportOthersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'documentOthers'
  ];

  AirportOthersSearchCtrl.$resolve = {
    documentOthers: [
      '$stateParams',
      'AirportDocumentOthers',
      function ($stateParams, AirportDocumentOthers) {
        return AirportDocumentOthers.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOthersSearchCtrl', AirportOthersSearchCtrl);
}(angular));
