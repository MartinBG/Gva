/*global angular*/
(function (angular) {
  'use strict';

  function AirportOthersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentOther,
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

    $scope.deleteDocumentOther = function (documentOther) {
      return AirportDocumentOther.remove({ id: $stateParams.id, ind: documentOther.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'AirportDocumentOther',
    'documentOthers'
  ];

  AirportOthersSearchCtrl.$resolve = {
    documentOthers: [
      '$stateParams',
      'AirportDocumentOther',
      function ($stateParams, AirportDocumentOther) {
        return AirportDocumentOther.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOthersSearchCtrl', AirportOthersSearchCtrl);
}(angular));
