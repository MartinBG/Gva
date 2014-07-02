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

    $scope.editDocumentOther = function (documentOther) {
      return $state.go('root.aircrafts.view.others.edit',
        {
          id: $stateParams.id,
          ind: documentOther.partIndex
        });
    };

    $scope.newDocumentOther = function () {
      return $state.go('root.aircrafts.view.others.new');
    };
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
