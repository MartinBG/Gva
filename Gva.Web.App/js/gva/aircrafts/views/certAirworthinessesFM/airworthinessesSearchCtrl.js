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

        if (lastReview) {
          if (lastReview.inspector && lastReview.inspector.inspector) {
            inspectorName = lastReview.inspector.inspector.name;
          }
        }
      }

      if (aw.part.form15Amendments) {
        if (aw.part.form15Amendments.amendment2) {
          validFrom = aw.part.form15Amendments.amendment2.issueDate;
          validTo = aw.part.form15Amendments.amendment2.validToDate;
        } else if (aw.part.form15Amendments.amendment1) {
          validFrom = aw.part.form15Amendments.amendment1.issueDate;
          validTo = aw.part.form15Amendments.amendment1.validToDate;
        }
      }
      if(!validFrom && !validTo) {
        validFrom = aw.part.issueDate;
        validTo = aw.part.validToDate;
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
