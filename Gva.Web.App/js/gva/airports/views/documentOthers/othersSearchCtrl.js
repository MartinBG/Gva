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

    $scope.editDocumentOther = function (documentOther) {
      return $state.go('root.airports.view.others.edit',
        {
          id: $stateParams.id,
          ind: documentOther.partIndex
        });
    };

    $scope.newDocumentOther = function () {
      return $state.go('root.airports.view.others.new');
    };
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
