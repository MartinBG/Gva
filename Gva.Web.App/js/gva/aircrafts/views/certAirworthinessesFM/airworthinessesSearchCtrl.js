/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMSearchCtrl(
    $scope,
    $state,
    $stateParams,
    aws
  ) {
    $scope.aws = aws.map(function (aw) {
      var lastReview, validFrom, validTo, inspectorName;
      if (aw.part.reviews) {
        lastReview = aw.part.reviews[aw.part.reviews.length - 1];

        if (lastReview.inspector && lastReview.inspector.inspector) {
          inspectorName = lastReview.inspector.inspector.name;
        }

        if (lastReview.amendment2) {
          validFrom = lastReview.amendment2.issueDate;
          validTo = lastReview.amendment2.validToDate;
        } else if (lastReview.amendment1) {
          validFrom = lastReview.amendment1.issueDate;
          validTo = lastReview.amendment1.validToDate;
        } else {
          validFrom = lastReview.issueDate;
          validTo = lastReview.validToDate;
        }
      }

      return {
        partIndex: aw.partIndex,
        act: aw.part.airworthinessCertificateType.name,
        issueDate: aw.part.issueDate,
        validFrom: validFrom,
        validTo: validTo,
        inspectorName: inspectorName
      };
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
    'aws'
  ];

  CertAirworthinessesFMSearchCtrl.$resolve = {
    aws: [
      '$stateParams',
      'AircraftCertAirworthinessesFM',
      function ($stateParams, AircraftCertAirworthinessesFM) {
        return AircraftCertAirworthinessesFM.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
  .controller('CertAirworthinessesFMSearchCtrl', CertAirworthinessesFMSearchCtrl);
}(angular));
