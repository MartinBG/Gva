/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessFM,
    aws
  ) {
    $scope.aws = aws.map(function (aw) {
      if (aw.part && aw.part.reviews) {
        var lastReview = aw.part.reviews[aw.part.reviews.length - 1];

        aw.part.validFromDate =
          lastReview.amendment2 ? lastReview.amendment2.issueDate :
          lastReview.amendment1 ? lastReview.amendment1.issueDate :
          lastReview.issueDate;

        aw.part.validToDate =
          lastReview.amendment2 ? lastReview.amendment2.validToDate :
          lastReview.amendment1 ? lastReview.amendment1.validToDate :
          lastReview.validToDate;
      }

      return aw;
    });


    $scope.editCertAirworthiness = function (aw) {
      return $state.go('root.aircrafts.view.airworthinessesFM.edit', {
        id: $stateParams.id,
        ind: aw.partIndex
      });
    };

    $scope.newCertAirworthiness = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.new');
    };
  }

  CertAirworthinessesFMSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessFM',
    'marks'
  ];

  CertAirworthinessesFMSearchCtrl.$resolve = {
    marks: [
      '$stateParams',
      'AircraftCertAirworthinessFM',
      function ($stateParams, AircraftCertAirworthinessFM) {
        return AircraftCertAirworthinessFM.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
  .controller('CertAirworthinessesFMSearchCtrl', CertAirworthinessesFMSearchCtrl);
}(angular));
