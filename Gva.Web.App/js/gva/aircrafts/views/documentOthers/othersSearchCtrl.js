/*global angular*/
(function (angular) {
  'use strict';

  function AircraftOthersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    documentOthers
  ) {
    $scope.documentOthers = documentOthers;
  }

  AircraftOthersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'documentOthers'
  ];

  AircraftOthersSearchCtrl.$resolve = {
    documentOthers: [
      '$stateParams',
      'AircraftDocumentOthers',
      function ($stateParams, AircraftDocumentOthers) {
        return AircraftDocumentOthers.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftOthersSearchCtrl', AircraftOthersSearchCtrl);
}(angular));
