/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CertAirworthinessesFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessesFM,
    initAW
  ) {
    $scope.aw = initAW.airworthinessFMPartVersion;
    $scope.form15Amendments = initAW.form15Amendments;
    $scope.reviewOther = initAW.reviewOther;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newCertAirworthinessForm.$validate()
        .then(function () {
          var aw = $scope.aw,
              actAlias;

          if ($scope.newCertAirworthinessForm.$valid) {
            actAlias = aw.part.airworthinessCertificateType.alias;
            if (actAlias !== 'special') {
              //clone the scope object to keep it clean in case something fails
              aw = _.cloneDeep($scope.aw);

              aw.part.reviews = [];

              if (
                actAlias === 'directive8' ||
                actAlias === 'vla' ||
                actAlias === 'unknown') {
                aw.part.reviews.push($scope.reviewOther);
              }
            }

            return AircraftCertAirworthinessesFM
              .save({ id: $stateParams.id }, aw)
              .$promise
              .then(function () {
                return $state.go('root.aircrafts.view.airworthinessesFM.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.search');
    };
  }

  CertAirworthinessesFMNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessesFM',
    'initAW'
  ];

  CertAirworthinessesFMNewCtrl.$resolve = {
    initAW: [
      '$stateParams',
      'AircraftCertAirworthinessesFM',
      function ($stateParams, AircraftCertAirworthinessesFM) {
        return AircraftCertAirworthinessesFM.newCertAirworthiness({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirworthinessesFMNewCtrl', CertAirworthinessesFMNewCtrl);
}(angular, _));
