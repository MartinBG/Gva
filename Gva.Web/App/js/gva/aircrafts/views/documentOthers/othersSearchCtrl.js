/*global angular*/
(function (angular) {
  'use strict';

  function AircraftOthersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOther,
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

    $scope.deleteDocumentOther = function (documentOther) {
      return AircraftDocumentOther.remove({ id: $stateParams.id, ind: documentOther.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'AircraftDocumentOther',
    'documentOthers'
  ];

  AircraftOthersSearchCtrl.$resolve = {
    documentOthers: [
      '$stateParams',
      'AircraftDocumentOther',
      function ($stateParams, AircraftDocumentOther) {
        return AircraftDocumentOther.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftOthersSearchCtrl', AircraftOthersSearchCtrl);
}(angular));
