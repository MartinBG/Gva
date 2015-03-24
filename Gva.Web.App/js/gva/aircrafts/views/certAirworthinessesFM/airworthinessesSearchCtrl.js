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
      var lastReview, validFrom, validTo, inspectorModel;
      if (aw.part.reviews && aw.part.reviews.length) {
        lastReview = aw.part.reviews[aw.part.reviews.length - 1];
        inspectorModel = lastReview.inspector;
        validFrom = lastReview.issueDate;
        validTo = lastReview.validToDate;
      } else {
        inspectorModel =  aw.part.inspector;
        validFrom = aw.part.issueDate;
        validTo = aw.part.validToDate;
      }
      return {
        partIndex: aw.partIndex,
        act: aw.part.airworthinessCertificateType.name,
        issueDate: aw.part.issueDate,
        validFrom: validFrom,
        validTo: validTo,
        inspectorModel: inspectorModel
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
